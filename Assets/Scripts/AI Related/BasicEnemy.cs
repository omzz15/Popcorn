using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BasicEnemy : MonoBehaviour
{
    
    public float health =10;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private float speed;
    [SerializeField] private float agrowRange = 10;
    [SerializeField] private float attackRange = 1;
    [SerializeField] private GameObject particleSystem;
    private NavMeshAgent navMeshAgent;
    private Transform player;
    private Transform baseCore;
    private HealthManager healthManager;
    private RaycastHit hit;
    private float attackCooldown;
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        player = GameObject.FindWithTag("Player").GetComponent<Transform>();
        baseCore = GameObject.FindWithTag("baseCore").GetComponent<Transform>();
        healthManager = GameObject.FindWithTag("GameManager").GetComponent<Transform>().GetComponent<HealthManager>();
        navMeshAgent.speed = speed;
    }
    void Update()
    {
        float playerDist = Vector3.Distance(player.position, transform.position);
        float baseCoreDist = Vector3.Distance(baseCore.position, transform.position);

        //Set Navigation
        if (playerDist <= agrowRange || playerDist < baseCoreDist){

            navMeshAgent.SetDestination(player.position);

        }
        else {

            navMeshAgent.SetDestination(baseCore.position);

        }

        //Attack Regardless of whatis there
        if (playerDist < attackRange || baseCoreDist < attackRange){

            navMeshAgent.SetDestination(transform.position);

            Debug.DrawRay(transform.position+ Vector3.up*1.5f, transform.forward);
            if (attackCooldown > 1 && Physics.Raycast(transform.position + Vector3.up*1.5f, transform.forward, out hit,  attackRange*2, layerMask)){

                attackCooldown = 0;
                healthManager.DamageTarget(hit.transform.gameObject.tag, 10);

            }

        }

        attackCooldown += 3 * Time.deltaTime;

    }

    public void TakeDamage (float damage){

        health -= damage;

        if (health <= 0){

            Instantiate(particleSystem, transform.position, Quaternion.identity);
            Destroy(gameObject);

        }

    }
}
