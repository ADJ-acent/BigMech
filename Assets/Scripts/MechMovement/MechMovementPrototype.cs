using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechMovementPrototype : MonoBehaviour
{
    public Transform vrHeadTransform;

    public Transform robotRigTransform;

    public float angleLimit;

    public float rotationMaxSpeed;
    
    private Vector3 rotateVelocity = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        //Quaternion.Lerp()
    }

    // Update is called once per frame
    void Update()
    {
        Quaternion headsetQuaternion = vrHeadTransform.localRotation;
        Vector3 headsetVector3 = headsetQuaternion.eulerAngles;
        //prevent weird head movement when close to purely up or down
        if (Vector3.Angle(Vector3.down, headsetVector3) > angleLimit && 
            Vector3.Angle(Vector3.up, headsetVector3) > angleLimit)
        {
            //project the headsetVector to the xz plane
            Vector3 projectedVector = Vector3.ProjectOnPlane(headsetVector3, Vector3.up);
            Vector3 robotVector = robotRigTransform.eulerAngles;
            robotRigTransform.localRotation = Quaternion.FromToRotation(robotVector, projectedVector);
        }
    }
    
    //adapted from https://forum.unity.com/threads/quaternion-to-remove-pitch.822768/
    public static Quaternion GetXAxisRotation( Quaternion quaternion)
    {
        float a = Mathf.Sqrt((quaternion.w * quaternion.w) + (quaternion.x * quaternion.x));
        return new Quaternion(x: quaternion.x, y: 0, z: 0, w: quaternion.w / a);
 
    }
 
    public static Quaternion GetYAxisRotation( Quaternion quaternion)
    {
        float a = Mathf.Sqrt((quaternion.w * quaternion.w) + (quaternion.y * quaternion.y));
        return new Quaternion (x: 0, y: quaternion.y, z: 0, w: quaternion.w / a);
 
    }
 
    public static Quaternion GetZAxisRotation( Quaternion quaternion)
    {
        float a = Mathf.Sqrt((quaternion.w * quaternion.w) + (quaternion.z * quaternion.z));
        return new Quaternion(x: 0, y: 0, z: quaternion.z, w: quaternion.w / a);
    }
}
