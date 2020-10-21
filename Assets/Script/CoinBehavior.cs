using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinBehavior : MonoBehaviour
{

    [SerializeField]
    private GameObject coin;
    [SerializeField]
    private Transform t_player;
    [SerializeField]
    private Transform t_coin;
    [SerializeField]
    private AudioSource coin_sound;
    
    public PlayerInteraction pi;

    private void Start()
    {
        t_player = GameObject.FindWithTag("Player").transform;
        pi = t_player.GetComponent<PlayerInteraction>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {        
        if (coin != null)
        {
            if (Vector2.Distance(t_player.position, t_coin.position) < 1)
            {                
                coin_sound.Play();
                Destroy(coin);
                pi.coins_num++;
            }
        }        
    }
}
