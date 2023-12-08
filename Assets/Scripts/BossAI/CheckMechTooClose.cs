using UnityEngine;

using BehaviorTree;
using UnityEngine.AI;

namespace BossAI
{
    public class CheckMechTooClose : Node
    {
        private Transform _transform;
        private readonly Animator _animator;
        private Transform _mechTransform;
        private float _personalSpace;
        // least seconds before crab can back off again
        private const float backoffTimer = 2f;
        // max seconds crab can be backing off for
        private const float backOffCounter = 1f;
        private float curBackOffCounter = 0f;
        private float lastBackOff;
        private bool backing = false;
        private NavMeshAgent _navMeshAgent;

        public CheckMechTooClose(Transform transform, Transform mechTransform, float personalSpace)
        {
            _transform = transform;
            _animator = transform.GetComponent<Animator>();
            _navMeshAgent = transform.GetComponent<NavMeshAgent>();
            _mechTransform = mechTransform;
            _personalSpace = personalSpace;
        }
        
        public override NodeState Evaluate()
        {
            object stun = GetData("stun");
            object attacking = GetData("Attack");
            if ((stun != null && (bool)stun))
            {
                return NodeState.Failure;
            }
            if ((new Vector3(_mechTransform.position.x, _transform.position.y, _mechTransform.position.z) - 
                 _transform.position).magnitude < _personalSpace)
            {
                // haven't backed off in a while
                if (Time.time - lastBackOff > backoffTimer)
                {
                    curBackOffCounter = 0f;
                }
                // is currently backing off
                else if (backing && Time.deltaTime + curBackOffCounter < backOffCounter)
                {
                    curBackOffCounter += Time.deltaTime;
                }
                else
                {
                    _navMeshAgent.updateRotation = true;
                    if (backing) lastBackOff = Time.time;
                    backing = false;
                    curBackOffCounter = 0f;
                    _navMeshAgent.ResetPath();
                    return NodeState.Failure;
                }

                _navMeshAgent.updateRotation = false;
                backing = true;
                lastBackOff = Time.time;
                state = NodeState.Running;
                return state;
            }
            _navMeshAgent.updateRotation = true;
            backing = false;
            return NodeState.Failure;
            
        }
    }
}
