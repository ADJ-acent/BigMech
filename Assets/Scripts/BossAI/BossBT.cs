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
        public float attackRange;
        private CheckStunStatus _stunNode;
        public float damage = 10f;
        bool playerInAttackRange;
        public GameObject endScreen;
        protected override Node SetupTree() 
        {
            Transform t = transform;
            _stunNode = new CheckStunStatus(t, playerController);
            return new Selector(new List<Node> {
                new CheckAliveStatus(t, endScreen),
                new CheckHitStatus(t),
                _stunNode, 
                new Sequence(new List<Node>
                {
                    new CheckMechTooClose(t, mechTransform, attackRange-5),
                    new TaskBackOff(t, mechTransform, attackRange-5)
                        
                }),
                new Sequence(new List<Node>
                {
                    new CheckMechInAttackRange(t, offsetFromMech, mechTransform, crabBossUI),
                    new TaskAttackMech(t, mechTransform, playerController, crabBossUI, damage)
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
            AudioManager.Instance.playCrabRoar();
        }
        public void CrabAttack()
        {
            // TODO: replace with actual value
            bool playerInAttackRange = Vector3.Distance(transform.position, mechTransform.position) <= attackRange + 10f;
            if (!playerController.isBlocking && playerInAttackRange) playerController.TakeDamage(damage);
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
            // crabBossUI.blockSignOn = false;
            crabBossUI.blockCheckDone = false;
        }

        public void BlockSuccessCalc()
        {
            playerController.BlockSuccessCalc();
        }

        public void ToggleOnOff()
        {
            crabBossUI.attackSignOn = false;
            crabBossUI.blockSignOn = true;
        }

        public void ToggleOffOn()
        {
            crabBossUI.blockSignOn = false;
            crabBossUI.attackSignOn = true;
        }
    }
}
