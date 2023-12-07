using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockedStun : MonoBehaviour
{
    public delegate void Blocked();
    public static event Blocked blocked;
    public Animator _animator;

    private void OnTriggerEnter(Collider other)
    {
        /*if (other.CompareTag("Mech"))
        {
            if (_animator.GetCurrentAnimatorStateInfo(0).IsTag("attack")) blocked?.Invoke();
        }*/
    }
    /*for testing stun*/
    [ContextMenu("stun")]
    private void triggerBlocked()
    {
        blocked?.Invoke();
    }
}
