using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class walkmanager : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform body;                  //body
    public LayerMask terrainLayer;          //terrain layer (note: higher than actual ground)
    public float stepstance;                //step distance
    public float high = 0.1f;               //
    public float speed = 2;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
