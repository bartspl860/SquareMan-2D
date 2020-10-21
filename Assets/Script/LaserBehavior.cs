using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBehavior : MonoBehaviour
{
    [Header("Object")]
    [SerializeField]
    private GameObject laser;   

    [Header("RigidBody2D")]
    [SerializeField]
    private Rigidbody2D laser_rb2d;

    [Header("Transform")]
    [SerializeField]
    private Transform t_laser;
    [SerializeField]
    private Transform shootingPoint;

    [Header("Local Variables")]
    [SerializeField]
    private float laser_thrust;
    [Header("Audio")]
    [SerializeField]
    private AudioSource laser_sound;
    [Space]
    public PlayerInteraction pi;
    // Start is called before the first frame update

    void Update()
    {

        if(pi.shoots_left > 0)
        {
            if (Input.GetButtonDown("Fire1") &&pi.pistol)
            {                
                Instantiate(laser, shootingPoint.position, shootingPoint.rotation);
                pi.shoots_left--;
                laser_sound.Play();
            }
        }
        
          
    }
}
