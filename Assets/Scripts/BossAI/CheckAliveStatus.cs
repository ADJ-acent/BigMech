
using UnityEngine;

using BehaviorTree;
using Unity.AI.Navigation.Samples;
using Unity.VisualScripting;
using UnityEngine.AI;

namespace BossAI
{
    public class CheckAliveStatus : Node
    {
        private Animator _animator;
        private Health _health;
        private Transform _transform;
        private NavMeshAgent _navMeshAgent;
        
        public CheckAliveStatus (Transform transform)
        {
            _transform = transform;
            _animator = transform.GetComponent<Animator>();
        }
        public override NodeState Evaluate()
        {
            object t = GetData("dead");
            
            if (t != null && (bool)t)
            {
                AudioManager.Instance.playVictory();
                Object.Destroy(_transform.gameObject);
                return NodeState.Success;
            }
    

            return NodeState.Failure;
        }
        
    }
}