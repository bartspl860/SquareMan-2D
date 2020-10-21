using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    
    public const float NEXT_WAYPOINT_THRESHOLD = 0.05f;

    private Transform t_player;    
    private Collider2D c2d_player;
    [SerializeField]
    private Rigidbody2D rb2d_enemy;
    [SerializeField]
    private float speed = 8f;
    [SerializeField]
    private Vector2[] path;
    [SerializeField]
    private Vector3 posAfterCol;
    [SerializeField]
    private AudioSource hit_sound;

    private int pathIndex = 0;
    private bool reverse = false;

    public PlayerInteraction pi;

    public Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        t_player = GameObject.FindWithTag("Player").transform;
        c2d_player = t_player.GetComponent<Collider2D>();
        pi = t_player.GetComponent<PlayerInteraction>();
        hit_sound = t_player.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 dir = path[pathIndex] - (Vector2) transform.position;
        dir.Normalize();

        rb.AddForce(dir * speed * Time.fixedDeltaTime);

        if(Vector2.Distance(transform.position, path[pathIndex]) < NEXT_WAYPOINT_THRESHOLD)
        {
            transform.position = path[pathIndex];
            
            pathIndex += reverse ? -1 : 1;

            if(pathIndex >= path.Length-1 || pathIndex <= 0)
            {
                reverse = !reverse;
            }
        }

        if (c2d_player != null && rb2d_enemy.IsTouching(c2d_player))
        {            
            hit_sound.Play();
            t_player.position = posAfterCol;
            pi.health--;
            pi.onlyOnce = true;
            pi.checkOneMoreThing = true;
        }
    }
}
