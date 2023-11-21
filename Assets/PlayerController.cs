using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float health;
    public float maxHealth;
    public Image healthBar;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.fillAmount = Mathf.Clamp(health / maxHealth, 0, 1);
    }
}
