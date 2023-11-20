using System.Collections;
using System.Collections.Generic;
using BehaviorTree;
using UnityEngine;
using UnityEngine.AI;

namespace BossAI
{
    public class CheckRemainBuilding : Node
    {
        private Transform _transform;
        private readonly Animator _animator;
        private readonly NavMeshAgent _navMeshAgent;
        private List<Transform> _buildingTransforms = new List<Transform>();

        public CheckRemainBuilding(Transform transform, Transform[] buildingTransforms)
        {
            _transform = transform;
            _animator = transform.GetComponent<Animator>();
            _navMeshAgent = transform.GetComponent<NavMeshAgent>();
            _buildingTransforms.AddRange(buildingTransforms);
        }
        
        public override NodeState Evaluate()
        {
            object t = GetData("target");
            if (t == null)
            {
                if (_buildingTransforms.Count > 0)
                {
                    parent.parent.SetData("target", _buildingTransforms[0].transform);
                    _buildingTransforms.RemoveAt(0);
                    state = NodeState.Success;
                    return state;
                }

                state = NodeState.Failure;
                return state;
            }

            state = NodeState.Success;
            return state;
        }
    }
}