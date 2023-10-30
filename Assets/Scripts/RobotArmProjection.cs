using System.Collections;
using System.Collections.Generic;
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
    public void Map()
    {
        Vector3 vectorToController = controllerTransform.position - headsetTransform.position;
        float distanceToController = vectorToController.magnitude;
        Vector3 normalizedToController = vectorToController.normalized;
        targetTransform.position = mechPivotTransform.position + normalizedToController * Mathf.Lerp(mechMin,mechMax,distanceToController/armLength);
        targetTransform.rotation = controllerTransform.rotation;
    }
}

public class RobotArmProjection : MonoBehaviour
{
    public MechArmMap leftArm;
    public MechArmMap rightArm;
    [SerializeField] private Transform mechPivot;
    [SerializeField] private Transform headSetTransform;
    public float armLength = .65f;
    public float mechMinReach;
    public float mechMaxReach;
    void Start()
    {
        leftArm.mechPivotTransform = mechPivot;
        rightArm.mechPivotTransform = mechPivot;
        leftArm.headsetTransform = headSetTransform;
        rightArm.headsetTransform = headSetTransform;
        leftArm.armLength = armLength;
        rightArm.armLength = armLength;
        leftArm.mechMax = mechMaxReach;
        rightArm.mechMax = mechMaxReach;
        leftArm.mechMin = mechMinReach;
        rightArm.mechMin = mechMinReach;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        leftArm.Map();
        rightArm.Map();
    }
}
