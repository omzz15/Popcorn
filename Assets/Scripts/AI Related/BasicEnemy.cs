using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BasicEnemy : MonoBehaviour
{
    
    [SerializeField] private float health;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private float speed;
    [SerializeField] private float agrowRange = 10;
    [SerializeField] private float attackRange = 1;
    private NavMeshAgent navMeshAgent;
    private Transform player;
    private Transform baseCore;
    private RaycastHit hit;
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        player = GameObject.FindWithTag("Player").GetComponent<Transform>();
        baseCore = GameObject.FindWithTag("baseCore").GetComponent<Transform>();
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

            if (Physics.BoxCast(transform.position, transform.localScale , transform.forward, out hit, transform.rotation,  attackRange, layerMask)){

                //hit.transform.gameObject.

            }

        }

    }
}
