using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;
using RayFire;
using UnityEngine.AI;

namespace BossAI
{
    public class TaskAttackMech : Node
    {
        private Transform _transform;
        private readonly Animator _animator;
        private float _attackCounter;
        private float _attackTime = 4f;
        private bool haveAttacked = false;
        private int curAttack;
        private Transform _mechTransform;
        // public PlayerController playerController;
        

        public TaskAttackMech(Transform transform, Transform mechTransform)
        {
            _transform = transform;
            _animator = transform.GetComponent<Animator>();
            _mechTransform = mechTransform;
        }
        
        public override NodeState Evaluate()
        {
            if (!haveAttacked)
            {
                curAttack = Random.Range(0, 2);
                _animator.SetTrigger("Attack");
                _animator.SetInteger("AttackNum", curAttack);
                _animator.SetFloat("AttackWaitTime",1f);
                haveAttacked = true;
                state = NodeState.Running;
                parent.parent.SetData("Attack", true);
                return state;
            }
            _attackCounter += Time.deltaTime;
            if (_attackCounter >= _attackTime)
            {
                    curAttack = Random.Range(0, 2);
                    _animator.SetTrigger("Attack");
                    _animator.SetInteger("AttackNum", curAttack);
                    _animator.SetFloat("AttackWaitTime",1f);
                    _attackCounter = 0;
                    parent.parent.SetData("Attack", true);
            }

            state = NodeState.Running;
            return state;
        }
    }
}