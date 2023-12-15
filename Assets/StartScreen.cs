using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;
using UnityEngine.XR.Interaction.Toolkit;

public class StartScreen : MonoBehaviour
{
    public InputActionReference rightTrigger;
    public InputActionReference rightGrip;
    public InputActionReference leftTrigger;
    public InputActionReference leftGrip;
    public GameObject cog;
    public GameObject startScreen;
    public GameObject blackBox;
    public MechMovementPrototype script;
    private Animator selfAnim;
    private Animator cogAnim;
    public int counter;

    // private List<string> triggers = new List<string>(){"Attack", "Block", "Start"};

    // Start is called before the first frame update
    void Start()
    {
        selfAnim = transform.GetComponent<Animator>();
        cogAnim = cog.GetComponent<Animator>();
        counter = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // if (triggers.Count != 0)
        // {
        //     Debug.Log(triggers);
        //     if (Mathf.Epsilon <= (rightTrigger.action.ReadValue<float>()) || 
        //         Mathf.Epsilon <= (rightGrip.action.ReadValue<float>()) ||
        //         Mathf.Epsilon <= (leftTrigger.action.ReadValue<float>()) || 
        //         Mathf.Epsilon <= (leftGrip.action.ReadValue<float>()))
        //         {
        //             selfAnim.SetTrigger(triggers[0]);
        //             triggers.RemoveAt(0);
        //         }
        // }
        // else 
        // {
        if (Mathf.Epsilon <= (rightGrip.action.ReadValue<float>()) &&  
            Mathf.Epsilon <= (leftGrip.action.ReadValue<float>()) && counter == 4)
            {
                selfAnim.SetTrigger("Starting");
                cog.SetActive(true);
            }
        // }
    }

    public void DisableObjects()
    {
        script.enabled = true;
        startScreen.SetActive(false);
        Destroy(startScreen);
        Destroy(blackBox);
    }

    public void SetCounter()
    {
        counter = 4;
    }
}
