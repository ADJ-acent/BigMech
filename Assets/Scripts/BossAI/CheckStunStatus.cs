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
        private float stunTime = 2.5f;
        private float stunCounter = 0f;
        private Animator _animator;
        
        public CheckStunStatus (Transform transform)
        {
            BlockedStun.blocked += setStunStatus;
            _animator = transform.GetComponent<Animator>();
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
                    _animator.SetTrigger("Idle");
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
            }

            state = stun ? NodeState.Success : NodeState.Failure;
            return state;
        }

        private void setStunStatus()
        {
            parent.SetData("stun", true);
        }

    }
}