using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]

public class RobotArmProjection_Lerped : MonoBehaviour
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
        leftArm.SlowedMap();
        rightArm.SlowedMap();
    }
}
