using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;
using UnityEngine.AI;

namespace BossAI
{
    public class CheckMechInAttractRange : Node
    {
        public CrabBossUI _crabBossUI;
        private Transform _transform;
        private readonly Animator _animator;
        private readonly NavMeshAgent _navMeshAgent;
        private readonly Transform _mechTransform;
        private float _attractRange;

        public CheckMechInAttractRange(Transform transform, float attractRange, Transform mechTransform, CrabBossUI crabBossUI)
        {
            _crabBossUI = crabBossUI;
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
                _crabBossUI.attackSignOn = true;
                _crabBossUI.blockSignOn = false;
                state = NodeState.Success;
                return state;
            }
            _crabBossUI.attackSignOn = false;
            _crabBossUI.blockSignOn = false;
            state = NodeState.Failure;
            return state;
        }

    }
}