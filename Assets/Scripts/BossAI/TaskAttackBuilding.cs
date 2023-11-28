using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;
using RayFire;
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
        private const uint maxCount = 3;
        private bool haveAttacked = false;
        private int curAttack;
        private const float scaleSize = 1.5f;
        private Vector3 originalLeft;
        private Vector3 originalRight;
        private RayfireActivator _left;
        private RayfireActivator _right;

        public TaskAttackBuilding(Transform transform, RayfireActivator left, RayfireActivator right)
        {
            _transform = transform;
            _animator = transform.GetComponent<Animator>();
            _left = left;
            _right = right;
            originalLeft = _left.boxSize;
            originalRight = _right.boxSize;
        }
        
        public override NodeState Evaluate()
        {
            
            Transform target = (Transform)GetData("target");
            if (!haveAttacked)
            {
                curAttack = Random.Range(0, 3);
                _animator.SetTrigger("Attack");
                _animator.SetInteger("AttackNum", curAttack);
                _animator.SetFloat("AttackWaitTime",0.1f);
                haveAttacked = true;
                state = NodeState.Running;
                curCount++;
                return state;
            }
            _attackCounter += Time.deltaTime;
            if (_attackCounter >= _attackTime)
            {
                if (curCount < maxCount)
                {
                    if (curAttack == maxCount-1)
                    {
                        RayfireConnectivity rc = target.GetComponent<RayfireConnectivity>();
                        rc.enabled = true;
                    }

                    _right.boxSize *= scaleSize;
                    _left.boxSize *= scaleSize;
                    curAttack = Random.Range(0, 3);
                    _animator.SetTrigger("Attack");
                    _animator.SetInteger("AttackNum", curAttack);
                    _animator.SetFloat("AttackWaitTime",0.1f);
                    _attackCounter = 0;
                    curCount++;
                }
                else
                {
                    _right.boxSize = originalRight;
                    _left.boxSize = originalLeft;
                    ClearData("target");
                    _animator.SetTrigger("Idle");
                    haveAttacked = false;
                    _attackCounter = 0f;
                    curCount = 0;
                }

            }

            if (curCount == maxCount && _attackCounter >= _attackTime * 0.75)
            {
                RayfireConnectivity rc = target.GetComponent<RayfireConnectivity>();
                if (rc != null)
                {
                    RFCollapse.StartCollapse(rc);
                }
            }

            state = NodeState.Running;
            return state;
        }
    }
}