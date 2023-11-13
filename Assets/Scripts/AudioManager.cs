using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager: Singleton<AudioManager>
{
    [Header("Building Smash Events")]
    [SerializeField] public AK.Wwise.Event buildingFall;
    [SerializeField] public AK.Wwise.Event smallPiecesBreak;
    [SerializeField] public AK.Wwise.Event punchBuilding;
    
    [Header("Mech Sounds")]
    [SerializeField] public AK.Wwise.Event mechFootSteps;
    [SerializeField] public AK.Wwise.Event armRampUp;

    // mech robot sounds


    public void playFootsteps()
    {
        mechFootSteps.Post(gameObject);
    }

    public void playArmRampUp()
    {
        armRampUp.Post(gameObject);
    }

    // building destroy sounds

    public void playBuildingCollapse(GameObject source)
    {
        buildingFall.Post(source);
    }

    public void playSmallPiecesShatter(GameObject source)
    {
        smallPiecesBreak.Post(source);
    }
    public void playPunchBuilding(GameObject source)
    {
        punchBuilding.Post(source);
    }

    
}
