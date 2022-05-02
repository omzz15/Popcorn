using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunSwitcher : MonoBehaviour
{
    int lastGun = -1;
    void Awake()
    {
        for (int i = 0; i < 9; i++) GunInfo.gunsInHand[i] = i;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameInfo.gameMode == 1)
        {
            for (int i = 48; i < 58; i++)
            {
                int num = i - 48;
                if (Input.GetKey(((char)i).ToString()))
                {
                    if (num == 0)
                    {
                        GunInfo.ableToShoot = false;
                        GunInfo.gunNum = -1;
                        DestoryCurrentGun();

                    }
                    else if (GunInfo.gunInitilized[GunInfo.gunsInHand[num - 1]] && GunInfo.gunsInHand[num - 1] != -1)
                    {
                        GunInfo.ableToShoot = true;
                        GunInfo.gunNum = GunInfo.gunsInHand[num - 1];
                        SpawnGun();
                    }
                }
            }
        }
    }
    void SpawnGun() 
    {
        if (GunInfo.gunNum != lastGun) 
        {
            DestoryCurrentGun();
            string newGunStr = "Gun" + GunInfo.gunNum;
            string newCrossHairName = "crossHair" + GunInfo.gunNum;
            if (GunInfo.gun[GunInfo.gunNum] != null)
            {
                GameObject newGun = Instantiate(GunInfo.gun[GunInfo.gunNum]);
                newGun.name = newGunStr;
            }
            else Debug.LogWarning("one or more components is mising to spawn current gun so curent gun was not spawned. current gun is:" + newGunStr);

            if (GunInfo.crossHairs[GunInfo.gunNum] != null)
            {
                GameObject newCrossHair = Instantiate(GunInfo.crossHairs[GunInfo.gunNum]);
                newCrossHair.name = newCrossHairName;
            }
            else Debug.LogWarning("one or more components is mising to spawn current cross hair so current cross hair was not spawned. current cross is:" + newCrossHairName);
        }
    }
    void DestoryCurrentGun() 
    {
        string lastGunStr = "Gun" + lastGun;
        string lastCrossHairName = "crossHair" + lastGun;
        if (lastGun > -1)
        {
            if (GunInfo.gun[lastGun] != null && GameObject.Find(lastGunStr)) Destroy(GameObject.Find(lastGunStr));
            else Debug.LogWarning("one or more components is mising to delete last gun so last gun was not deleted. last gun is:" + lastGunStr);
            if (GameObject.Find(lastCrossHairName) && GunInfo.crossHairs[lastGun] != null) Destroy(GameObject.Find(lastCrossHairName));
            else Debug.LogWarning("one or more components is mising to delete last cross hair so last cross hair was not deleted. last cross hair is:" + lastCrossHairName);
        } 
        lastGun = GunInfo.gunNum;
    }
}
