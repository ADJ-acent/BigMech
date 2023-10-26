using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;

    // Update is called once per frame
    private void Awake()
    {
        //player = GameObject.Find("XRRig").transform;
        player = Camera.main.transform;
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        ChasePlayer();
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }
}
