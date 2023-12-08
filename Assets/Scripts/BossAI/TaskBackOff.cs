using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;
using UnityEngine.AI;
using UnityEngine.XR.Interaction.Toolkit.Inputs;

namespace BossAI
{
    public class TaskBackOff : Node
    {
        private Transform _transform;
        private readonly Animator _animator;
        private readonly NavMeshAgent _navMeshAgent;
        private float _personalSpace;
        private Transform _mechTransform;

        public TaskBackOff(Transform transform, Transform mechTransform, float personalSpace)
        {
            _transform = transform;
            _animator = transform.GetComponent<Animator>();
            _navMeshAgent = transform.GetComponent<NavMeshAgent>();
            _personalSpace = personalSpace;
            _mechTransform = mechTransform;
        }
        
        public override NodeState Evaluate()
        {
            Vector3 targetPosition = (_transform.position - _mechTransform.position).normalized *_personalSpace;
            _navMeshAgent.SetDestination(new Vector3(targetPosition.x, _transform.position.y, targetPosition.z));

            state = NodeState.Running;
            return state;
        }

    }
}