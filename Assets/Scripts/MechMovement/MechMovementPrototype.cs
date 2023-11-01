using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechMovementPrototype : MonoBehaviour
{
    public Transform vrHeadTransform;

    public Transform robotRigTransform;

    public float angleLimit;
    // Max Angle to rotate per tick
    public float rotationMaxAngle;
    
    // Start is called before the first frame update
    void Start()
    {
        //Quaternion.Lerp()
    }

    // Update is called once per frame
    void Update()
    {
        Quaternion headsetQuaternion = vrHeadTransform.rotation;
        Vector3 headsetVector3 = headsetQuaternion.eulerAngles;
        //prevent weird head movement when close to purely up or down
        if (Vector3.Angle(Vector3.down, vrHeadTransform.forward) > angleLimit && 
            Vector3.Angle(Vector3.up, vrHeadTransform.forward) > angleLimit)
        {
            robotRigTransform.rotation = Quaternion.RotateTowards(robotRigTransform.rotation, 
                Quaternion.Euler(0f, headsetVector3.y,0), rotationMaxAngle); 
        }
    }
}
