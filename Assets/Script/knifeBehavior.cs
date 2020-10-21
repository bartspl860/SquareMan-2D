using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class knifeBehavior : MonoBehaviour
{
    private Transform t_ag_enemy;
    private Collider2D c2d_ag_en;
    private PolygonCollider2D PolygonCollider2D;
    public PlayerInteraction pi;    
    // Start is called before the first frame update
    void Start()
    {
        t_ag_enemy = GameObject.FindWithTag("AgresiveEnemy").transform;
        c2d_ag_en = t_ag_enemy.GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        AgresiveEnemy enemy = collision.GetComponent<AgresiveEnemy>();

        if (enemy != null)
        {
            pi.knife_audio.clip = pi.slay_knife;
            pi.knife_audio.Play();
            enemy.TakeDamage(1);
        }
    }

    private void FixedUpdate()
    {        

    }
}
