using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;

public class StartScreen : MonoBehaviour
{
    public InputActionReference rightTrigger;
    public InputActionReference rightGrip;
    public InputActionReference leftTrigger;
    public InputActionReference leftGrip;
    public GameObject cog;
    public GameObject startScreen;
    public GameObject pointLight;
    public GameObject blackBox;
    public MechMovementPrototype script;
    private Animator selfAnim;
    private Animator cogAnim;

    private List<string> triggers = new List<string>(){"Attack", "Block", "Start"};

    // Start is called before the first frame update
    void Start()
    {
        selfAnim = transform.GetComponent<Animator>();
        cogAnim = cog.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (triggers.Count != 0)
        {
            if (Mathf.Epsilon <= (rightTrigger.action.ReadValue<float>()) || 
                Mathf.Epsilon <= (rightGrip.action.ReadValue<float>()) ||
                Mathf.Epsilon <= (leftTrigger.action.ReadValue<float>()) || 
                Mathf.Epsilon <= (leftGrip.action.ReadValue<float>()))
                {
                    selfAnim.SetTrigger(triggers[0]);
                    triggers.RemoveAt(0);
                }
        }
        else 
        {
            if (Mathf.Epsilon <= (rightGrip.action.ReadValue<float>()) &&  
                Mathf.Epsilon <= (leftGrip.action.ReadValue<float>()))
                {
                    selfAnim.SetTrigger("Starting");
                    cog.SetActive(true);
                }
        }
    }

    public void DisableObjects()
    {
        pointLight.SetActive(false);
        blackBox.SetActive(false);
        script.enabled = true;
        startScreen.SetActive(false);
        Destroy(startScreen);
    }

    public void RemoveTrigger()
    {
        // triggers.RemoveAt(0);
    }
}
