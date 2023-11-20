using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCanvas : MonoBehaviour
{
    public Canvas middleCanvas;
    public Image attackSign;
    public Image blockSign;
    public Image armorSign;
    public Image warningSignLeft;
    public Image warningSignRight;
    public Transform humanTransform;
    public Transform RobotTransform;

    private bool attackSignOn;
    private bool blockSignOn;
    private bool armorSignOn;

    void Awake()
    {
        attackSign.enabled = false;
        blockSign.enabled = false;
        armorSign.enabled = false;
        warningSignLeft.enabled = false;
        warningSignRight.enabled = false;

        attackSignOn = false;
        blockSignOn = false;
        armorSignOn = false;

        humanTransform = GameObject.Find("VRCharacterIK").transform;
    }

    private Vector3 PositionCalc(float angle, int wrap)
    {
        float MeToCanvas = Vector3.Distance(humanTransform.position, transform.position);
        float indicatorDistance = Mathf.Tan(Mathf.Deg2Rad * angle) * MeToCanvas;
        float newX = RobotTransform.position.x + wrap * indicatorDistance;
        Vector3 pos = new Vector3(newX, attackSign.transform.position.y, 
                                middleCanvas.transform.position.z);
        return pos;
    }

    public void ShowAttackSign(float angle, int wrap)
    {
        if (blockSignOn == false)
        {
            Vector3 pos = PositionCalc(angle, wrap);
            attackSign.transform.position = pos;
            attackSign.enabled = true;
            attackSignOn = true;
        }
    }

    public void ShowBlockSign(float angle, int wrap)
    {
        attackSign.enabled = false;
        attackSignOn = false;

        Vector3 pos = PositionCalc(angle, wrap);
        blockSign.transform.position = pos;
        blockSign.enabled = true;
        blockSignOn = true;

        StartCoroutine(SpawnDelay());
    }

    public void ShowLeftWarningSign()
    {
        warningSignLeft.enabled = true;
    }

    public void ShowRightWarningSign()
    {
        warningSignRight.enabled = true;
    }

    public void HideBlockSign()
    {
        blockSign.enabled = false;
        blockSignOn = false;
    }

    private IEnumerator SpawnDelay()
    {
        yield return new WaitForSeconds(2);
        HideBlockSign();
    }
}
