using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class MechFist : MonoBehaviour
{
    private float lastPlayedSound = 0f;
    [SerializeReference]
    private ActionBasedController _controller;
    [SerializeReference] private GameObject _explosionGameObject;
    private ParticleSystem _explosion;
    public bool isLeft;
    public RobotArmProjection robotArmProjection;
    
    // Start is called before the first frame update
    void Start()
    {
        _explosion = _explosionGameObject?.GetComponent<ParticleSystem>();
        _explosion?.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = Vector3.zero;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Building")&& Time.time - lastPlayedSound > 2.0f)
        {
            AudioManager.Instance.playBuildingCollapse(gameObject);
            lastPlayedSound = Time.time;
            if (_explosion!= null)
            {
                _explosion.Clear();
                _explosion.transform.position = other.transform.position;
                _explosion.Play();
            }
            _controller.SendHapticImpulse(0.3f, .2f);
        }

        if (other.gameObject.CompareTag("Crab"))
        {
            robotArmProjection.hitMech(isLeft);
        }
    }
    
}
