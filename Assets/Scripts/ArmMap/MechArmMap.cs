using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
[System.Serializable]
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
    public float yAngleAdjustment;
    private Vector3 _lastDirectionVector = Vector3.zero;
    private const float startingMomentumSpeed = 0.2f;
    private float currentSpeed = startingMomentumSpeed;
    private float lastStayStillTime = 0f;
    public void Map()
    {
        Vector3 vectorToController = controllerTransform.position - headsetTransform.position;
        float distanceToController = vectorToController.magnitude;
        Vector3 normalizedToController = vectorToController.normalized;
        Vector3 newPositionOffset = normalizedToController * 
            Mathf.Lerp(mechMin,mechMax,distanceToController/armLength);
        
        targetTransform.position = mechPivotTransform.position + (Quaternion.AngleAxis(yAngleAdjustment, Vector3.down) * newPositionOffset);
        targetTransform.rotation = controllerTransform.rotation;
    }
    public void SlowedMap()
    {
        Vector3 vectorToController = controllerTransform.position - headsetTransform.position;
        float distanceToController = vectorToController.magnitude;
        Vector3 normalizedToController = vectorToController.normalized;
        
        //rotate the target position towards the front of the player so it is more intuitive to control
        Vector3 targetPosition = mechPivotTransform.position + Quaternion.AngleAxis(yAngleAdjustment, Vector3.down) *
            (normalizedToController * Mathf.Lerp(mechMin, mechMax, distanceToController / armLength));
        Vector3 newPosition = Vector3.Lerp(targetTransform.position, targetPosition,
            armVelocity /(targetPosition - targetTransform.position).magnitude);
        targetTransform.position = newPosition;
        targetTransform.rotation = controllerTransform.rotation;
    }

    public void MomentumMap()
    {
        Vector3 vectorToController = controllerTransform.position - headsetTransform.position;
        float distanceToController = vectorToController.magnitude;
        Vector3 normalizedToController = vectorToController.normalized;
        
        //rotate the target position towards the front of the player so it is more intuitive to control
        Vector3 targetPosition = mechPivotTransform.position + Quaternion.AngleAxis(yAngleAdjustment, Vector3.down) *
            (normalizedToController * Mathf.Lerp(mechMin, mechMax, distanceToController / armLength));
        
        //find the displacement between current position and previous
        Vector3 directionVector = targetPosition - targetTransform.position;
        float dot = Vector3.Dot(directionVector, _lastDirectionVector);
        if (dot > 0)
        {
            if (directionVector.magnitude > 1)
            {
                currentSpeed += 0.03f;
            }
            else
            {
                if (lastStayStillTime == 0f) lastStayStillTime = Time.time;
                else if (Time.time - lastStayStillTime >= .5f)
                {
                    lastStayStillTime = 0f;
                    currentSpeed = startingMomentumSpeed;
                }
            }
        }
        else
        {
            currentSpeed = startingMomentumSpeed;
            lastStayStillTime = 0f;
        }

        _lastDirectionVector = directionVector;
        Vector3 newPosition = Vector3.Lerp(targetTransform.position, targetPosition,
            currentSpeed /(targetPosition - targetTransform.position).magnitude);
        targetTransform.position = newPosition;
        targetTransform.rotation = controllerTransform.rotation;
    }
}