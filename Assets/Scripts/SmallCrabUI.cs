using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SmallCrabUI : MonoBehaviour
{
    public Canvas middleCanvas;
    public Canvas indicatorCanvas;
    public GameObject attackSignBlue0;
    public GameObject attackSignGreen0;
    public GameObject warningSignLeft;
    public GameObject warningSignRight;

    public Transform player;
    public Transform humanTransform;
    public Transform mechTransform;
    private float angle;

    public PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        attackSignBlue0.SetActive(false);
        attackSignGreen0.SetActive(false);
        warningSignLeft.SetActive(false);
        warningSignRight.SetActive(false);

        angle = 40f;
    }

    public void AttackSignCalc(Image sign, Image sign1, Image sign2, Transform crabTransform)
    {
        if (crabTransform == null) return;
        Vector3 diff = crabTransform.position - player.position;
        Vector3 projectedVector = new Vector3(diff.x, 0, diff.z);
        float angleToPosition = Vector3.SignedAngle(mechTransform.forward, projectedVector, Vector3.up);
        angleToPosition *= 1.2f;

        if ((-1 * angle) <= angleToPosition && angleToPosition <= angle)
        {
            if (angleToPosition < 0) 
            {
                HideWarningSign(sign1, sign2);
                ShowAttackSign(sign, Mathf.Abs(angleToPosition), -1);
            }
            else if (angleToPosition >= 0) 
            {
                HideWarningSign(sign1, sign2);
                ShowAttackSign(sign, Mathf.Abs(angleToPosition), 1);
            }
        }
        else if ((-1 * angle) > angleToPosition) ShowLeftWarningSign(sign, sign1);
        else ShowRightWarningSign(sign, sign2);
    }

    private Vector3 PositionCalc(float angle, int wrap)
    {
        float MeToCanvas = Vector3.Distance(humanTransform.position, middleCanvas.transform.position);
        float indicatorDistance = Mathf.Tan(Mathf.Deg2Rad * angle) * MeToCanvas;
        RectTransform rectT = middleCanvas.GetComponent<RectTransform>();
        float scaledIndicatorDistance = rectT.rect.width * rectT.localScale.x * indicatorDistance / 1.5f;
        Vector3 direction = (Quaternion.Euler(0, wrap * 90, 0) * mechTransform.forward);
        Vector3 distanceChange = scaledIndicatorDistance * direction;
        Vector3 pos = attackSignBlue0.transform.position + distanceChange;
        return pos;
    }

    public void ShowAttackSign(Image sign, float angle, int wrap)
    {
        Vector3 pos = PositionCalc(angle, wrap);
        sign.transform.position = pos;
        sign.enabled = true;
    }

    public void ShowLeftWarningSign(Image sign, Image sign1)
    {
        HideAttackSign(sign);
        sign1.enabled = true;
    }

    public void ShowRightWarningSign(Image sign, Image sign2)
    {
        HideAttackSign(sign);
        sign2.enabled = true;
    }

    public void HideWarningSign(Image sign, Image sign1)
    {
        sign.enabled = false;
        sign1.enabled = false;
    }

    public void HideAttackSign(Image sign)
    {
        sign.enabled = false;
    }
}
