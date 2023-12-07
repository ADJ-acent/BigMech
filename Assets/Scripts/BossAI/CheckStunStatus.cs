using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;
using UnityEngine.AI;

namespace BossAI
{
    public class CheckStunStatus : Node
    {
        private bool isStunned = false;
        private float stunTime = 3.5f;
        private float stunCounter = 0f;
        private Animator _animator;
        private PlayerController _player;
        private NavMeshAgent _navMeshAgent;
        
        public CheckStunStatus (Transform transform, PlayerController player)
        {
            BlockedStun.blocked += setStunStatus;
            _animator = transform.GetComponent<Animator>();
            _navMeshAgent = transform.GetComponent<NavMeshAgent>();
            _player = player;
        }
        public override NodeState Evaluate()
        {
            if (isStunned)
            {
                stunCounter += Time.deltaTime;
                if (stunCounter >= stunTime)
                {
                    stunCounter = 0f;
                    isStunned = false;
                    parent.SetData("stun", false);
                    _animator.SetFloat("AttackDir", 1f);
                    _animator.SetTrigger("Idle");
                    _navMeshAgent.isStopped = false;
                    state = NodeState.Failure;
                    return state;
                }
                state = NodeState.Success;
                return state;
            }
            
            object t = GetData("stun");
            if (t == null)
            {
                state = NodeState.Failure;
                return state;
            }

            bool stun = (bool)t;
            if (stun)
            {
                stunCounter = 0f;
                isStunned = true;
                _animator.SetTrigger("Stun");
                _animator.SetFloat("AttackDir", -1f);
                _navMeshAgent.isStopped = true;
            }

            state = stun ? NodeState.Success : NodeState.Failure;
            return state;
        }

        public void setStunStatus()
        {
            if (_player.isBlocking)
            {
                if (_animator.GetCurrentAnimatorStateInfo(0).IsTag("leftAttack"))
                {
                    //play left block sound
                }
                else
                {
                    //play right block sound
                }
                parent.SetData("stun", true);
                
            }
            //damage player otherwise
        }

    }
}