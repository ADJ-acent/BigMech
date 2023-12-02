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

    public bool isBlocking;
    public bool crabIsAttacking;
    public bool successfulBlocking;
    public bool unsuccessfulBlocking;
    public Transform crabTransform;
    public Animator _animator;
    public float crabDamage;

    // Start is called before the first frame update
    void Start()
    { 
        healthRight = maxHealth;
        healthLeft = maxHealth;

        _animator = crabTransform.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        HealthBarFill();
        BlockingCalc();
    }

    private void HealthBarFill()
    {
        healthBarRight.fillAmount = Mathf.Clamp(healthRight / maxHealth, 0, 1);
        healthBarLeft.fillAmount = Mathf.Clamp(healthLeft / maxHealth, 0, 1);
    }

    private void BlockingCalc()
    {
        Vector3 meToCrab = crabTransform.position - transform.position;
        meToCrab.y = 0f;
        if (Physics.Raycast(transform.position, meToCrab, 3f))
        {
            isBlocking = true;
        }
        else isBlocking = false;

        AnimatorStateInfo crabStateInfo = _animator.GetCurrentAnimatorStateInfo(0);   
        if (crabStateInfo.IsTag("attack"))
        {
            successfulBlocking = isBlocking;
            unsuccessfulBlocking = !isBlocking;
        }
        else 
        {
            successfulBlocking = false;
            unsuccessfulBlocking = false;
        }
    }

    public void TakeDamage()
    {
        if (unsuccessfulBlocking) healthRight -= crabDamage;
    }
}
