using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public PlayerCanvas playerMechanics;

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
        player = GameObject.Find("Player").transform;
        enemy = GameObject.Find("Enemy").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInAttackRange) ChasePlayer();
        else 
        {
            playerMechanics.ShowAttackSign(player.position, enemy.position);
            AttackPlayer();
        }
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }

    private void AttackPlayer()
    {
        agent.SetDestination(transform.position);
        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            playerMechanics.ShowBlockSign(player.position, enemy.position);
            Debug.LogFormat("PUNCH! from {0}", transform.name);

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
            // playerMechanics.HideBlockSign(player.position, enemy.position);
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }
}
