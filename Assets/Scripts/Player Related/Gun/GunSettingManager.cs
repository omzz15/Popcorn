using UnityEngine;

public class GunSettingManager : MonoBehaviour
{
    [Header("Genaral Info")]
    public int gunNum;
    public bool gunUseable;
    public GameObject gun;
    public GameObject bulletTip;
    public GameObject gunFlash;
    public GameObject crossHair;

    public Vector2 spreadAngle;
    public float bulletsPerSec;
    public int bulletsPerShot;
    public float bulletDamage;
    public bool automatic;

    [Space]
    [Header("Reload Info")]
    public int currentBullets;
    public int maxBullets;
    public float reloadTime;
    public bool waitToReload;
    public float waitTime;
    public bool reloadWhileShooting;

    [Space]
    [Header("ZoomInfo")]
    public bool ableToZoom;
    public float zoomMagnification;
    public float zoomSensitivityDivider;
    public float crossHairMagnification;
    public float zoomSpeedMultiplyer;
    public Vector2 zoomSpreadDecrese;

    [Space]
    [Header("Bullet Info")]
    public float timeEachBulletIsAlive;
    public float forcePerBullet;


    void Start()
    {
        //Genaral Info
        GunInfo.bulletTip[gunNum - 1] = bulletTip;
        GunInfo.gun[gunNum - 1] = gun;
        GunInfo.gunFlash[gunNum - 1] = gunFlash;
        GunInfo.crossHairs[gunNum - 1] = crossHair;
        GunInfo.spreadAngle[gunNum - 1] = spreadAngle;
        GunInfo.bulletsPerS[gunNum - 1] = bulletsPerSec;
        GunInfo.bulletsPerShot[gunNum - 1] = bulletsPerShot;
        GunInfo.bulletDamage[gunNum - 1] = bulletDamage;
        GunInfo.automatic[gunNum - 1] = automatic;

        //Reload Info
        GunInfo.currentBullets[gunNum - 1] = currentBullets;
        GunInfo.maxBullets[gunNum - 1] = maxBullets;
        GunInfo.reloadTime[gunNum - 1] = reloadTime;
        GunInfo.waitToReload[gunNum - 1] = waitToReload;
        GunInfo.waitTime[gunNum - 1] = waitTime;
        GunInfo.reloadWhileShooting[gunNum - 1] = reloadWhileShooting;

        //Zoom
        GunInfo.ableToZoom[gunNum - 1] = ableToZoom;
        GunInfo.zoomMagnification[gunNum - 1] = zoomMagnification;
        GunInfo.zoomSensitivityDivider[gunNum - 1] = zoomSensitivityDivider;
        GunInfo.crossHairMagnification[gunNum - 1] = crossHairMagnification;
        GunInfo.zoomSpeedMultiplyer[gunNum - 1] = zoomSpeedMultiplyer;
        GunInfo.zoomSpreadDecrese[gunNum - 1] = zoomSpreadDecrese;

        //Bullet Info
        GunInfo.timeAlive[gunNum - 1] = timeEachBulletIsAlive;
        GunInfo.force[gunNum - 1] = forcePerBullet;

        //other
        GunInfo.gunInitilized[gunNum - 1] = gunUseable;
    }
}
