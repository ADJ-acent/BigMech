using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;
using UnityEngine.AI;

namespace BossAI
{
    public class TaskWander : Node
    {
        private Transform _transform;
        private Animator _animator;
        private NavMeshAgent _navMeshAgent;
        private const float _waitTime = 3f; // in seconds
        private float _counter = 0f;
        private bool _waiting = true;
         private readonly float _wanderDistance;

        public TaskWander(Transform transform, float wanderDistance)
        {
            _transform = transform;
            _animator = transform.GetComponent<Animator>();
            _navMeshAgent = transform.GetComponent<NavMeshAgent>();
            _wanderDistance = wanderDistance;
        }

        public override NodeState Evaluate()
        {
            if (_waiting)
            {
                _counter += Time.deltaTime;
                if (_counter >= _waitTime)
                {
                    _waiting = false;
                    _animator.SetTrigger("Idle");
                    _counter = 0;
                    // go in a random direction
                    Vector2 randomDir = Random.insideUnitCircle * _wanderDistance;
                    NavMeshHit hit;
                    if (NavMesh.SamplePosition(new Vector3(randomDir.x, 0, randomDir.y) + _transform.position,
                            out hit, 10.0f, NavMesh.AllAreas))
                    {
                        _navMeshAgent.SetDestination(hit.position);
                    }
                    
                }
            }
            else
            {
                // Check if agent reached the destination
                if (!_navMeshAgent.pathPending)
                {
                    if (_navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance)
                    {
                        if (!_navMeshAgent.hasPath || _navMeshAgent.velocity.sqrMagnitude == 0f)
                        {
                            _waiting = true;
                            _animator.SetTrigger("Dance");
                            _counter = 0;
                        }
                    }
                }
            }


            state = NodeState.Running;
            return state;
        }

    }
}