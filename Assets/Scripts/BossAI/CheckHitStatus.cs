using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;
using UnityEngine.AI;

namespace BossAI
{
    public class CheckHitStatus : Node
    {
        private float hitTime = .5f;
        private float hitCounter = 0f;
        private float blockTime = 3f;
        private float blockCounter = 0f;
        private Animator _animator;
        private Health _health;
        private Transform _transform;
        private NavMeshAgent _navMeshAgent;
        private bool isHit;
        private bool isBlocking;
        
        public CheckHitStatus (Transform transform)
        {
            _transform = transform;
            _animator = transform.GetComponent<Animator>();
            _health = transform.GetComponent<Health>();
            _navMeshAgent = transform.GetComponent<NavMeshAgent>();
        }
        public override NodeState Evaluate()
        {
            object isStunned = GetData("stun");
            // if currently is blocking, check if we passed the block timer
            if (isBlocking)
            {
                if (blockCounter + Time.deltaTime < blockTime)
                {
                    blockCounter += Time.deltaTime;
                    return NodeState.Success;
                }
                _animator.SetTrigger("Idle");
                _navMeshAgent.isStopped = false;
                isBlocking = false;
                return NodeState.Failure;
            }
            
            
            if (isStunned == null || !(bool)isStunned)
            {
                if (isHit)
                {
                    
                    return NodeState.Success;
                }
                
                return NodeState.Failure;
            }
            if (isHit)
            {
                _navMeshAgent.isStopped = true;
                _transform.position -= _transform.forward * 5;
                hitCounter = 0f;
                return NodeState.Success;
            }

            return NodeState.Failure;
        }

        private void setHitStatus()
        {
            isHit = true;
        }
    }
}