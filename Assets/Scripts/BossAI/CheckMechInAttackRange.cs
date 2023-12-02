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
        private readonly float _offset;
        public CrabBossUI _crabBossUI;

        public CheckMechInAttackRange(Transform transform, float offset, Transform mechTransform, CrabBossUI crabBossUI)
        {
            _crabBossUI = crabBossUI;
            _transform = transform;
            _animator = transform.GetComponent<Animator>();
            _navMeshAgent = transform.GetComponent<NavMeshAgent>();
            _offset = offset;
            _mechTransform = mechTransform;
        }
        
        public override NodeState Evaluate()
        {
            if (Vector3.Distance(_transform.position, _mechTransform.position) <= _offset + 20)
            {
                Vector3 mechPos = _mechTransform.position;
                _transform.LookAt(new Vector3(mechPos.x, _transform.position.y, mechPos.z));
                _crabBossUI.blockSignOn = true;
                _crabBossUI.attackSignOn = false;

                state = NodeState.Success;
                return state;
            }
            state = NodeState.Failure;
            return state;
        }

    }
}