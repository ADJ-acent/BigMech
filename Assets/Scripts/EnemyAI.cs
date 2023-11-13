using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public Transform enemy;
    public LayerMask whatIsPlayer;

    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public float attackRange;
    private bool playerInAttackRange;

    // Update is called once per frame
    private void Awake()
    {
        //player = GameObject.Find("XRRig").transform;
        player = GameObject.Find("Player").transform;
        enemy = GameObject.Find("Enemy").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        float y = player.position.y;
        Vector3 pos = new Vector3(transform.position.x, player.position.y, transform.position.z);
        playerInAttackRange = Physics.CheckSphere(pos, attackRange, whatIsPlayer);

        if (!playerInAttackRange) ChasePlayer();
        else AttackPlayer();
    }

    private void ChasePlayer()
    {
        float y = agent.transform.position.y;
        Vector3 destination = new Vector3(player.position.x, y, player.position.z);
        agent.SetDestination(destination);
    }

    private void AttackPlayer()
    {
        agent.SetDestination(transform.position);
        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            Debug.LogFormat("PUNCH! from {0}", transform.name);

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }
}
