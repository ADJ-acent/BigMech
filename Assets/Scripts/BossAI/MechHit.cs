using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechHit : MonoBehaviour
{
    public delegate void HitByMech(Vector3 direction);
    public static event HitByMech hitByMech;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Crab"))
        {
            hitByMech?.Invoke(Vector3.zero);
        }
    }
}
