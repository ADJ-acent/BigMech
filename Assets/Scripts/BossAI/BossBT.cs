using System.Collections.Generic;
using BehaviorTree;
using RayFire;
using UnityEngine;

namespace BossAI
{
    public class BossBT : BTree
    {

        public Transform mechTransform;
        public Transform[] buildingTransforms;
        public RayfireActivator left;
        public RayfireActivator right;
        public float attackRange = 5f;
        protected override Node SetupTree()
        {
            Transform t = transform;
            return new Selector(new List<Node> {
                new Sequence(new List<Node>
                {
                    new CheckBuildingInAttackRange(t, attackRange),
                    new TaskAttackBuilding(t, left, right)
                }),
                
                new Sequence(new List<Node>
            {
                new CheckRemainBuilding(t, buildingTransforms),
                new TaskGoToBuilding(t),
            }), 
            new TaskWander(t,50f)});
        }
    }

}
