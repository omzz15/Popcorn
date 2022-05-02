using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    float myTimeAlive;

    void Start()
    {
        myTimeAlive = GunInfo.timeAlive[GunInfo.gunNum];
        gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * GunInfo.force[GunInfo.gunNum]);
    }

    // Update is called once per frame
    void Update()
    {
        myTimeAlive -= Time.deltaTime;

        if (myTimeAlive <= 0) Destroy(gameObject);
    }
    void OnTriggerEnter(Collider other)
    {
        if(!other.CompareTag("SelfBullet")) Destroy(gameObject);
    }
}
