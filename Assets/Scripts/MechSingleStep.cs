using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class MechSingleStep : MonoBehaviour
{
    public Transform vrHeadTransform;

    [Header("Robot")]
    public Transform robotRigTransform;
    public Transform robotParentTransform;
    public float angleLimit;
    public float rotationMaxAngle;
    public InputActionReference rightMove;
    public float speed;

    private bool canMove;
    private float stepDelay;
    private float stepTimer;
    private bool isMoving;

    [Header("Wwise Events")]
    public AK.Wwise.Event mechFootSteps;

    void Start()
    {
        stepDelay = 1.0f;
        stepTimer = 0.0f;
        canMove = true;
        isMoving = false;
    }

    void Update() // Use Update instead of FixedUpdate for input handling
    {
        Quaternion headsetQuaternion = vrHeadTransform.rotation;
        Vector3 headsetVector3 = headsetQuaternion.eulerAngles;

        if (canMove)
        {
            if (Vector3.Angle(Vector3.down, vrHeadTransform.forward) > angleLimit &&
                Vector3.Angle(Vector3.up, vrHeadTransform.forward) > angleLimit)
            {
                robotRigTransform.rotation = Quaternion.RotateTowards(robotRigTransform.rotation,
                    Quaternion.Euler(0f, headsetVector3.y, 0), rotationMaxAngle);
                Vector3 oldPos = robotParentTransform.position;
                float rightVal = rightMove.action.ReadValue<Vector2>().x;
                float forwardVal = rightMove.action.ReadValue<Vector2>().y;
                Vector3 movementDir = forwardVal * robotRigTransform.forward + rightVal * robotParentTransform.right;
                //Debug.Log(vrHeadTransform.forward);
                // Decrease the stepTimer
                stepTimer -= Time.deltaTime;

                if (rightVal != 0 || forwardVal != 0)
                {
                    if (!isMoving)
                    {
                        StartCoroutine(PlayFootstepSound());
                        robotParentTransform.position = oldPos + (movementDir.normalized) * speed;
                        isMoving = true;
                    }

                    // If the timer reaches 0 or below, stop moving
                    if (stepTimer <= 0.0f)
                    {
                        isMoving = false;
                        canMove = false; // Prevent further movement until the joystick is released
                    }
                }
                else
                {
                    isMoving = false;
                }
            }
        }
        else
        {
            // If canMove is false, check if the joystick was released, and reset the timer
            float rightVal = rightMove.action.ReadValue<Vector2>().x;
            float forwardVal = rightMove.action.ReadValue<Vector2>().y;

            if (rightVal == 0 && forwardVal == 0)
            {
                stepTimer = stepDelay; // Reset the timer
                canMove = true; // Allow movement initiation again
            }
        }
    }

    private IEnumerator PlayFootstepSound()
    {
        mechFootSteps.Post(gameObject);
        yield return new WaitForSeconds(0.5f); // Adjust the delay for footstep sound
    }
}
