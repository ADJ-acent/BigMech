using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;
using UnityEngine.AI;

namespace BossAI
{
    public class CheckMechInAttractRange : Node
    {
        private Transform _transform;
        private readonly Animator _animator;
        private readonly NavMeshAgent _navMeshAgent;
        private readonly Transform _mechTransform;
        private float _attractRange;

        public CheckMechInAttractRange(Transform transform, float attractRange, Transform mechTransform)
        {
            _transform = transform;
            _animator = transform.GetComponent<Animator>();
            _navMeshAgent = transform.GetComponent<NavMeshAgent>();
            _attractRange = attractRange;
            _mechTransform = mechTransform;
        }
        
        public override NodeState Evaluate()
        {
            if (Vector3.Distance(_transform.position, _mechTransform.position) <= _attractRange)
            { 
                state = NodeState.Success;
                return state;
            }
            state = NodeState.Failure;
            return state;
        }

    }
}