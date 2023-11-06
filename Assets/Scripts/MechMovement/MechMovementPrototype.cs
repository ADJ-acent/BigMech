using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MechMovementPrototype : MonoBehaviour
{
    public Transform vrHeadTransform;

    public Transform robotRigTransform;
    public Transform robotParentTransform;
    // limit of angle to the y axis, if less than this, don't rotate according to the headset
    public float angleLimit;
    // Max Angle to rotate per tick
    public float rotationMaxAngle;
    public InputActionReference rightMove;
    public float speed;
    
    // Start is called before the first frame update
    void Start()
    {
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
            float rightVal = rightMove.action.ReadValue<Vector2>().x;
            float forwardVal = rightMove.action.ReadValue<Vector2>().y;
            Vector3 movementDir = forwardVal * robotRigTransform.forward +rightVal * robotParentTransform.right;
            robotParentTransform.position = oldPos + (movementDir.normalized)*speed;
        }
    }
}
