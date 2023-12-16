using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class EndScreen : MonoBehaviour
{
    public GameObject cog;
    private Animator cogAnim;
    private Animator selfAnim;
    public InputActionReference rightGrip;
    public InputActionReference leftGrip;
    public int counter;

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
        if (Mathf.Epsilon <= (rightGrip.action.ReadValue<float>()) &&  
            Mathf.Epsilon <= (leftGrip.action.ReadValue<float>()) && counter == 4)
        {
            selfAnim.SetTrigger("Starting");
            cog.SetActive(true);
        }
    }

    public void DisableObjects()
    {
        SceneManager.LoadScene("Final");
    }

    public void SetCounter()
    {
        counter = 4;
    }
}