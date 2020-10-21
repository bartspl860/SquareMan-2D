using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPackBehavior : MonoBehaviour
{
    private Transform t_player;
    [SerializeField]
    private AudioSource ammo_audio;

    public PlayerInteraction pi;
    // Start is called before the first frame update
    void Start()
    {
        t_player = GameObject.FindWithTag("Player").transform;
        pi = t_player.GetComponent<PlayerInteraction>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(Vector2.Distance(t_player.position,transform.position) < 1)
        {
            pi.knife_audio.clip = pi.ammo_pick;
            pi.knife_audio.Play();            
            pi.shoots_left += 15;
            Destroy(this.gameObject);
        }        
    }
}
