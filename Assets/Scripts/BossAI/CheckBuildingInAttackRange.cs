using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;
using UnityEngine.AI;

namespace BossAI
{
    public class CheckBuildingInAttackRange : Node
    {
        private Transform _transform;
        private readonly Animator _animator;
        private readonly NavMeshAgent _navMeshAgent;
        private float _attackRange;

        public CheckBuildingInAttackRange(Transform transform, float attackRange)
        {
            _transform = transform;
            _animator = transform.GetComponent<Animator>();
            _navMeshAgent = transform.GetComponent<NavMeshAgent>();
            _attackRange = attackRange;
        }
        
        public override NodeState Evaluate()
        {
            object t = GetData("target");
            if (t == null)
            {
                state = NodeState.Failure;
                return state;
            }

            Transform target = (Transform)t;
            Vector3 buildingPosition = target.position;
            if (Vector3.Distance(_transform.position, buildingPosition) <= _attackRange)
            {
                _transform.LookAt(new Vector3(buildingPosition.x, _transform.position.y, buildingPosition.z), Vector3.up);
                state = NodeState.Success;
                return state;
            }
            state = NodeState.Failure;
            return state;
        }

    }
}