using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCanvas : MonoBehaviour
{
    public Canvas middleCanvas;
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
        Vector3 pos = new Vector3(newX, middleCanvas.transform.position.y, 
                                middleCanvas.transform.position.z);
        return pos;
    }

    public void ShowAttackSign(GameObject attackSign, float angle, int wrap)
    {
        if (blockSignOn == false)
        {
            Vector3 pos = PositionCalc(angle, wrap);
            attackSign.transform.position = pos;
            attackSign.SetActive(true);
            attackSignOn = true;
        }
    }

    public void ShowBlockSign(GameObject attackSign, GameObject blockSign, float angle, int wrap)
    {
        attackSign.SetActive(false);
        attackSignOn = false;

        Vector3 pos = PositionCalc(angle, wrap);
        blockSign.transform.position = pos;
        blockSign.SetActive(true);
        blockSignOn = true;

        StartCoroutine(SpawnDelay(blockSign));
    }

    public void ShowLeftWarningSign()
    {
        warningSignLeft.enabled = true;
    }

    public void ShowRightWarningSign()
    {
        warningSignRight.enabled = true;
    }

    public void HideLeftWarningSign()
    {
        warningSignLeft.enabled = false;
    }

    public void HideRightWarningSign()
    {
        warningSignRight.enabled = false;
    }

    public void HideBlockSign(GameObject blockSign)
    {
        blockSign.SetActive(false);
        blockSignOn = false;
    }

    private IEnumerator SpawnDelay(GameObject blockSign)
    {
        yield return new WaitForSeconds(2);
        HideBlockSign(blockSign);
    }
}
