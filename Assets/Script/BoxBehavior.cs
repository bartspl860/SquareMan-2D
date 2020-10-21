using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxBehavior : MonoBehaviour
{
    [SerializeField]
    private Transform t_box;
    [SerializeField]
    private Transform t_player;
    [SerializeField]
    private float box_thrust;
    [SerializeField]
    private float grabPoint;
    [SerializeField]
    private Rigidbody2D rb2d_box;
    [SerializeField]
    private SpriteRenderer player;
    [SerializeField]
    private Sprite busy;
    [SerializeField]
    private Sprite look_x;

    private Collider2D c2d_enemy;
    private Transform t_enemy;

    private void Start()
    {
        t_player = GameObject.FindWithTag("Player").transform;
        player = t_player.GetComponent<SpriteRenderer>();

        t_enemy = GameObject.FindWithTag("Enemy").transform;
        c2d_enemy = t_enemy.GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (rb2d_box.IsTouching(c2d_enemy))
        {
            Destroy(gameObject);
        }
        if (Input.GetKey("left shift"))
        {
            if (Vector2.Distance(t_box.position, t_player.position) < grabPoint)
            {
                if (Input.GetKey("w"))
                {
                    player.sprite = busy;
                    rb2d_box.AddForce(t_box.up * box_thrust * Time.deltaTime);
                }
                else if (Input.GetKey("s"))
                {
                    player.sprite = busy;
                    rb2d_box.AddForce(t_box.up * -box_thrust * Time.deltaTime);
                }
                else if (Input.GetKey("a"))
                {
                    player.sprite = busy;
                    player.flipX = true;
                    rb2d_box.AddForce(t_box.right * -box_thrust * Time.deltaTime);
                }
                else if (Input.GetKey("d"))
                {
                    player.sprite = busy;
                    player.flipX = false;
                    rb2d_box.AddForce(t_box.right * box_thrust * Time.deltaTime);
                }
            }
        }
    }
}
