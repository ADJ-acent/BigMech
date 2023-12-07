using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;
using Unity.VisualScripting;
using UnityEngine.AI;

namespace BossAI
{
    public class CheckHitStatus : Node
    {
        private float blockTime = 2f;
        private float blockCounter = 0f;
        private Animator _animator;
        private Health _health;
        private Transform _transform;
        private NavMeshAgent _navMeshAgent;
        private bool isHit;
        private bool isBlocking;
        private float lastBlockCounter = 0f;
        public CheckHitStatus (Transform transform)
        {
            _transform = transform;
            _animator = transform.GetComponent<Animator>();
            _health = transform.GetComponent<Health>();
            _navMeshAgent = transform.GetComponent<NavMeshAgent>();
            RobotArmProjection.hit += SetHitStatus;
        }
        public override NodeState Evaluate()
        {
            // if currently is blocking, check if we passed the block timer
            if (isBlocking)
            {
                if (blockCounter + Time.deltaTime < blockTime)
                {
                    blockCounter += Time.deltaTime;
                    return NodeState.Success;
                }

                lastBlockCounter = Time.time;
                _animator.SetTrigger("Idle");
                _navMeshAgent.isStopped = false;
                isBlocking = false;
                blockCounter = 0f;
                return NodeState.Failure;
            }
            object isStunned = GetData("stun");
            //hit when not stunned
            if ((isStunned == null || !(bool)isStunned) && isHit)
            {
                _animator.SetTrigger("Block");
                blockCounter = 0f;
                isBlocking = true;
                isHit = false;
                return NodeState.Success;
            }
            return NodeState.Failure;
        }

        private void SetHitStatus(Vector3 hitDirection, int damage)
        {
            object isStunned = GetData("stun");
            object isAttacking = GetData("Attack");
            // if stunned move crab to the opposite of the hit
            if (isStunned != null && (bool) isStunned)// strong hit
            {
                AudioManager.Instance.playBigHit();
                _navMeshAgent.isStopped = true;
                _animator.SetTrigger("Hit");
                if (_health.DealDamage(damage)) parent.SetData("dead", true);
                
            }
            // shouldn't interrupt attack
            else if (isAttacking != null && (bool)isAttacking || (Time.time - lastBlockCounter > 4.0f))
            {
                AudioManager.Instance.playMidHit();
                if (_health.DealDamage(damage / 10)) parent.SetData("dead", true);
                return;
            }

            if (!isBlocking) // weak hit
            {
                AudioManager.Instance.playMidHit();
                if (_health.DealDamage(damage / 10)) parent.SetData("dead", true);
            }
            else
            {
                // else (hit but no damage)
                AudioManager.Instance.playSmallHit();
            }
            isHit = true;
        }
    }
}