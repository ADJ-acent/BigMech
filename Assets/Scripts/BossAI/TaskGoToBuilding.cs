using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;
using UnityEngine.AI;

namespace BossAI
{
    public class TaskGoToBuilding : Node
    {
        private Transform _transform;
        private readonly Animator _animator;
        private readonly NavMeshAgent _navMeshAgent;
        private bool _hasMoved = false;

        public TaskGoToBuilding(Transform transform)
        {
            _transform = transform;
            _animator = transform.GetComponent<Animator>();
            _navMeshAgent = transform.GetComponent<NavMeshAgent>();
        }
        
        public override NodeState Evaluate()
        {
            Transform target = (Transform)GetData("target");
            
            if (!_hasMoved)
            {
                Vector3 targetPOS = new Vector3(target.position.x, _transform.position.y, target.position.z);
                _navMeshAgent.SetDestination(targetPOS);
                _animator.SetTrigger("Idle");
                _hasMoved = true;
                return NodeState.Running;
            }
            // Check if agent reached the destination
            if (!_navMeshAgent.pathPending)
            {
                    if (!_navMeshAgent.hasPath || _navMeshAgent.velocity.sqrMagnitude == 0f)
                    {
                        Debug.Log(target);
                        Debug.Log(target.position);
                        Vector3 targetPOS = new Vector3(target.position.x, _transform.position.y, target.position.z);
                        Debug.Log(_navMeshAgent.SetDestination(targetPOS));
                        
                    }

            }

            state = NodeState.Running;
            return state;
        }

    }
}