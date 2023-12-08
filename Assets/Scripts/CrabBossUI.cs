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
    public bool successfulAttack;
    public bool blockCheckDone;
    public Image blockCheckResult;

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
        angle = 40f;
    }

    // Update is called once per frame
    void Update()
    {
        if (successfulAttack) 
        {
            blockSignOn = false;
            HideBlockSign();
            attackSignBlue.enabled = false;
            AttackSignCalc(attackSignGreen);
            // StartCoroutine(Wait());
        }

        if (blockCheckDone) return;
        if (blockSignOn) 
        {
            HideAttackSign();
            if (playerController.successfulBlocking) 
            {
                blockSignYellow.enabled = false;
                BlockSignCalc(blockSignGreen);
                // StartCoroutine(Wait());
            }
            else if (playerController.unsuccessfulBlocking)
            {
                blockSignBlue.enabled = false;
                BlockSignCalc(blockSignRed);
            }
            else if (playerController.isBlocking)
            {
                blockSignBlue.enabled = false;
                blockSignGreen.enabled = false;
                BlockSignCalc(blockSignYellow);
            }
            else 
            {
                blockSignYellow.enabled = false;
                blockSignGreen.enabled = false;
                blockSignRed.enabled = false;
                BlockSignCalc(blockSignBlue);
            }
        }
        else
        {
            HideBlockSign();
            if (attackSignOn) 
            {
                attackSignGreen.enabled = false;
                AttackSignCalc(attackSignBlue);
            }
            else
            {
                HideAttackSign();
            }
        }
    }

    private void AttackSignCalc(Image sign)
    {
        if (crabTransform == null) return;
        Vector3 diff = crabTransform.position - player.position;
        Vector3 projectedVector = new Vector3(diff.x, 0, diff.z);
        float angleToPosition = Vector3.SignedAngle(mechTransform.forward, projectedVector, Vector3.up);

        if ((-1 * angle) <= angleToPosition && angleToPosition <= angle)
        {
            if (angleToPosition < 0) ShowAttackSign(sign, Mathf.Abs(angleToPosition), -1);
            else if (angleToPosition >= 0) ShowAttackSign(sign, Mathf.Abs(angleToPosition), 1);
        }
        else if ((-1 * angle) > angleToPosition) ShowLeftWarningSign();
        else ShowRightWarningSign();
    }

    public void ShowAttackSign(Image sign, float angle, int wrap)
    {
        Vector3 pos = PositionCalc(angle, wrap);
        sign.transform.position = pos;
        sign.enabled = true;
    }

    private void BlockSignCalc(Image sign)
    {
        if (crabTransform == null) return;
        Vector3 diff = crabTransform.position - player.position;
        Vector3 projectedVector = new Vector3(diff.x, 0, diff.z);
        float angleToPosition = Vector3.SignedAngle(mechTransform.forward, projectedVector, Vector3.up);

        if ((-1 * angle) <= angleToPosition && angleToPosition <= angle)
        {
            if (angleToPosition < 0) ShowBlockSign(sign, Mathf.Abs(angleToPosition), -1);
            else if (angleToPosition >= 0) ShowBlockSign(sign, Mathf.Abs(angleToPosition), 1);
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
        Vector3 direction = (Quaternion.Euler(0, wrap * 90, 0) * mechTransform.forward);
        Vector3 distanceChange = scaledIndicatorDistance * direction;
        Vector3 pos = indicatorCanvas.transform.position + (1f * distanceChange);
        return pos;
    }

    public void ShowBlockSign(Image sign, float angle, int wrap)
    {
        Vector3 pos = PositionCalc(angle, wrap);
        sign.transform.position = pos;
        sign.enabled = true;
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
        HideAttackSign();
        HideBlockSign();
        warningSignLeft.enabled = true;
    }

    public void ShowRightWarningSign()
    {
        HideAttackSign();
        HideBlockSign();
        warningSignRight.enabled = true;
    }

    public IEnumerator Wait()
    {
        yield return new WaitForSeconds(10);
    }
}
