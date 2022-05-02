using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunPositionGetter : MonoBehaviour
{
    public int gunNum;
    public bool hasGunTip;
    public string gunTipName;
    [Header("Normal Pos")]
    public Transform gunNormalPos;
    public Transform gunTipStart;
    public Transform crossHair;
    [Header("Zoom Pos")]
    public Transform gunZoomPos;
    public Transform gunTipZoomStart;

    void Start()
    {
        if (hasGunTip) 
        {
            if (!string.IsNullOrEmpty(gunTipName)) GunInfo.gunTipName[gunNum - 1] = gunTipName;
            else Debug.LogWarning("gun tip name not set or has gun tip has been selected");
        }
    }
    void Update()
    {
        if (gunNum - 1 == GunInfo.gunNum && GameInfo.gameMode == 1)
        {
            GunInfo.gunNormalPos[gunNum - 1] = gunNormalPos;
            
            GunInfo.crossHairPos[gunNum - 1] = crossHair;
            GunInfo.gunZoomPos[gunNum - 1] = gunZoomPos;

            if (!hasGunTip) 
            {
                GunInfo.gunTipStart[gunNum - 1] = gunTipStart;
                GunInfo.gunTipZoomStart[gunNum - 1] = gunTipZoomStart;
            }
        }
    }
}
