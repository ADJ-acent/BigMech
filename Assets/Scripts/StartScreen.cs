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
    public Canvas startCanvas;
    public GameObject blackBox;
    public MechMovementPrototype script;
    private Animator selfAnim;
    private Animator cogAnim;
    public int counter;

    public GameObject smallCrab1;
    public GameObject smallCrab2;
    public GameObject smallCrab3;
    public GameObject smallCrab4;
    public GameObject smallCrab5;
    public GameObject smallCrab6;
    public GameObject smallCrab7;
    public GameObject smallCrab8;
    public GameObject smallCrab9;
    public GameObject CrabBoss;

    private bool soundPlayed;

    // private List<string> triggers = new List<string>(){"Attack", "Block", "Start"};

    // Start is called before the first frame update
    void Start()
    {
        selfAnim = transform.GetComponent<Animator>();
        cogAnim = cog.GetComponent<Animator>();
        counter = 0;
        soundPlayed = false;
        startCanvas = startScreen.GetComponent<Canvas>();
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
                if (!soundPlayed)
                {
                    StartCoroutine(mechStarting());
                }
        }
        // }
    }

    public void DisableObjects()
    {
        script.enabled = true;
        /*startScreen.SetActive(false);*/
        ActivateSmallCrabs();
        startCanvas.enabled = false;
        /*Destroy(startScreen);*/
        Destroy(blackBox);
    }

    public void SetCounter()
    {
        counter = 4;
    }

    public void ActivateSmallCrabs()
    {
        bool done = true;
        if (smallCrab1 != null)
        {
            done = false;
            smallCrab1.GetComponent<UnityEngine.AI.NavMeshAgent>().speed = 12f;
        }
            
        if (smallCrab1 != null)
        {
            done = false;
            smallCrab2.GetComponent<UnityEngine.AI.NavMeshAgent>().speed = 12f;
        }
        if (smallCrab1 != null)
        {
            done = false;
            smallCrab3.GetComponent<UnityEngine.AI.NavMeshAgent>().speed = 12f;
        }
        if (smallCrab1 != null)
        {
            done = false;
            smallCrab4.GetComponent<UnityEngine.AI.NavMeshAgent>().speed = 12f;
        }
        if (smallCrab1 != null)
        {
            done = false;
            smallCrab5.GetComponent<UnityEngine.AI.NavMeshAgent>().speed = 12f;
        }
        if (smallCrab1 != null)
        {
            done = false;
            smallCrab6.GetComponent<UnityEngine.AI.NavMeshAgent>().speed = 12f;
        }
        if (smallCrab1 != null)
        {
            done = false;
            smallCrab7.GetComponent<UnityEngine.AI.NavMeshAgent>().speed = 12f;
        }
        if (smallCrab1 != null)
        {
            done = false;
            smallCrab8.GetComponent<UnityEngine.AI.NavMeshAgent>().speed = 12f;
        }
        if (smallCrab1 != null)
        {
            done = false;
            smallCrab9.GetComponent<UnityEngine.AI.NavMeshAgent>().speed = 12f;
        }

        if (done)
        {
            StopCoroutine(Wait());
            CrabBoss.SetActive(true);
            Destroy(startScreen);
            
        }
        StartCoroutine(Wait());
    }

    public IEnumerator Wait()
    {
        yield return new WaitForSeconds(20.0f);
        CrabBoss.SetActive(true);
        Destroy(startScreen);
    }

    public IEnumerator mechStarting()
    {
        yield return new WaitForSeconds(0.5f);
        AudioManager.Instance.playMechStart(startScreen);
        soundPlayed = true;
    }
}
