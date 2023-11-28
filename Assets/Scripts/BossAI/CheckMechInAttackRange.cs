using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;
using UnityEngine.AI;

namespace BossAI
{
    public class CheckMechInAttackRange : Node
    {
        private Transform _transform;
        private readonly Animator _animator;
        private readonly NavMeshAgent _navMeshAgent;
        private readonly Transform _mechTransform;
        private float _attackRange;

        public CheckMechInAttackRange(Transform transform, float attackRange, Transform mechTransform)
        {
            _transform = transform;
            _animator = transform.GetComponent<Animator>();
            _navMeshAgent = transform.GetComponent<NavMeshAgent>();
            _attackRange = attackRange;
            _mechTransform = mechTransform;
        }
        
        public override NodeState Evaluate()
        {
            if (Vector3.Distance(_transform.position, _mechTransform.position) <= _attackRange)
            { 
                state = NodeState.Success;
                return state;
            }
            state = NodeState.Failure;
            return state;
        }

    }
}