using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletPhysics : MonoBehaviour
{
    [SerializeField]
    private GameObject laser;
    [SerializeField]
    private ContactFilter2D agresive_enemy;
    [SerializeField]
    private Rigidbody2D rb2d;
    [SerializeField]
    private CapsuleCollider2D cc2d;
    [SerializeField]
    private float speed = 40f;

    // Start is called before the first frame update
    void Start()
    {
        rb2d.velocity = transform.right * speed;        
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        AgresiveEnemy enemy = hitInfo.GetComponent<AgresiveEnemy>();
        if(enemy != null)
        {
            enemy.TakeDamage(1);
        }
        Destroy(laser);
    }
}
