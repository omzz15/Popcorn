using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunControl : MonoBehaviour
{
    private Transform head;
    private AudioSource audioSource;
    private ParticleSystem particleSource;
    private Animator animator;

    void Start(){

        audioSource = GetComponent<AudioSource>();
        particleSource = gameObject.transform.GetChild(0).gameObject.GetComponent<ParticleSystem>();
        head = GetComponentInParent<Transform>();
        animator = GetComponent<Animator>();

    }
    void Update()
    {
        Debug.DrawRay(head.position, transform.forward);
        //shoot
        if (Input.GetMouseButtonDown(0)){

            audioSource.Play();
            particleSource.Clear();
            particleSource.Play();
            animator.Play("PistolShoot");

            RaycastHit hit;
            if (Physics.Raycast(head.position, transform.right, out hit)){

                if (hit.transform.gameObject.tag == "enemy") {

                    hit.transform.gameObject.GetComponent<BasicEnemy>().TakeDamage(10);

                }

            }

        }
    }
}
