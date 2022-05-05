using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunManager : MonoBehaviour
{
    public Transform cameraTransform;
    public GameObject[] guns = new GameObject[1];

    private int currentGun = -1;
    private GameObject gun;

    // Start is called before the first frame update
    void Start()
    {
        loadGun(0);
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 48; i < 58; i++)
        {
            int num = i - 48;
            if (Input.GetKey(((char)i).ToString()))
            {
                if (num == 0)
                {
                    destroyGun();
                }
                else if (num - 1 < guns.Length)
                {
                    loadGun(num - 1);
                }
            }
        }
    }

   

    private void destroyGun()
    {
        //if (gun == null) return;
        Destroy(gun);
        currentGun = -1;
    }

    private void loadGun(GameObject gun) {
        destroyGun();
        this.gun = Instantiate(gun, cameraTransform);
    }

    private void loadGun(int slot) {
        if (currentGun == slot) return;
        loadGun(guns[slot]);
        currentGun = slot;
    }
}
