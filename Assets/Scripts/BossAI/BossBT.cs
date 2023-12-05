using System.Collections.Generic;
using BehaviorTree;
using RayFire;
using UnityEngine;

namespace BossAI
{
    public class BossBT : BTree
    {
        public PlayerController playerController;
        public CrabBossUI crabBossUI;

        public Transform mechTransform;
        public float offsetFromMech;
        public float mechAttractRange;
        public Transform[] buildingTransforms;
        public RayfireActivator leftClaw;
        public RayfireActivator rightClaw;
        public float attackRange = 5f;
        protected override Node SetupTree() 
        {
            Transform t = transform;
            return new Selector(new List<Node> {
                new CheckAliveStatus(t),
                new CheckHitStatus(t),
                new CheckStunStatus(t), 
                new Sequence(new List<Node>
                {
                    new CheckMechInAttackRange(t, offsetFromMech, mechTransform, crabBossUI),
                    new TaskAttackMech(t, mechTransform, playerController, crabBossUI)
                }),
                new Sequence(new List<Node>
                {
                    new CheckMechInAttractRange(t, mechAttractRange, mechTransform, crabBossUI),
                    new TaskGoToMech(t, mechTransform, offsetFromMech)
                }),
                new Sequence(new List<Node>
                {
                    new CheckBuildingInAttackRange(t, attackRange),
                    new TaskAttackBuilding(t, leftClaw, rightClaw)
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
