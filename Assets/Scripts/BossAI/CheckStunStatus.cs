using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;
using UnityEngine.AI;

namespace BossAI
{
    public class CheckStunStatus : Node
    {
        public override NodeState Evaluate()
        {
             object t = GetData("stun");
             if (t == null)
             {
                state = NodeState.Success;
                return state;
             }

             bool stun = (bool)t;
             state = stun ? NodeState.Failure : NodeState.Success;
             return state;
        }

    }
}