using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
[System.Serializable]


public class RobotArmProjection : MonoBehaviour
{
    [Tooltip("How the arm is mapped to the body, direct maps 1 to 1, slow maps the arm with delayed movement, " +
             "momentum accelerates the arm when moving in the same direction")]
    public MappingType mappingType;
    public MechArmMap leftArm;
    public MechArmMap rightArm;
    [SerializeField] private Transform mechPivot;
    [SerializeField] private Transform headSetTransform;
    public float armLength = .65f;
    public float mechMinReach;
    public float mechMaxReach;
    public enum MappingType
    {
        Direct,
        Slow,
        Momentum
        
    }
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
        switch (mappingType)
        {
            case MappingType.Direct:
                leftArm.Map();
                rightArm.Map();
                break;
            case MappingType.Momentum:
                leftArm.MomentumMap();
                rightArm.MomentumMap();
                break;
            case MappingType.Slow:
                leftArm.SlowedMap();
                rightArm.SlowedMap();
                break;
        }
        
    }
}
