using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float healthRight;
    public float maxHealth;
    public Image healthBarRight;
    public float healthLeft;
    public Image healthBarLeft;

    // Start is called before the first frame update
    void Start()
    { 
        healthRight = maxHealth;
        healthLeft = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        healthBarRight.fillAmount = Mathf.Clamp(healthRight / maxHealth, 0, 1);
        healthBarLeft.fillAmount = Mathf.Clamp(healthLeft / maxHealth, 0, 1);
    }
}
