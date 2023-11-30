using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth = 100;
    public int curHealth;

    private void Start()
    {
        curHealth = maxHealth;
    }

    /* returns true if the entity is dead, false otherwise*/
    public bool DealDamage(int delta)
    {
        curHealth = Math.Clamp(curHealth - delta, 0, maxHealth);
        if (curHealth == 0) return true;
        return false;
    }
}
