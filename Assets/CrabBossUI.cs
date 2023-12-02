using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrabBossUI : MonoBehaviour
{
    public Canvas middleCanvas;
    public Canvas indicatorCanvas;
    public Image attackSignBlue;
    public Image attackSignGreen;
    public Image blockSignBlue;
    public Image blockSignYellow;
    public Image blockSignRed;
    public Image blockSignGreen;
    public Image warningSignLeft;
    public Image warningSignRight;

    public bool attackSignOn;
    public bool blockSignOn;

    public Transform crabTransform;
    public Transform player;
    public Transform humanTransform;
    public Transform mechTransform;
    private float angle;

    public PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        // TODO: remove this line
        attackSignBlue.enabled = false;
        attackSignGreen.enabled = false;
        blockSignBlue.enabled = false;
        blockSignGreen.enabled = false;
        blockSignYellow.enabled = false;
        blockSignRed.enabled = false;
        warningSignLeft.enabled = false;
        warningSignRight.enabled = false;

        player = GameObject.Find("PlayerController").transform;
        humanTransform = GameObject.Find("VRCharacterIK").transform;
        mechTransform = GameObject.Find("Robotv2").transform; // TODO: change robot name

        angle = 40f;
    }

    // Update is called once per frame
    void Update()
    {
        if (blockSignOn) 
        {
            HideAttackSign();
            BlockSignCalc();
            BlockSignMode();
        }
        else
        {
            HideBlockSign();
            if (attackSignOn) 
            {
                AttackSignCalc();
                AttackSignMode();
            }
            else 
            {
                HideAttackSign();
                Debug.Log("good");
            }
        }
    }

    private void AttackSignMode()
    {

    }

    private void AttackSignCalc()
    {
        Vector3 diff = crabTransform.position - player.position;
        Vector3 projectedVector = new Vector3(diff.x, 0, diff.z);
        float angleToPosition = Vector3.SignedAngle(mechTransform.forward, projectedVector, Vector3.up);

        if ((-1 * angle) <= angleToPosition && angleToPosition <= angle)
        {
            if (angleToPosition < 0) ShowAttackSign(Mathf.Abs(angleToPosition), -1);
            else if (angleToPosition >= 0) ShowAttackSign(Mathf.Abs(angleToPosition), 1);
        }
        else if ((-1 * angle) > angleToPosition) ShowLeftWarningSign();
        else ShowRightWarningSign();
    }

    public void ShowAttackSign(float angle, int wrap)
    {
        Vector3 pos = PositionCalc(angle, wrap);
        attackSignBlue.transform.position = pos;
        attackSignBlue.enabled = true;
    }

    private void BlockSignMode()
    {
        if (blockSignBlue.enabled == true)
        {
            if (playerController.isBlocking)
            {
                blockSignBlue.enabled = false;
                blockSignYellow.enabled = true;
                if (playerController.successfulBlocking)
                {
                    blockSignYellow.enabled = false;
                    blockSignGreen.enabled = true;
                }
            }
            if (playerController.unsuccessfulBlocking)
            {
                blockSignBlue.enabled = false;
                blockSignRed.enabled = true;
            }

        }
    }   

    private void BlockSignCalc()
    {
        Vector3 diff = crabTransform.position - player.position;
        Vector3 projectedVector = new Vector3(diff.x, 0, diff.z);
        float angleToPosition = Vector3.SignedAngle(mechTransform.forward, projectedVector, Vector3.up);

        if ((-1 * angle) <= angleToPosition && angleToPosition <= angle)
        {
            if (angleToPosition < 0) ShowBlockSign(Mathf.Abs(angleToPosition), -1);
            else if (angleToPosition >= 0) ShowBlockSign(Mathf.Abs(angleToPosition), 1);
        }
        else if ((-1 * angle) > angleToPosition) ShowLeftWarningSign();
        else ShowRightWarningSign();
    }

    private Vector3 PositionCalc(float angle, int wrap)
    {
        float MeToCanvas = Vector3.Distance(humanTransform.position, middleCanvas.transform.position);
        float indicatorDistance = Mathf.Tan(Mathf.Deg2Rad * angle) * MeToCanvas;
        RectTransform rectT = middleCanvas.GetComponent<RectTransform>();
        float scaledIndicatorDistance = rectT.rect.width * rectT.localScale.x * indicatorDistance / 1.5f;
        Vector3 distanceChange = scaledIndicatorDistance * (Quaternion.Euler(0, wrap * 90, 0) * mechTransform.forward);
        Vector3 pos = indicatorCanvas.transform.position + (1f * distanceChange);
        return pos;
    }

    public void ShowBlockSign(float angle, int wrap)
    {
        Vector3 pos = PositionCalc(angle, wrap);
        blockSignBlue.transform.position = pos;
        blockSignBlue.enabled = true;

        // StartCoroutine(SpawnDelay());
    }

    public void HideBlockSign()
    {
        blockSignBlue.enabled = false;
        blockSignYellow.enabled = false;
        blockSignGreen.enabled = false;
        blockSignRed.enabled = false;
        warningSignLeft.enabled = false;
        warningSignRight.enabled = false;
    }

    public void HideAttackSign()
    {
        attackSignBlue.enabled = false;
        attackSignGreen.enabled = false;
        warningSignLeft.enabled = false;
        warningSignRight.enabled = false;
    }

    public void ShowLeftWarningSign()
    {
        warningSignLeft.enabled = true;
    }

    public void ShowRightWarningSign()
    {
        warningSignRight.enabled = true;
    }

    private IEnumerator SpawnDelay()
    {
        yield return new WaitForSeconds(2);
        HideBlockSign();
    }
}
