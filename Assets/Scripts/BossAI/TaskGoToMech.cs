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
        private readonly float _offset;

        public TaskGoToMech(Transform transform, Transform mechTransform, float offset)
        {
            _transform = transform;
            _animator = transform.GetComponent<Animator>();
            _navMeshAgent = transform.GetComponent<NavMeshAgent>();
            _mechTransform = mechTransform;
            _offset = offset;
        }
        
        public override NodeState Evaluate()
        {

            Vector3 mechPosition = _mechTransform.position;
            _navMeshAgent.SetDestination(mechPosition + getOffsetFromMech(mechPosition));
            _animator.SetTrigger("Idle");
            _hasMoved = true;
            return NodeState.Running;

        }
        private Vector3 getOffsetFromMech(Vector3 mechPosition)
        {
            
            return (_transform.position - mechPosition).normalized * _offset;

        }
    
    }
    

}