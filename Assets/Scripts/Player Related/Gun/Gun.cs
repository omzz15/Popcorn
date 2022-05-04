using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    //settings
    [Header("Transforms")]
    public Vector3 playerPosOffset;
    public Quaternion playerRotOffset;
    public Transform gunTip;
    public Transform shellEject;

    [Space]
    [Header("Objects")]
    public GameObject bullet;
    public GameObject shell;
    public GameObject flash;
    public GameObject noise;

    [Header("Genaral Info")]
    public bool automatic;
    public Vector2 spreadAngle;
    public float bulletsPerSec;
    public int bulletsPerShot;
    public float bulletDamage;

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
    public Vector2 zoomSpreadDecrese;
    public float zoomSensitivityDivider;
    public float zoomSpeedMultiplyer;

    [Space]
    [Header("Bullet Info")]
    public float bulletLiveTime;
    public float bulletVelocity;
    public float maxDistance;
}
