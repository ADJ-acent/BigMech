using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;
using UnityEngine.AI;

namespace BossAI
{
    public class TaskGoToMech : Node
    {
        private Transform _transform;
        private readonly Animator _animator;
        private readonly NavMeshAgent _navMeshAgent;
        private bool _hasMoved = false;
        private Transform _mechTransform;

        public TaskGoToMech(Transform transform, Transform mechTransform)
        {
            _transform = transform;
            _animator = transform.GetComponent<Animator>();
            _navMeshAgent = transform.GetComponent<NavMeshAgent>();
            _mechTransform = mechTransform;
        }
        
        public override NodeState Evaluate()
        {
            if (!_hasMoved)
            {
                _navMeshAgent.SetDestination(_mechTransform.position);
                _animator.SetTrigger("Idle");
                _hasMoved = true;
                return NodeState.Running;
            }
            // Check if agent reached the destination
            if (!_navMeshAgent.pathPending)
            {
                    if (!_navMeshAgent.hasPath || _navMeshAgent.velocity.sqrMagnitude == 0f)
                    {
                        _navMeshAgent.SetDestination(_mechTransform.position);
                        _animator.SetTrigger("Idle");
                        
                    }

            }

            state = NodeState.Running;
            return state;
        }

    }
}