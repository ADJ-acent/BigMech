using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth = 100;
    
    /* returns true if the entity is dead, false otherwise*/
    public bool DealDamage(int delta)
    {
        maxHealth = Math.Clamp(maxHealth - delta, 0, maxHealth);
        if (maxHealth == 0) return true;
        return false;
    }
}
