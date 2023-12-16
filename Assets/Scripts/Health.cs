using System;
using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour
{
    public PlayerController playerController;
    public CrabBossUI crabBossUI;
    public int maxHealth = 100;
    public int curHealth;

    private void Start()
    {
        curHealth = maxHealth;
        playerController.crabMaxHealth = maxHealth;
    }

    /* returns true if the entity is dead, false otherwise*/
    public bool DealDamage(int delta)
    {
        StartCoroutine(Wait());

        curHealth = Math.Clamp(curHealth - delta, 0, maxHealth);
        playerController.healthLeft -= delta;
        if (curHealth == 0) 
        {
            Destroy(crabBossUI.attackSignBlue);
            Destroy(crabBossUI.attackSignGreen);
            crabBossUI.attackSignOn = false;
            crabBossUI.blockSignOn = false;
            return true;
        }
        return false;
    }

    public IEnumerator Wait()
    {
        crabBossUI.successfulAttack = true;
        yield return new WaitForSeconds(1);
        crabBossUI.successfulAttack = false;
    }
}
