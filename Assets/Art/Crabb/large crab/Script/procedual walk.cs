using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class target : MonoBehaviour
{
    public Transform body;                  //body
    public LayerMask terrainLayer;          //terrain layer (note: higher than actual ground)
    public float stepstance;                //step distance
    public float high = 0.1f;               //
    public float speed = 2;                 //
    public GameObject manager;
    Vector3 newposition, oldposition, currentposition; //positions
    private float offset = 2.5f;
    float lerp = 1;


    //public GameObject manager; //manage all above parameters

    public target[] otherLegs= new target[default];   //legs that does not move simultaniously 
    public float footSpacing1, footSpacing2; // position wrt. body
    private void Start()
    {
        newposition = transform.position;
        currentposition = transform.position;
        body = manager.GetComponent<walkmanager>().body;                  //body
        terrainLayer = manager.GetComponent<walkmanager>().terrainLayer;     //terrain layer (note: higher than actual ground)
        stepstance = manager.GetComponent<walkmanager>().stepstance;            //step distance
        high = manager.GetComponent<walkmanager>().high;               //
        speed = manager.GetComponent<walkmanager>().speed;

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
        Ray ray = new Ray(body.position + (body.up * footSpacing1)+(body.right*footSpacing2), -body.forward);
        if (Physics.Raycast(ray,out RaycastHit info, 10, terrainLayer.value))
        {
            if (Vector3.Distance(newposition, info.point) > stepstance && noLegsMoving() && lerp >= 1)
            {
                lerp = 0;
                newposition = info.point + new Vector3(0,offset,0);
            }
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
}
