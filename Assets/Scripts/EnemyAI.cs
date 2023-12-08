using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.VFX;

public class EnemyAI : MonoBehaviour
{
    Animator anim;
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask whatIsPlayer;
    public Transform mechTransform;

    // public GameObject aliveEnemy;
    // public GameObject deadEnemy;
    // public ParticleSystem deathVFX;

    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public float attackRange;
    private bool playerInAttackRange;
    private bool dead;

    private GameObject attackSignBlue0;
    private GameObject attackSignGreen0;
    private GameObject warningSignLeft0;
    private GameObject warningSignRight0;
    private Image warningSignLeft;
    private Image warningSignRight;
    private Image attackSignBlue;
    private Image attackSignGreen;
    public SmallCrabUI smallCrabUI;

    public PlayerController playerController;
    public float damage;
    public Canvas canvas;

    private void Awake()
    {
        SpawnIndicator();

        anim = GetComponent<Animator>();
        player = GameObject.Find("PlayerController").transform;
        mechTransform = GameObject.Find("Mech").transform;
        agent = GetComponent<NavMeshAgent>();
        transform.LookAt(player);
        // deathVFX.Stop();
    }

    private void Update()
    {
        if (!dead)
        {
            playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

            if (!playerInAttackRange) ChasePlayer();
            else AttackPlayer();
        }
    }

    public void SpawnIndicator()
    {
        attackSignBlue0 = Instantiate(smallCrabUI.attackSignBlue0, smallCrabUI.attackSignBlue0.transform.position, smallCrabUI.attackSignBlue0.transform.rotation, canvas.transform);
        attackSignGreen0 = Instantiate(smallCrabUI.attackSignGreen0, smallCrabUI.attackSignGreen0.transform.position, smallCrabUI.attackSignGreen0.transform.rotation, canvas.transform);
        warningSignLeft0 = Instantiate(smallCrabUI.warningSignLeft, smallCrabUI.warningSignLeft.transform.position, smallCrabUI.warningSignLeft.transform.rotation, canvas.transform);
        warningSignRight0 = Instantiate(smallCrabUI.warningSignRight, smallCrabUI.warningSignRight.transform.position, smallCrabUI.warningSignRight.transform.rotation, canvas.transform);

        attackSignBlue = attackSignBlue0.GetComponent<Image>();
        attackSignGreen = attackSignGreen0.GetComponent<Image>();
        warningSignLeft = warningSignLeft0.GetComponent<Image>();
        warningSignRight = warningSignRight0.GetComponent<Image>();
        attackSignBlue.rectTransform.sizeDelta = new Vector2(60, 60);
        attackSignGreen.rectTransform.sizeDelta = new Vector2(60, 60);
        attackSignBlue.enabled = false;
        attackSignGreen.enabled = false;
        warningSignLeft.enabled = false;
        warningSignRight.enabled = false;
    }

    private void ChasePlayer()
    {
        float y = agent.transform.position.y;
        Vector3 destination = new Vector3(player.position.x, y, player.position.z);
        agent.SetDestination(destination);
    }

    private void AttackPlayer()
    {
        smallCrabUI.AttackSignCalc(attackSignBlue, warningSignLeft, warningSignRight, transform);
        agent.SetDestination(transform.position);
        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            anim.SetTrigger("Attack");

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
            // aliveEnemy.SetActive(false);
            // deadEnemy.SetActive(true);
            // deathVFX.Clear();
            // deathVFX.Play();
            agent.enabled = false;
            GetComponent<BoxCollider>().enabled = false;
            dead = true;
            attackSignBlue.enabled = false;
            smallCrabUI.HideWarningSign(warningSignLeft, warningSignRight);
            StartCoroutine(Wait());
            anim.SetTrigger("Hit");
        }
    }

    private void Hit()
    {
        playerController.TakeDamage(damage);
    }

    public IEnumerator Wait()
    {
        smallCrabUI.AttackSignCalc(attackSignGreen, warningSignLeft, warningSignRight, transform);
        yield return new WaitForSeconds(2);     
        Destroy(attackSignBlue0);
        Destroy(attackSignGreen0);
        Destroy(attackSignBlue);
        Destroy(attackSignGreen);   
        Destroy(warningSignLeft0);
        Destroy(warningSignRight0);
        Destroy(warningSignLeft);
        Destroy(warningSignRight);
        // TODO: replace with animation
        Destroy(transform);
    }
}
