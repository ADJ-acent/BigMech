using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechArmMap
{
    public Transform controllerTransform;
    public Transform targetTransform;
    [HideInInspector] public Transform mechPivotTransform;
    [HideInInspector] public Transform headsetTransform;
    [HideInInspector] public float armLength = .65f;
    [HideInInspector] public float mechMin = 6f;
    [HideInInspector] public float mechMax = 20f;
    public float armVelocity;
    public void Map()
    {
        Vector3 vectorToController = controllerTransform.position - headsetTransform.position;
        float distanceToController = vectorToController.magnitude;
        Vector3 normalizedToController = vectorToController.normalized;
        targetTransform.position = mechPivotTransform.position + normalizedToController * Mathf.Lerp(mechMin,mechMax,distanceToController/armLength);
        targetTransform.rotation = controllerTransform.rotation;
    }
    public void SlowedMap()
    {
        Vector3 vectorToController = controllerTransform.position - headsetTransform.position;
        float distanceToController = vectorToController.magnitude;
        Vector3 normalizedToController = vectorToController.normalized;
        Vector3 targetPosition = mechPivotTransform.position + normalizedToController * Mathf.Lerp(mechMin,mechMax,distanceToController/armLength);
        targetTransform.position = Vector3.Lerp(targetTransform.position, targetPosition,
            armVelocity /(targetPosition - targetTransform.position).magnitude);
        targetTransform.rotation = controllerTransform.rotation;
    }
}