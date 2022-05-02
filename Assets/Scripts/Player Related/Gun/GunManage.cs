using UnityEngine;

public class GunManage : MonoBehaviour
{
    public SliderManager slide;
    public Camera mainCam;
    public bool debug;

    bool buttonPressed;
    bool buttonWasPressed;
    float timeFromLastReload;
    float timeBetweenReloads;
    float timeSinceLastShot;
    int lastGun = -1;
    Vector2 spreadAngle;
    Transform gunTipPos;

    void Start()
    {
        UpdateSliders();
    }

    // Update is called once per frame
    void Update()
    {
        if (GunInfo.gunNum > -1 && GameInfo.gameMode != 2)
        {
            if (lastGun != GunInfo.gunNum)
            {
                UpdateSliders();
                lastGun = GunInfo.gunNum;
            }
            if (GunInfo.ableToShoot)
            {
                MoveGun();
                if (GameInfo.gameMode == 1) Zooming();
                ReloadManager();
                CanShoot();
                if (GameInfo.gameMode == 1) Shoot();
                if (debug)
                {
                    Debug.Log("amo: " + GunInfo.currentBullets[GunInfo.gunNum]);
                    Debug.Log("max amo: " + GunInfo.maxBullets[GunInfo.gunNum]);
                    Debug.Log("shooting: " + GunInfo.shooting[GunInfo.gunNum]);
                }
            }
        }
        else if (GameInfo.gameMode != 2) 
        {
            slide.editAmo(0);
            slide.setMaxAmo(0);
        }
    }

    void ReloadManager()
    {
        if (!GunInfo.shooting[GunInfo.gunNum]) timeSinceLastShot += Time.deltaTime;
        else timeSinceLastShot = 0;

        if (!GunInfo.waitToReload[GunInfo.gunNum] || timeSinceLastShot >= GunInfo.waitTime[GunInfo.gunNum])
        {
            if (timeSinceLastShot > 1 / GunInfo.bulletsPerS[GunInfo.gunNum] || GunInfo.reloadWhileShooting[GunInfo.gunNum])
            {
                timeBetweenReloads = GunInfo.reloadTime[GunInfo.gunNum] / GunInfo.maxBullets[GunInfo.gunNum];
                timeFromLastReload += Time.deltaTime;

                if (GunInfo.currentBullets[GunInfo.gunNum] < GunInfo.maxBullets[GunInfo.gunNum] && timeFromLastReload >= timeBetweenReloads)
                {
                    GunInfo.currentBullets[GunInfo.gunNum]++;
                    slide.editAmo(GunInfo.currentBullets[GunInfo.gunNum]);
                    timeFromLastReload = 0;
                }
            }
            else timeFromLastReload = 0;
        }
        if (GunInfo.waitToReload[GunInfo.gunNum] && timeSinceLastShot <= GunInfo.waitTime[GunInfo.gunNum]) slide.editGunReload(timeSinceLastShot);
        else slide.editGunReload(GunInfo.waitTime[GunInfo.gunNum]);
        slide.editreloadWait(timeFromLastReload);
    }
    void CanShoot()
    {
        buttonPressed = Input.GetMouseButton(0);
        if (GunInfo.currentBullets[GunInfo.gunNum] > 0 && (timeSinceLastShot >= 1 / GunInfo.bulletsPerS[GunInfo.gunNum]) && buttonPressed)
        {
            if (GunInfo.automatic[GunInfo.gunNum]) GunInfo.shooting[GunInfo.gunNum] = true;
            else if (!buttonWasPressed)
            {
                GunInfo.shooting[GunInfo.gunNum] = true;
                buttonWasPressed = true;
            }
            else GunInfo.shooting[GunInfo.gunNum] = false;
        }
        else
        {
            GunInfo.shooting[GunInfo.gunNum] = false;
            if (!buttonPressed) buttonWasPressed = false;
        }
    }

    void Shoot()
    {
        if (GunInfo.shooting[GunInfo.gunNum])
        {
            GunInfo.currentBullets[GunInfo.gunNum]--;
            slide.editAmo(GunInfo.currentBullets[GunInfo.gunNum]);

            if (GunInfo.zooming)
            {
                spreadAngle.x = GunInfo.spreadAngle[GunInfo.gunNum].x - GunInfo.zoomSpreadDecrese[GunInfo.gunNum].x;
                spreadAngle.y = GunInfo.spreadAngle[GunInfo.gunNum].y - GunInfo.zoomSpreadDecrese[GunInfo.gunNum].y;
                if (!string.IsNullOrEmpty(GunInfo.gunTipName[GunInfo.gunNum])) gunTipPos = GunInfo.gunZoomPos[GunInfo.gunNum].Find(GunInfo.gunTipName[GunInfo.gunNum]);
                else if (GunInfo.gunTipZoomStart[GunInfo.gunNum] != null) gunTipPos = GunInfo.gunTipZoomStart[GunInfo.gunNum];
            }
            else
            {
                spreadAngle.x = GunInfo.spreadAngle[GunInfo.gunNum].x;
                spreadAngle.y = GunInfo.spreadAngle[GunInfo.gunNum].y;
                if (!string.IsNullOrEmpty(GunInfo.gunTipName[GunInfo.gunNum])) gunTipPos = GunInfo.gunNormalPos[GunInfo.gunNum].transform.Find(GunInfo.gunTipName[GunInfo.gunNum]);
                else if (GunInfo.gunTipStart[GunInfo.gunNum] != null) gunTipPos = GunInfo.gunTipStart[GunInfo.gunNum];
            }
            if (spreadAngle.x < 0) spreadAngle.x = 0;
            if (spreadAngle.y < 0) spreadAngle.y = 0;

            for (int i = 0; i < GunInfo.bulletsPerShot[GunInfo.gunNum]; i++)
            {

                Vector3 raycastDir;
                Quaternion bulletRot = new Quaternion(0,0,0,0);

                float randX = Random.Range(-.005f * spreadAngle.y, .005f * spreadAngle.y);
                float randY = Random.Range(-.005f * spreadAngle.x, .005f * spreadAngle.x);

                if (gunTipPos != null)
                {
                    bulletRot.x = gunTipPos.rotation.x + randX;
                    bulletRot.y = gunTipPos.rotation.y + randY;
                    bulletRot.z = gunTipPos.rotation.z;
                    bulletRot.w = gunTipPos.rotation.w;
                }

                Quaternion camWithRand = new Quaternion(mainCam.transform.rotation.x + randX, mainCam.transform.rotation.y + randY, mainCam.transform.rotation.z, mainCam.transform.rotation.w);

                raycastDir = camWithRand * Vector3.forward;
                
                if (i % 10 == 0 || i < 10)
                {
                    if (GunInfo.bulletTip[GunInfo.gunNum] != null && gunTipPos != null) Instantiate(GunInfo.bulletTip[GunInfo.gunNum], gunTipPos.position, bulletRot);
                    else Debug.LogWarning("bullet tip has or more missing parts and can not load");
                }
                if (Physics.Raycast(mainCam.transform.position, raycastDir, out RaycastHit hit, GunInfo.force[GunInfo.gunNum]))
                {
                    if (debug) Debug.DrawRay(mainCam.transform.position, bulletRot * Vector3.forward, Color.yellow, 1);
                    if (hit.transform.CompareTag("Player")) GunInfo.numOfSelfBulletsHitPlayer++;
                    else if (hit.transform.CompareTag("Enemy") && GunInfo.bulletDamage[GunInfo.gunNum] > hit.transform.GetComponent<EnemyHealth>().bulletResistance) hit.transform.GetComponent<EnemyHealth>().amountOfDamageTaken += (GunInfo.bulletDamage[GunInfo.gunNum] - hit.transform.GetComponent<EnemyHealth>().bulletResistance);
                    else if ((hit.transform.CompareTag("Obilisc") || hit.transform.CompareTag("Block")) && hit.transform.GetComponent<ObiliscHealthManager>().bulletProtection < GunInfo.bulletDamage[GunInfo.gunNum]) hit.transform.GetComponent<ObiliscHealthManager>().amountOfDamageTaken += GunInfo.bulletDamage[GunInfo.gunNum] - hit.transform.GetComponent <ObiliscHealthManager>().bulletProtection;
                    if (debug) Debug.Log(hit.transform.tag);
                }
            }
            if (GunInfo.gunFlash[GunInfo.gunNum] != null && gunTipPos != null) Instantiate(GunInfo.gunFlash[GunInfo.gunNum], gunTipPos.position, gunTipPos.rotation);
            else Debug.LogWarning("one or more parts is mising form gun flash so it was not loaded");
        }
    }
    void UpdateSliders()
    {
        if (GunInfo.gunNum >= 0)
        {
            slide.setMaxAmo(GunInfo.maxBullets[GunInfo.gunNum]);
            slide.editAmo(GunInfo.currentBullets[GunInfo.gunNum]);
            if (GunInfo.waitToReload[GunInfo.gunNum])
            {
                slide.setMaxGunReload(GunInfo.waitTime[GunInfo.gunNum]);
                slide.editGunReload(GunInfo.waitTime[GunInfo.gunNum]);
            }
            else
            {
                slide.setMaxGunReload(1);
                slide.editGunReload(1);
            }
            slide.setMaxReloadWait(GunInfo.reloadTime[GunInfo.gunNum] / GunInfo.maxBullets[GunInfo.gunNum]);
        }
    }
    void MoveGun()
    {
        string gunStr = "Gun" + GunInfo.gunNum;
        string crossHairString = "crossHair" + GunInfo.gunNum;
        if (GunInfo.zooming)
        {
            if (GunInfo.gun[GunInfo.gunNum] != null && GunInfo.gunZoomPos[GunInfo.gunNum] != null)
            {
                GameObject.Find(gunStr).transform.position = GunInfo.gunZoomPos[GunInfo.gunNum].position;
                GameObject.Find(gunStr).transform.rotation = GunInfo.gunZoomPos[GunInfo.gunNum].rotation;
            }
            else Debug.LogWarning("one or more parts is mising form gun zoom move so it was not moved");

            if (GunInfo.crossHairs[GunInfo.gunNum] != null && GunInfo.crossHairPos[GunInfo.gunNum] != null && GameObject.Find(crossHairString))
            {
                GameObject.Find(crossHairString).transform.position = GunInfo.crossHairPos[GunInfo.gunNum].position;
                GameObject.Find(crossHairString).GetComponent<Transform>().position += new Vector3(0, 0, GunInfo.crossHairPos[GunInfo.gunNum].position.y * GunInfo.crossHairMagnification[GunInfo.gunNum]);
                GameObject.Find(crossHairString).transform.rotation = GunInfo.crossHairPos[GunInfo.gunNum].rotation;
            }
            else Debug.LogWarning("one or more parts is mising form cross hair move so it was not moved");
        }
        else
        {
            if (GunInfo.gun[GunInfo.gunNum] != null && GunInfo.gunNormalPos[GunInfo.gunNum] != null)
            {
                GameObject.Find(gunStr).transform.position = GunInfo.gunNormalPos[GunInfo.gunNum].position;
                GameObject.Find(gunStr).transform.rotation = GunInfo.gunNormalPos[GunInfo.gunNum].rotation;
            }
            else Debug.LogWarning("one or more parts is mising form gun normal move so it was not moved");

            if (GunInfo.crossHairs[GunInfo.gunNum] != null && GunInfo.crossHairPos[GunInfo.gunNum] != null && GameObject.Find(crossHairString))
            {
                GameObject.Find(crossHairString).transform.position = GunInfo.crossHairPos[GunInfo.gunNum].position;
                GameObject.Find(crossHairString).transform.rotation = GunInfo.crossHairPos[GunInfo.gunNum].rotation;
            }
            else Debug.LogWarning("one or more parts is mising form cross hair move so it was not moved");
        }
    }
    void Zooming()
    {
        if (Input.GetMouseButton(1) && GunInfo.ableToZoom[GunInfo.gunNum]) GunInfo.zooming = true;
        else GunInfo.zooming = false;
    }
}