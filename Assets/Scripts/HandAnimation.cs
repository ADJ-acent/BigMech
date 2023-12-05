using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneTemplate;
using UnityEngine;
using UnityEngine.InputSystem;

public class HandAnimation : MonoBehaviour
{
    public InputActionReference inputAction;

    public Animator animator;

    private void Start()
    {
        if (animator == null)
        {
            animator = transform.GetComponent<Animator>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("open", Mathf.Epsilon >= (inputAction.action.ReadValue<float>()));
        
        
    }
}
