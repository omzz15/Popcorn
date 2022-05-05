using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    //settings
    [Header("Transforms")]
    public Vector3 playerPosOffset;
    public Quaternion playerRotOffset;
    public Vector3 zoomingPlayerPosOffset;
    public Quaternion zoomingPlayerRotOffset;
    public Transform gunTip;
    //public Transform shellEject;

    [Space]
    [Header("Objects")]
    public ParticleSystem flash;
    public Animator recoil;
    public AudioSource noise;

    [Header("Genaral Info")]
    public bool automatic;
    public Vector2 spreadAngle;
    public int bulletsPerShot;
    public float bulletDamage;

    [Space]
    [Header("Reload Info")]
    public int currentBullets;
    public int maxBullets;
    public float reloadTime;
    public float reloadWaitTime;

    [Space]
    [Header("Zoom Info")]
    public float zoomMagnification;
    public float crosshairMagnification;
    public Vector2 zoomSpreadDecrese;
    public float zoomSensitivityDivider;
    public float zoomSpeedMultiplyer;

    [Space]
    [Header("Bullet Info")]
    public float maxDistance;


    private float timeSinceLastShot;
    private float timeSinceLastReload;

    private void Update()
    {
        timeSinceLastReload += Time.deltaTime;
        timeSinceLastShot += Time.deltaTime;
    }

    public void resetTimeSinceLastShot() {
        timeSinceLastShot = 0;
    }

    public void resetTimeSinceLastRelaod() { 
        timeSinceLastReload = 0;
    }

    private bool shootKeyPressed()
    {
        return (Input.GetButtonDown("shoot") || (automatic && Input.GetButton("shoot")));
    }

    private bool doneShooting()
    {
        return recoil.GetCurrentAnimatorStateInfo(0).IsName("empty");
    }

    private bool canShoot()
    {
        return currentBullets > 0 && doneShooting();
    }

    private bool canReload()
    {
        return (timeSinceLastShot >= reloadWaitTime && timeSinceLastReload >= reloadTime && currentBullets < maxBullets);
    }

    private void shoot() {
        if (canShoot() && shootKeyPressed()) {
            if (Info.isZooming)
            {
                spreadAngle.x = spreadAngle.x - zoomSpreadDecrese.x;
                spreadAngle.y = spreadAngle.y - zoomSpreadDecrese.y;
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
                Quaternion bulletRot = new Quaternion(0, 0, 0, 0);

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
                    else if ((hit.transform.CompareTag("Obilisc") || hit.transform.CompareTag("Block")) && hit.transform.GetComponent<ObiliscHealthManager>().bulletProtection < GunInfo.bulletDamage[GunInfo.gunNum]) hit.transform.GetComponent<ObiliscHealthManager>().amountOfDamageTaken += GunInfo.bulletDamage[GunInfo.gunNum] - hit.transform.GetComponent<ObiliscHealthManager>().bulletProtection;
                    if (debug) Debug.Log(hit.transform.tag);
                }
            }
            if (GunInfo.gunFlash[GunInfo.gunNum] != null && gunTipPos != null) Instantiate(GunInfo.gunFlash[GunInfo.gunNum], gunTipPos.position, gunTipPos.rotation);
            else Debug.LogWarning("one or more parts is mising form gun flash so it was not loaded");

            resetTimeSinceLastShot();
            currentBullets --;
        }
    }
}
