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

    private void Awake()
    {
        Info.currentGun = this;
        transform.position = playerPosOffset;
        transform.rotation = playerRotOffset;

        defineActions();
    }

    private void Update()
    {
        updateTimes();
        setScoping();
        reload();
        shoot();
    }

    private void updateTimes() {
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

    private bool shouldShoot()
    {
        return currentBullets > 0 && doneShooting() && shootKeyPressed();
    }

    private bool canReload()
    {
        return (timeSinceLastShot >= reloadWaitTime && timeSinceLastReload >= reloadTime && currentBullets < maxBullets);
    }

    private void setScoping() {
        if (Input.GetKeyDown("Scope"))
            Info.SetScoping(true, true);
        else if (Input.GetKeyUp("Scope"))
            Info.SetScoping(false, true);
    }

    private void reload() {
        if (canReload()) {
            currentBullets++;
            resetTimeSinceLastRelaod();
        }
    }

    private void shoot() {
        if (shouldShoot()) {
            for (int i = 0; i < bulletsPerShot; i++)
            {
                Transform ray = gunTip;

                float randX = Random.Range(-.005f * spreadAngle.y, .005f * spreadAngle.y);
                float randY = Random.Range(-.005f * spreadAngle.x, .005f * spreadAngle.x);

                
                
                Vector3 raycastDir = ray.forward;

                if (Physics.Raycast(gunTip,out RaycastHit hit, maxDistance))
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

    private void defineActions() {
        GameController.GetActionManager().AddAction(ActionManager.k_OnScoping, () => {
            transform.position = zoomingPlayerPosOffset;
            transform.rotation = zoomingPlayerRotOffset;
            spreadAngle.x -= zoomSpreadDecrese.x;
            spreadAngle.y -= zoomSpreadDecrese.y;
            
        });
        GameController.GetActionManager().AddAction(ActionManager.k_OnUnscoping, () => {
            transform.position = playerPosOffset;
            transform.rotation = playerRotOffset;
            spreadAngle.x += zoomSpreadDecrese.x;
            spreadAngle.y += zoomSpreadDecrese.y;
        });
    }
}
