using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCanvas : MonoBehaviour
{
    public Canvas playerCanvas;
    public Image attackSign;
    public Image blockSign;
    public Image warningSign;
    public Image armorSign;

    private bool attackSignOn;
    private bool blockSignOn;

    void Awake()
    {
        attackSign.enabled = false;
        blockSign.enabled = false;
        warningSign.enabled = false;
        armorSign.enabled = false;

        attackSignOn = false;
        blockSignOn = false;
    }

    public void ShowAttackSign(Vector3 playerPosition, Vector3 enemyPosition)
    {
        if (blockSignOn == false)
        {
            // TODO: replace hardcoded x value
            Vector3 pos = new Vector3(0.6f, attackSign.transform.position.y, 
                                      playerCanvas.transform.position.z);
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
        Vector3 pos = new Vector3(0.6f, blockSign.transform.position.y, 
                                  playerCanvas.transform.position.z);
        blockSign.transform.position = pos;
        blockSign.enabled = true;
        blockSignOn = true;

        StartCoroutine(SpawnDelay());
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
