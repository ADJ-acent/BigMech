
using System.Collections;
using UnityEngine;

using BehaviorTree;
using UnityEngine.AI;
namespace BossAI
{
    public class CheckAliveStatus : Node
    {
        private Animator _animator;
        private Health _health;
        private Transform _transform;
        private NavMeshAgent _navMeshAgent;
        private bool dying = false;
        private float time = 0f;
        private GameObject _endScreen;
        
        public CheckAliveStatus (Transform transform, GameObject endScreen)
        {
            _transform = transform;
            _animator = transform.GetComponent<Animator>();
            _navMeshAgent = transform.GetComponent<NavMeshAgent>();
            _endScreen = endScreen;
        }
        public override NodeState Evaluate()
        {
            if (dying)
            {
                if (Time.time - time > 5f)
                {
                    Object.Destroy(_transform.gameObject);
                }
                return NodeState.Success;
            }
            
            object t = GetData("dead");
            
            if (t != null && (bool)t)
            {
                _navMeshAgent.isStopped = true;
                AudioManager.Instance.setNoneState();
                AudioManager.Instance.playVictory();
                dying = true;
                time = Time.time;
                _animator.SetTrigger("Dying");
                _endScreen.SetActive(true);
                return NodeState.Success;
            }
    

            return NodeState.Failure;
        }
    }
}