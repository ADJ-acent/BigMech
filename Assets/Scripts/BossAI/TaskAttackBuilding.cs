using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;
using UnityEngine.AI;

namespace BossAI
{
    public class TaskAttackBuilding : Node
    {
        private Transform _transform;
        private readonly Animator _animator;
        private float _attackCounter;
        private float _attackTime = 4f;
        private uint curCount = 0;
        private const uint maxCount = 2;
        private bool haveAttacked = false;

        public TaskAttackBuilding(Transform transform)
        {
            _transform = transform;
            _animator = transform.GetComponent<Animator>();
        }
        
        public override NodeState Evaluate()
        {
            
            Transform target = (Transform)GetData("target");
            if (!haveAttacked)
            {
                Debug.Log(0);
                _animator.SetTrigger("Attack");
                _animator.SetInteger("AttackNum", Random.Range(0,5));
                haveAttacked = true;
                state = NodeState.Running;
                return state;
                
            }
            _attackCounter += Time.deltaTime;
            if (_attackCounter >= _attackTime)
            {
                if (curCount < maxCount)
                {
                    Debug.Log(1);
                    _animator.SetTrigger("Attack");
                    _animator.SetInteger("AttackNum", Random.Range(0,2));
                    _attackCounter = 0;
                    curCount++;
                }
                else
                {
                    ClearData("target");
                    _animator.SetTrigger("Idle");
                    haveAttacked = false;
                    _attackCounter = 0f;
                    curCount = 0;
                }

            }

            state = NodeState.Running;
            return state;
        }

    }
}