using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunManager : MonoBehaviour
{
    public Transform playerTransform;
    public GameObject[] guns = new GameObject[1];

    private GameObject gun;
    private Gun gunScript;

    // Start is called before the first frame update
    void Start()
    {
        loadGun(0);
        loadGun(1);
    }

    // Update is called once per frame
    void Update()
    {
    }

   

    private void destroyGun(bool resetScript)
    {
        if (gun == null) return;
        Destroy(gun);
        if(resetScript) gunScript = null;
    }

    private void loadGun(GameObject gun) {
        gunScript = gun.GetComponent<Gun>();
        destroyGun(false);
        this.gun = Instantiate(gun, playerTransform.position + gunScript.playerPosOffset, playerTransform.rotation * gunScript.playerRotOffset, playerTransform);
    }

    private void loadGun(int slot) {
        loadGun(guns[slot]);
    }
}
