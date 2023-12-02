using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestScript : MonoBehaviour
{
    public Image attack;
    // Start is called before the first frame update
    void Start()
    {
        attack.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("On");
            attack.enabled = true;
        }
    }
}
