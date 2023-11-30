using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

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

    public int damage;
    // event delegate to notify hitting
    public delegate void Hit(Vector3 dir, int damage);
    public static event Hit hit;
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
        leftArm.isLeft = true;
        rightArm.isLeft = false;
        AudioManager.Instance.startArmRampUp();
    }

    // Update is called once per frame
    private void FixedUpdate()
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

    public void hitMech(bool isLeft)
    {
        Vector3 lastMove;
        if (isLeft)
        {
            lastMove = leftArm.getMovementInfo();
        }
        else
        {
            lastMove = rightArm.getMovementInfo();
        }
        if (lastMove.magnitude >= 0.5f) hit?.Invoke(lastMove, damage);
    }
}
