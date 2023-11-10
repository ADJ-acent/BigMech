using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager: MonoBehaviour
{
    [Header("Building Smash Events")]
    [SerializeField] public AK.Wwise.Event buildingFall;
    [SerializeField] public AK.Wwise.Event smallPiecesBreak;
    [SerializeField] public AK.Wwise.Event punchBuilding;
    
    [Header("Mech Sounds")]
    [SerializeField] public AK.Wwise.Event mechFootSteps;

    public void playFootsteps()
    {
        mechFootSteps.Post(gameObject);
    }

    public void playBuildingCollapse()
    {
        buildingFall.Post(gameObject);
    }

    public void playSmallPiecesShatter()
    {
        smallPiecesBreak.Post(gameObject);
    }
    public void playPunchBuilding()
    {
        punchBuilding.Post(gameObject);
    }

    
}
