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

    private void loadGun(GameObject gun) {
        this.gun = gun;
        gunScript = gun.GetComponent<Gun>();

        Instantiate(gun, gunScript.playerPosOffset, gunScript.playerRotOffset, playerTransform);
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
