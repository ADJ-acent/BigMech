using System.Collections;
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
        private CheckStunStatus _stunNode;
        private TaskAttackMech _attackMechNode;
        protected override Node SetupTree() 
        {
            Transform t = transform;
            _stunNode = new CheckStunStatus(t, playerController);
            return new Selector(new List<Node> {
                new CheckAliveStatus(t),
                new CheckHitStatus(t),
                _stunNode, 
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

        public void CrabStartAttack()
        {
            crabBossUI.blockSignOn = true;
            crabBossUI.attackSignOn = false;
        }
        public void CrabAttack()
        {
            _stunNode.setStunStatus();
            crabBossUI.blockCheckDone = true;
            crabBossUI.blockCheckResult =
                playerController.isBlocking ? crabBossUI.blockSignGreen : crabBossUI.blockSignRed;
            StartCoroutine(turnOffBlockSign());
        }
        

        private IEnumerator turnOffBlockSign()
        {
            yield return new WaitForSeconds(2);
            crabBossUI.HideBlockSign();
            crabBossUI.blockSignOn = false;
            crabBossUI.blockCheckDone = false;
        }
    }

}
