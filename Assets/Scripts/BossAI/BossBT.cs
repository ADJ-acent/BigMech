using System.Collections.Generic;
using BehaviorTree;
using UnityEngine;

namespace BossAI
{
    public class BossBT : BTree
    {
        public Transform mechTransform;
        protected override Node SetupTree()
        {
            return new Node();
        }
    }

}
