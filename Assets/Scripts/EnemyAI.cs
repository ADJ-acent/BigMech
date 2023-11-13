using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.VFX;

public class EnemyAI : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public Transform enemy;
    public LayerMask whatIsPlayer;

    public PlayerCanvas playerCanvas;
    public GameObject aliveEnemy;
    public GameObject deadEnemy;
    public ParticleSystem deathVFX;
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public float attackRange;
    private bool playerInAttackRange;
    private bool dead;
    private void Awake()
    {
        player = GameObject.Find("PlayerController").transform;
        enemy = GameObject.Find("Enemy").transform;
        agent = GetComponent<NavMeshAgent>();
        transform.LookAt(player);
        deathVFX.Stop();
    }

    private void Update()
    {
        if (!dead)
        {
            playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

            if (!playerInAttackRange) ChasePlayer();
            else
            {
                // Vector3 toPosition = (transform.position - player.position).normalized();
                // float angleToPosition = Vector3.Angle(player.forward, toPosition);

                // if (angleToPosition > 60f)
                // {
                //     playerCanvas.ShowLeftWarningSign();
                // }
                // else if (angleToPosition < -60f)
                // {
                //     playerCanvas.ShowRightWarningSign();
                // }
                // else playerCanvas.ShowAttackSign(player.position, transform.position);
                AttackPlayer();
            }
        }
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
            // playerCanvas.ShowBlockSign(player.position, transform.position);
            Debug.LogFormat("PUNCH! from {0}", transform.name);

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Mech"))
        {
            aliveEnemy.SetActive(false);
            deadEnemy.SetActive(true);
            deathVFX.Clear();
            deathVFX.Play();
            agent.enabled = false;
            GetComponent<BoxCollider>().enabled = false;
            dead = true;
        }
    }
}
