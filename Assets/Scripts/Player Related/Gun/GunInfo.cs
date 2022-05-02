using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunInfo : MonoBehaviour
{
    /// <summary>
    /// Gun Settings
    /// </summary>

    public static int numOfGuns = 9;
    public static int[] gunsInHand = new int[9];
    public static bool[] gunInitilized = new bool[numOfGuns];
    public static bool ableToShoot;

    // gameobjects, scripts, and transforms
    public static GameObject[] bulletTip = new GameObject[numOfGuns];
    public static GameObject[] gun = new GameObject[numOfGuns];
    public static GameObject[] gunFlash = new GameObject[numOfGuns];
    public static GameObject[] crossHairs = new GameObject[numOfGuns];
    public static string[] gunTipName = new string[numOfGuns];
    public static Transform[] gunNormalPos = new Transform[numOfGuns];
    public static Transform[] gunZoomPos = new Transform[numOfGuns];
    public static Transform[] gunTipStart = new Transform[numOfGuns];
    public static Transform[] gunTipZoomStart = new Transform[numOfGuns];
    public static Transform[] crossHairPos = new Transform[numOfGuns];

    //Genaral Info
    public static int gunNum;
    public static Vector2[] spreadAngle = new Vector2[numOfGuns];
    public static float[] bulletsPerS = new float[numOfGuns];
    public static int[] bulletsPerShot = new int[numOfGuns];
    public static float[] bulletDamage = new float[numOfGuns];
    public static bool[] automatic = new bool[numOfGuns];

    // reload info
    public static int[] currentBullets = new int[numOfGuns];
    public static int[] maxBullets = new int[numOfGuns];
    public static float[] reloadTime = new float[numOfGuns];
    public static bool[] waitToReload = new bool[numOfGuns];
    public static float[] waitTime = new float[numOfGuns];
    public static bool[] reloadWhileShooting = new bool[numOfGuns];

    // zoom
    public static bool[] ableToZoom = new bool[numOfGuns];
    public static float[] zoomMagnification = new float[numOfGuns];
    public static float[] zoomSensitivityDivider = new float[numOfGuns];
    public static float[] crossHairMagnification = new float[numOfGuns];
    public static float[] zoomSpeedMultiplyer = new float[numOfGuns];
    public static Vector2[] zoomSpreadDecrese = new Vector2[numOfGuns];
    public static bool zooming;

    // bullet info
    public static float[] force = new float[numOfGuns];
    public static float[] timeAlive = new float[numOfGuns];

    //other
    public static bool[] shooting = new bool[numOfGuns];

    public static int numOfSelfBulletsHitPlayer;
}
