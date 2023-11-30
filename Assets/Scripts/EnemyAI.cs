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
    public float angle;
    private bool dead;

    public PlayerController playerController;
    public float damage;

    public GameObject attackSign0;
    public GameObject blockSign0;
    private GameObject attackSign;
    private GameObject blockSign;
    public Canvas canvas;

    private void Awake()
    {
        SpawnIndicator();

        angle = 35f;
        player = GameObject.Find("PlayerController").transform;
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
                // var playerCanvas = new PlayerCanvas();
                // playerCanvas = Instantiate(playerCanvas1, transform.position + star.getVector(), Quaternion.identity);
                // playerCanvas.transform.parent = playerCanvas1.transform;

                AttackPlayer();
            }
        }
    }

    public void SpawnIndicator()
    {
        attackSign = Instantiate(attackSign0, attackSign0.transform.position, attackSign0.transform.rotation);
        blockSign = Instantiate(blockSign0, blockSign0.transform.position, blockSign0.transform.rotation);

        // attackSign = Instantiate(attackSign0) as Image;
        attackSign.transform.SetParent(canvas.transform, false);
        // blockSign = Instantiate(blockSign0) as Image;
        blockSign.transform.SetParent(canvas.transform, false);

        attackSign.SetActive(false);
        blockSign.SetActive(false);
    }

    private void AttackSignCalc()
    {
        Vector3 diff = transform.position - player.position;
        Vector3 projectedVector = new Vector3(diff.x, 0, diff.z);
        float angleToPosition = Vector3.SignedAngle(mechTransform.forward, projectedVector, Vector3.up);

        if (angleToPosition > angle)
        {
            playerCanvas.ShowRightWarningSign();
        }
        else if (angleToPosition < (-1 * angle))
        {
            playerCanvas.ShowLeftWarningSign();
        }
        else 
        {
            if (angleToPosition < 0) playerCanvas.ShowAttackSign(attackSign, Mathf.Abs(angleToPosition), -1);
            else if (angleToPosition >= 0) playerCanvas.ShowAttackSign(attackSign, Mathf.Abs(angleToPosition), 1);
        }
    }

    private void BlockSignCalc()
    {
        Vector3 diff = transform.position - player.position;
        Vector3 projectedVector = new Vector3(diff.x, 0, diff.z);
        float angleToPosition = Vector3.SignedAngle(mechTransform.forward, projectedVector, Vector3.up);

        if ((-1 * angle) <= angleToPosition && angleToPosition <= angle)
        {
            if (angleToPosition < 0) playerCanvas.ShowBlockSign(attackSign, blockSign, Mathf.Abs(angleToPosition), -1);
            else if (angleToPosition >= 0) playerCanvas.ShowBlockSign(attackSign, blockSign, Mathf.Abs(angleToPosition), 1);
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
        AttackSignCalc();
        agent.SetDestination(transform.position);
        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            BlockSignCalc();
            Debug.LogFormat("PUNCH! from {0}", transform.name);
            playerController.health -= damage;

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
