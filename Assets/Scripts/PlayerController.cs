using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float crabMaxHealth;
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
    public float damage;
    public bool isPlaying;

    // Start is called before the first frame update
    void Start()
    { 
        healthRight = maxHealth;
        healthLeft = maxHealth;

        _animator = crabTransform.GetComponent<Animator>();
        isPlaying = false;
    }

    // Update is called once per frame
    void Update()
    {
        HealthBarFill();
        if (crabTransform != null) BlockingCalc();
    }

    private void HealthBarFill()
    {
        healthBarRight.fillAmount = Mathf.Clamp(healthRight / maxHealth, 0, 1);
        healthBarLeft.fillAmount = Mathf.Clamp(healthLeft / crabMaxHealth, 0, 1);
    }

    private void BlockingCalc()
    {
        Vector3 meToCrab = crabTransform.position - transform.position;
        meToCrab.y = 0f;
        LayerMask mask = LayerMask.GetMask("Player");
        isBlocking = Physics.Raycast(transform.position, meToCrab, 10f, mask);
        
        AnimatorStateInfo crabStateInfo = _animator.GetCurrentAnimatorStateInfo(0);   
        if (crabStateInfo.IsTag("leftAttack") || crabStateInfo.IsTag("rightAttack"))
        {
            successfulBlocking = isBlocking;
            unsuccessfulBlocking = !isBlocking;
            
        }
        else 
        {
            successfulBlocking = false;
            unsuccessfulBlocking = false;
        }

        if (successfulBlocking && !isPlaying)
        {
            StartCoroutine(PlayBlockSound());
            isPlaying = successfulBlocking;
        }
    }

    public void TakeDamage(float damage)
    {
       healthRight -= damage;
    }

    private IEnumerator PlayBlockSound()
    {
        AudioManager.Instance.playBigHit();
        yield return new WaitForSeconds(1f);
        isPlaying = false;
            
    }
    
}
