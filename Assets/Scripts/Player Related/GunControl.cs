using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunControl : MonoBehaviour
{
    private Transform head;
    private AudioSource audioSource;
    private ParticleSystem particleSource;
    private Animator animator;

    public int shootDist = 10;

    void Start(){

        audioSource = GetComponent<AudioSource>();
        particleSource = gameObject.transform.GetChild(0).gameObject.GetComponent<ParticleSystem>();
        head = transform.parent.GetComponent<Transform>();
        animator = GetComponent<Animator>();

    }
    void Update()
    {
        //shoot
        if (Input.GetMouseButtonDown(0)){

            audioSource.Play();
            particleSource.Clear();
            particleSource.Play();
            animator.Play("PistolShoot");

            RaycastHit hit;
            if (Physics.Raycast(head.position, transform.right, out hit, shootDist)){

                if (hit.transform.gameObject.tag == "enemy") {

                    hit.transform.gameObject.GetComponent<BasicEnemy>().TakeDamage(10);

                }

            }

        }
    }
}
