using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrabBossUI : MonoBehaviour
{
    public Canvas middleCanvas;
    public Image attackSign;
    public Image blockSign;
    public Image warningSignLeft;
    public Image warningSignRight;

    public bool attackSignOn;
    public bool blockSignOn;
    public bool warningSignLeftOn;
    public bool warningSignRightOn;

    public Vector3 crabPosition;
    public Transform player;
    public Transform humanTransform;
    public Transform mechTransform;

    private float angle;

    // Start is called before the first frame update
    void Start()
    {
        attackSign.enabled = false;
        blockSign.enabled = false;
        warningSignLeft.enabled = false;
        warningSignRight.enabled = false;

        Vector3 rand = Random.insideUnitCircle;
        print(rand);
        crabPosition = new Vector3(rand.x, 0, rand.y) + transform.position;
        player = GameObject.Find("PlayerController").transform;
        humanTransform = GameObject.Find("VRCharacterIK").transform;
        mechTransform = GameObject.Find("Robotv2").transform; // TODO: change robot name

        angle = 65f;
    }

    // Update is called once per frame
    void Update()
    {
        // if crab in attack range

        // if crab attack boolean
        BlockSignCalc();
        if (blockSignOn) BlockSignMode();

        // else hide this or that
    }

    private void BlockSignMode()
    {

    }

    private void BlockSignCalc()
    {
        Vector3 diff = crabPosition - player.position;
        Vector3 projectedVector = new Vector3(diff.x, 0, diff.z);
        float angleToPosition = Vector3.SignedAngle(mechTransform.forward, projectedVector, Vector3.up);
        // angleToPosition = angleToPosition * 1.7f;
        // if (angleToPosition >= 60f && angleToPosition < 70f) angleToPosition = angleToPosition * 0.9f;
        // else if (angleToPosition >= 70f) angleToPosition = angleToPosition * 0.8f;

        // print(angleToPosition);

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
        float MeToCanvas = Vector3.Distance(humanTransform.position, transform.position);
        float indicatorDistance = Mathf.Tan(Mathf.Deg2Rad * angle) * MeToCanvas;
        float newX = mechTransform.position.x + wrap * indicatorDistance;
        Vector3 pos = new Vector3(newX, middleCanvas.transform.position.y, 
                                middleCanvas.transform.position.z);
        return pos;
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

    public void HideBlockSign()
    {
        blockSign.enabled = false;
        blockSignOn = false;
    }

    public void ShowLeftWarningSign()
    {
        warningSignLeft.enabled = true;
        warningSignLeftOn = true;
    }

    public void ShowRightWarningSign()
    {
        warningSignRight.enabled = true;
        warningSignLeftOn = true;
    }

    public void HideLeftWarningSign()
    {
        warningSignLeft.enabled = false;
        warningSignLeftOn = false;
    }

    public void HideRightWarningSign()
    {
        warningSignRight.enabled = false;
        warningSignRightOn = false;
    }

    private IEnumerator SpawnDelay()
    {
        yield return new WaitForSeconds(2 );
        HideBlockSign();
    }
}
