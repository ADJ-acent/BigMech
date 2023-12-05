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
    [SerializeField] public AK.Wwise.Event smallHit;
    [SerializeField] public AK.Wwise.Event midHit;
    [SerializeField] public AK.Wwise.Event bigHit;
    [SerializeField] public GameObject leftArmSocket;
    [SerializeField] public GameObject rightArmSocket;
    private string RTPC_Velocity = "ArmVelocity";

    // mech robot sounds

    public void startArmRampUp()
    {
            armRampUp.Post(leftArmSocket);
            armRampUp.Post(rightArmSocket);
       
    }

    public void updateArmRampUp(bool left, float robotArmVelocity)
    {
        if (left)
        {
            AkSoundEngine.SetRTPCValue(RTPC_Velocity, robotArmVelocity, leftArmSocket);
        }
        else
        {
            AkSoundEngine.SetRTPCValue(RTPC_Velocity, robotArmVelocity, rightArmSocket);
        }
    }

    public void playFootsteps()
    {
        mechFootSteps.Post(gameObject);
    }

    public void playSmallHit(GameObject source)
    {
        smallHit.Post(source);
    }

    public void playMidHit(GameObject source)
    {
        midHit.Post(source);
    }

    public void playBigHit(GameObject source)
    {
        bigHit.Post(source);
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
