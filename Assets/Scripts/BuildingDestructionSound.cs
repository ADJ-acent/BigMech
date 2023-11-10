using System;
using System.Collections;
using System.Collections.Generic;
using RayFire;
using UnityEngine;

public class BuildingDestructionSound : MonoBehaviour
{
    private float lastActivated = 0f;
    private bool collided = false;
    
    // Start is called before the first frame update
    void Start()
    {
        RayfireRigid rayfireRigid = gameObject.GetComponent<RayfireRigid>();
        if (rayfireRigid!= null)
        {
            rayfireRigid.activationEvent.LocalEvent += BuildingActivated;
            rayfireRigid.demolitionEvent.LocalEvent += BuildingDemolished;
        }
        
    }

    void BuildingActivated(RayfireRigid rigid)
    {
        if (Time.time - lastActivated > 1f)
        {
            
            AudioManager.Instance.playPunchBuilding(gameObject);
            lastActivated = Time.time;
        }
        
    }
    
    
    void BuildingDemolished(RayfireRigid rigid)
    {
        AudioManager.Instance.playBuildingCollapse(gameObject);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Building") && !collided)
        {
            AudioManager.Instance.playSmallPiecesShatter(gameObject);
            collided = true;
        }
        
    }
}
