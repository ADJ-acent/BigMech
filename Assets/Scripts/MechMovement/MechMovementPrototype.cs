using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MechMovementPrototype : MonoBehaviour
{
    public Transform vrHeadTransform;

    [Header("Robot")]
    public Transform robotRigTransform;
    public Transform robotParentTransform;
    // limit of angle to the y axis, if less than this, don't rotate according to the headset
    public float angleLimit;
    // Max Angle to rotate per tick
    public float rotationMaxAngle;
    public InputActionReference rightMove;
    public float speed;
    public bool pureConstantMode;
    public bool yWaveMode;
    [Header("Wwise Events")]
    public AK.Wwise.Event mechFootSteps;
    private bool isMoving = false;
    private float startY;

    
    // Start is called before the first frame update
    void Start()
    {
        startY = robotParentTransform.position.y;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Quaternion headsetQuaternion = vrHeadTransform.rotation;
        Vector3 headsetVector3 = headsetQuaternion.eulerAngles;
        //prevent weird head movement when close to purely up or down
        if (Vector3.Angle(Vector3.down, vrHeadTransform.forward) > angleLimit && 
            Vector3.Angle(Vector3.up, vrHeadTransform.forward) > angleLimit)
        {
            robotRigTransform.rotation = Quaternion.RotateTowards(robotRigTransform.rotation, 
                Quaternion.Euler(0f, headsetVector3.y,0), rotationMaxAngle);
            Vector3 oldPos = robotParentTransform.position;
            float rightVal;
            float forwardVal;
            if (pureConstantMode)
            {
                rightVal = rightMove.action.ReadValue<Vector2>().normalized.x;
                forwardVal = rightMove.action.ReadValue<Vector2>().normalized.y;
            }
            else
            {
                rightVal = rightMove.action.ReadValue<Vector2>().x;
                forwardVal = rightMove.action.ReadValue<Vector2>().y;
            }

            Vector3 movementDir = forwardVal * robotRigTransform.forward + rightVal * robotRigTransform.right;
            //play the mech foot step sound 
            if (!isMoving && (rightVal != 0 || forwardVal != 0))
            {
                StartCoroutine(PlayFootstepSounds());
                isMoving = true;
            }
        
            robotParentTransform.position = oldPos + (movementDir.normalized)*speed;
            if (isMoving&& yWaveMode)
            {
                robotParentTransform.position = new Vector3(robotParentTransform.position.x, startY + Mathf.Sin(Time.time*6.28f)/2, robotParentTransform.position.z);
            }
        }
    }

    private IEnumerator PlayFootstepSounds()
    {
        isMoving = true;

        while (true)
        {
            mechFootSteps.Post(gameObject);

            // Adjust the delay for footstep sound (you can change this value)
            yield return new WaitForSeconds(1f);
            float rightVal = rightMove.action.ReadValue<Vector2>().x;
            float forwardVal = rightMove.action.ReadValue<Vector2>().y;
            // Check if the joystick is released
            if (rightVal == 0 && forwardVal == 0)
            {
                isMoving = false;
                yield break; // Exit the coroutine
            }
        }
    }

}
