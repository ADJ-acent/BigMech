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
    //public Transform enemy;
    public LayerMask whatIsPlayer;
    public Transform mechTransform;

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
        //enemy = GameObject.Find("Enemy").transform;
        mechTransform = GameObject.Find("Mech").transform;
        agent = GetComponent<NavMeshAgent>();
        transform.LookAt(player);
        deathVFX.Stop();
    }

    private void Update()
    {
        if (!dead)
        {
            playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

            if (!playerInAttackRange) 
            {
                ChasePlayer();
            }
            else
            {
                ShowIndicators();
                AttackPlayer();
            }
        }
    }

    private void ShowIndicators()
    {
        Vector3 diff = transform.position - player.position;
        Vector3 projectedVector = new Vector3(diff.x, 0, diff.z);
        float angleToPosition = Vector3.SignedAngle(mechTransform.forward, projectedVector, Vector3.up);
        Debug.LogFormat("{0} has angle {1}", transform.name, angleToPosition);

        if (angleToPosition > 55f)
        {
            playerCanvas.ShowRightWarningSign();
        }
        else if (angleToPosition < -55f)
        {
            playerCanvas.ShowLeftWarningSign();
        }
        else playerCanvas.ShowAttackSign(player.position, transform.position);
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
            //dead = true;
        }
    }
}
