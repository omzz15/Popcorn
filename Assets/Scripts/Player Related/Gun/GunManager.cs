using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunManager : MonoBehaviour
{
    public Transform playerTransform;
    public GameObject[] guns = new GameObject[1];
    public int[] gunsInHand = new int[1];

    private GameObject gun;
    private Gun gunScript;

    // Start is called before the first frame update
    void Start()
    {
        loadGunFromHand(0);
        loadGunFromHand(1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private bool shootKeyPressed() {
        return (Input.GetButtonDown("shoot") || (gunScript.automatic && Input.GetButton("shoot")));
    }

    public void loadGunFromHand(int slot) {
        loadGun(gunsInHand[slot]);
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

    /*
    public void addGun(GameObject gun, int slot) {
        guns[slot] = gun;
    }
    */
}
