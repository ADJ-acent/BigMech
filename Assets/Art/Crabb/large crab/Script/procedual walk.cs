﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class target : MonoBehaviour
{
    public Transform body;                  //body
    public LayerMask terrainLayer;          //terrain layer (note: higher than actual ground)
    public float stepstance;                //step distance
    public float high = 0.1f;               //
    public float speed = 2;                 //
    public GameObject manager;              //public GameObject manager; //manage all above parameters
    Vector3 newposition, oldposition, currentposition; //positions
    private float offset = 0.5F;
    float lerp = 1;
    public Transform center;    //an empty object that keep record of the default position

    public target[] otherLegs = new target[default];   //legs that does not move simultaniously 
    public float footSpacing1, footSpacing2; // position wrt. body
    private bool crabWalkSound;
    private int count;
    bool withinRange;
    private float footPos;
    public GameObject Ground;
    float groundYPosition;

    private void Start()
    {
        newposition = transform.position;
        currentposition = transform.position;
        body = manager.GetComponent<walkmanager>().body;                  //body
        terrainLayer = manager.GetComponent<walkmanager>().terrainLayer;     //terrain layer (note: higher than actual ground)
        stepstance = manager.GetComponent<walkmanager>().stepstance;            //step distance
        high = manager.GetComponent<walkmanager>().high;               //
        speed = manager.GetComponent<walkmanager>().speed;
        crabWalkSound = false;
        count = 0;
        withinRange = false;
        groundYPosition = Ground.transform.position.y;
        //Debug.Log(groundYPosition);

    }

    bool noLegsMoving()
    {
        foreach (target leg in otherLegs)
        {
            if (leg == null)
            {
                continue;
            }
            if (leg.lerp < 1) // true if all >=1 
            {
                return false;
            }
        }
        return true;
    }
    void Update()
    {




        transform.position = currentposition;
        //Ray ray = new Ray(body.position + (body.up * footSpacing1)+(body.right*footSpacing2), -body.forward);
        //Ray ray = new Ray(center.position, -body.forward);
        /*
        if (Physics.Raycast(ray,out RaycastHit info, 10, terrainLayer.value))
        {
            if (Vector3.Distance(newposition, info.point) > stepstance && noLegsMoving() && lerp >= 1)
            {
                lerp = 0;
                newposition = info.point + new Vector3(0,offset,0);
            }
        }*/
        if (Vector3.Distance(newposition, center.position) > stepstance && noLegsMoving() && lerp >= 1)
        {
            lerp = 0;
            newposition = center.position + new Vector3(0, offset, 0);
        }

        //Vector3 footposition1 = Vector3.Lerp(oldposition, newposition, lerp);
        float footposition1 = currentposition.y-6f;
        //footposition1.y += Mathf.Sin(lerp * Mathf.PI) * high;
        //print(footposition1);

        if (footposition1 <= groundYPosition - Mathf.Epsilon && footposition1 >= groundYPosition + Mathf.Epsilon)
        {
            withinRange = true;
        }

        //if (lerp <= 1 - Mathf.Epsilon && lerp >= 1 + Mathf.Epsilon)
        //{
        //    withinRange = true;
        //    Debug.Log("ever here?");
        //}

        if (withinRange && !crabWalkSound)
        {
            crabWalkSound = true;
            StartCoroutine(PlayCrabWalkSound());
            //AudioManager.Instance.playCrabWalk();
        }

        if (lerp < 1)
        {
            Vector3 footposition = Vector3.Lerp(oldposition, newposition, lerp);
            footposition.y += Mathf.Sin(lerp * Mathf.PI) * high;
            currentposition = footposition;
            lerp += Time.deltaTime * speed;
        }
        else
        {
            oldposition = newposition;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(newposition, 0.2f);
    }

    private IEnumerator PlayCrabWalkSound()
    {
        AudioManager.Instance.playCrabWalk();
        count += 1;
        //Debug.Log(count);
        yield return new WaitForSeconds(0.5f);
        crabWalkSound = false;
    }
}
