using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBehavior : MonoBehaviour
{
    private Rigidbody2D camera_rb2d;    
    private Transform t_camera;
    private Camera cam;

    [SerializeField]
    private GameObject text_above;
    [SerializeField]
    private Collider2D Collider2D;
    [SerializeField]
    private SpriteRenderer SpriteRenderer;
    [SerializeField]
    private Sprite door_open;
    [SerializeField]
    private Sprite door_closed;
    [SerializeField]
    private int which_lvl = 1;
    
    private Transform t_player;
    private Collider2D c2d_player;
    private bool onlyOnce = true;

    // Start is called before the first frame update
    void Start()
    {
        t_player = GameObject.FindWithTag("Player").transform;
        c2d_player = t_player.GetComponent<Collider2D>();

        t_camera = GameObject.FindWithTag("MainCamera").transform;
        camera_rb2d = t_camera.GetComponent<Rigidbody2D>();
        cam = t_camera.GetComponent<Camera>();

        t_camera.position = new Vector3(7.6f, -9.5f, -10);
        cam.backgroundColor = new Color(0f, 0f, 0f);
    }    

    void FixedUpdate()
    {
        if (Collider2D.IsTouching(c2d_player))
        {
            SpriteRenderer.sprite = door_open;
            if (Input.GetKeyDown("e"))
            {
                which_lvl++;
            }
            text_above.GetComponent<Canvas>().enabled = true;
        }
        else
        {
            text_above.GetComponent<Canvas>().enabled = false;
            SpriteRenderer.sprite = door_closed;
        }
        
        float lvl1_camera_position = 7.6f;
        float lvl2_camera_position = 49.5f;
        float lvl1_lvl2_percent_length = (t_camera.position.x - lvl1_camera_position) / (lvl2_camera_position - lvl1_camera_position);

        if (which_lvl == 2)
        {            
            if (onlyOnce)
            {
                t_player.position = new Vector3(42.4f, -17.7f, 0f);
                onlyOnce = false;
            }

            Color lvl2_BG = new Color((214f / 255f), (162f / 255f), (144f / 255f));
            cam.backgroundColor = Color.Lerp(Color.black, lvl2_BG, lvl1_lvl2_percent_length);

            if (t_camera.position.x <= lvl2_camera_position)
            {
                camera_rb2d.velocity = t_camera.right * 15;                
            }
            else
            {                
                camera_rb2d.velocity = t_camera.right * 0;
            }
        }
    }
}
