using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunControl : MonoBehaviour
{
    private AudioSource audioSource;

    void Start(){

        audioSource = GetComponent<AudioSource>();

    }
    void Update()
    {
        //shoot
        if (Input.GetMouseButtonDown(0)){

            audioSource.Play();

            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit)){

                if (hit.transform.gameObject.tag == "enemy") {

                    hit.transform.gameObject.GetComponent<BasicEnemy>().TakeDamage(10);

                }

            }

        }
    }
}
