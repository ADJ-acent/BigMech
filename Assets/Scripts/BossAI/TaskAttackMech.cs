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
        public PlayerController _playerController;
        public CrabBossUI _crabBossUI;

        public TaskAttackMech(Transform transform, Transform mechTransform, PlayerController playerController, CrabBossUI crabBossUI)
        {
            _crabBossUI = crabBossUI;
            _playerController = playerController;
            _transform = transform;
            _animator = transform.GetComponent<Animator>();
            _mechTransform = mechTransform;
        }
        
        public override NodeState Evaluate()
        {
            _attackCounter += Time.deltaTime;
            if (!haveAttacked || _attackCounter >= _attackTime)
            {
                    curAttack = Random.Range(0, 2);
                    _animator.SetTrigger("Attack");
                    _animator.SetInteger("AttackNum", curAttack);
                    _animator.SetFloat("AttackWaitTime",1f);

                    // TODO: may need to move code elsewhere
                    _playerController.TakeDamage();
                    _crabBossUI.blockSignOn = false;
                    _crabBossUI.attackSignOn = true;

                    _attackCounter = 0;
                    haveAttacked = true;
                    parent.parent.SetData("Attack", true);
            }

            state = NodeState.Running;
            return state;
        }
    }
}