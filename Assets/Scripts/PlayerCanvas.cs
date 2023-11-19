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
    }

    public void ShowAttackSign(Vector3 playerPosition, Vector3 enemyPosition)
    {
        if (blockSignOn == false)
        {
            Vector3 pos = new Vector3(0.6f, attackSign.transform.position.y, 
                                      middleCanvas.transform.position.z);
            attackSign.transform.position = pos;
            attackSign.enabled = true;
            attackSignOn = true;
        }
    }

    public void ShowBlockSign(Vector3 playerPosition, Vector3 enemyPosition)
    {
        attackSign.enabled = false;
        attackSignOn = false;

        // TODO: replace hardcoded x value
        Vector3 pos = new Vector3(0f, blockSign.transform.position.y, 
                                  middleCanvas.transform.position.z);
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
