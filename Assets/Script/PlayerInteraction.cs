using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerInteraction : MonoBehaviour
{
    [Header("Object")]        
    [SerializeField]
    private GameObject knife_obj;    


    [Header("RB2D")]    
    [SerializeField]
    private Rigidbody2D rb2d;        

    [Header("Collider2D")]
    [SerializeField]
    private BoxCollider2D c2d_knife;

    [Header("Transform")]
    
    public Transform t_player;
    [SerializeField]
    private Transform t_shootingPoint;
        
    [SerializeField]
    private Transform t_pistol;
    [SerializeField]
    private Transform t_knife_sprite;
    [SerializeField]
    private Transform t_knife;

    [Header("Sprite Renderer")]
    [SerializeField]
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    private Sprite look_up;
    [SerializeField]
    private Sprite look_down;
    [SerializeField]
    private Sprite look_x;
    [SerializeField]
    private SpriteRenderer sr_pistol;    
    [SerializeField]
    private Sprite pistol_sprite;
    [SerializeField]
    private SpriteRenderer sr_knife;
    [SerializeField]
    private Sprite knife_sprite;

    [Header("Audio")]
    [SerializeField]
    private AudioSource ammo_audio;
    [SerializeField]
    private AudioClip load_clip;
    [SerializeField]
    private AudioClip no_ammo_clip;
    [SerializeField]
    private AudioSource pick_gun;
    
    public AudioSource knife_audio;

    [SerializeField]
    private AudioClip whoosh_knife;
    [SerializeField]
    private AudioClip pick_knife;

    public AudioClip ammo_pick;
    public AudioClip slay_knife;
    

    [Header("Animation")]
    [SerializeField]
    private Animator animator;

    [Header("Text")]
    [SerializeField]
    private TMP_Text coin_output;
    [SerializeField]
    private TMP_Text tmp_text;
    [SerializeField]
    private TMP_Text shoots_output;
    [SerializeField]
    private TMP_SpriteAsset bullet_TMP;
    [SerializeField]
    private TMP_SpriteAsset knife_TMP;

    [Header("Local Variables")]
    [SerializeField]
    private bool[] eq;
    
    [SerializeField]
    private float thrust;
    [SerializeField]
    private int lvl_inc = 1;
    public int enemys_slayed = 0;
    public int coins_num = 0;
    public int start_health = 5;
    public int health = 5;
    public bool onlyOnce = true;
    public bool checkOneMoreThing = true;
    public int shoots_left = 10;
    private string zwrot = "right";
    public bool knife = false;
    public bool pistol = false;
    public float pistol_on_player = 0.86f;
    private string knife_local_zwrot = "right";
    IEnumerator animation()
    {
        checkOneMoreThing = false;
        yield return new WaitForSeconds(0.1f);
        onlyOnce = false;
    }
    IEnumerator slaywithknife()
    {
        knife_obj.GetComponent<Collider2D>().enabled = true;
        yield return new WaitForSeconds(0.25f);
        knife_obj.GetComponent<Collider2D>().enabled = false;
    }
    
    private void FixedUpdate()
    {
        //eq | 0 -> knife, 1 -> pistol


        //num of coins
        coin_output.text = "<sprite name=coin> " + coins_num;

        //hearts left
        tmp_text.text = "Health:<br>";

        //shoots left
        if (pistol)
        {
            shoots_output.rectTransform.localPosition = new Vector3(-116.6f, 1.7f, 0f);
            shoots_output.fontSize = 12;
            shoots_output.spriteAsset = bullet_TMP;
            shoots_output.text = "<sprite name=bullet_3>" + shoots_left;
        }            
        else if (knife)
        {
            shoots_output.rectTransform.localPosition = new Vector3(-136.6f, -1f, 0f);
            shoots_output.fontSize = 20;
            shoots_output.spriteAsset = knife_TMP;
            shoots_output.text = "<sprite name=fighting knife_3>∞";
        }            
        else
        {
            shoots_output.text = "";
        }

        for (int i = 0; i < health; i++)
        {
            tmp_text.text += "<sprite name=hearths_0> ";
        }
        for (int i = 0; i < start_health - health; i++)
        {
            if (i == 0)
            {
                if (!onlyOnce)
                {
                    tmp_text.text += "<sprite name=hearths_3> ";
                }
                if (onlyOnce && checkOneMoreThing)
                {
                    StartCoroutine(animation());
                }
                if (onlyOnce && !checkOneMoreThing)
                {
                    tmp_text.text += "<sprite name=hearths_2> ";
                }
            }
            else
            {
                tmp_text.text += "<sprite name=hearths_3> ";
            }
        }

        //if health equals 0 then preload scene
        if (health <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }


        //weapons
        if (Input.GetKey("1"))
        {
            if(!pistol)
                pick_gun.Play();

            pistol = true;
            knife = false;
            
        }
        if (Input.GetKey("2"))
        {
            if (!knife)
            {
                knife_audio.clip = pick_knife;
                knife_audio.Play();
            }                
            knife = true;
            pistol = false;
        }
        if (Input.GetKey("3"))
        {
            pistol = false;
            knife = false;
        }

        if (!pistol)
        {
            sr_pistol.sprite = null;
        }
        if (pistol)
        {
            sr_pistol.sprite = pistol_sprite;
            sr_pistol.size = new Vector2(0.8f, 0.6f);
        }
        if (knife)
        {
            sr_knife.enabled = true;
        }
        if (!knife)
        {
            knife_obj.GetComponent<Collider2D>().enabled = false;
            sr_knife.enabled = false;            
        }

        //movement
        if (Input.GetKey("w"))
        {
            if (zwrot == "down")
            {
                t_shootingPoint.Rotate(0f, 0f, 180f);
                t_pistol.Rotate(0f, 0f, 180f);
            }
            if (zwrot == "right")
            {
                t_shootingPoint.Rotate(0f, 0f, 90f);
                t_pistol.Rotate(0f, 0f, 90f);
            }
            if (zwrot == "left")
            {
                t_shootingPoint.Rotate(0f, 0f, -90f);
                t_pistol.Rotate(180f, 0f, -90f);
            }            
            t_shootingPoint.localPosition = new Vector2(-0.14f, 1.25f);
            t_pistol.localPosition = new Vector2(0f, pistol_on_player);
            spriteRenderer.sprite = look_up;
            rb2d.AddForce(t_player.up * thrust * Time.deltaTime);
            zwrot = "up";
        }
        if (Input.GetKey("s"))
        {
            if (zwrot == "up")
            {
                t_shootingPoint.Rotate(0f, 0f, -180f);
                t_pistol.Rotate(0f, 0f, -180f);
            }
            if (zwrot == "right")
            {
                t_shootingPoint.Rotate(0f, 0f, -90f);
                t_pistol.Rotate(0f, 0f, -90f);
            }
            if (zwrot == "left")
            {
                t_shootingPoint.Rotate(0f, 0f, 90f);
                t_pistol.Rotate(180f, 0f, 90f);
            }
            t_shootingPoint.localPosition = new Vector2(0.14f, -1.25f);
            t_pistol.localPosition = new Vector2(0f, -pistol_on_player);
            spriteRenderer.sprite = look_down;
            rb2d.AddForce(t_player.up * -thrust * Time.deltaTime);
            zwrot = "down";
        }
        if (Input.GetKey("a"))
        {            
            if (zwrot == "up")
            {
                t_shootingPoint.Rotate(0f, 0f, 90f);
                t_pistol.Rotate(180f, 0f, -90f);
            }
            if (zwrot == "right")
            {
                t_shootingPoint.Rotate(0f, 0f, 180f);
                t_pistol.Rotate(180f, 0f, 180f);
            }
            if (zwrot == "down")
            {
                t_shootingPoint.Rotate(0f, 0f, 270f);
                t_pistol.Rotate(180f, 0f, -270f);
            }
            if(knife_local_zwrot == "right")
            {
                t_knife_sprite.eulerAngles = new Vector3(0f, 0f, 180f);
                sr_knife.flipX = true;
                sr_knife.flipY = true;
                c2d_knife.offset = new Vector2(-1.7f, 0f);
                t_knife_sprite.localPosition = new Vector2(-1.3f, -0.11f);                
            }
            
            t_shootingPoint.localPosition = new Vector2(-1.25f, 0.14f);            
            t_pistol.localPosition = new Vector2(-pistol_on_player, 0f);
            spriteRenderer.sprite = look_x;
            spriteRenderer.flipX = true;
            rb2d.AddForce(t_player.right * -thrust * Time.deltaTime);

            zwrot = "left";
            knife_local_zwrot = "left";
        }
        if (Input.GetKey("d"))
        {
            if (zwrot == "up")
            {
                t_shootingPoint.Rotate(0f, 0f, -90f);
                t_pistol.Rotate(0f, 0f, -90f);
            }
            if (zwrot == "left")
            {
                t_shootingPoint.Rotate(0f, 0f, -180f);
                t_pistol.Rotate(180f, 0f, -180f);
            }
            if (zwrot == "down")
            {
                t_shootingPoint.Rotate(0f, 0f, -270f);
                t_pistol.Rotate(0f, 0f, -270f);
            }
            if(knife_local_zwrot == "left")
            {
                t_knife_sprite.eulerAngles = new Vector3(0f, 0f, 180f);
                sr_knife.flipX = false;
                sr_knife.flipY = true;
                c2d_knife.offset = new Vector2(0.2f, 0f);
                t_knife_sprite.localPosition = new Vector2(-0.2f, -0.11f);                
            }   
            
            t_shootingPoint.localPosition = new Vector2(1.25f, 0.14f);
            t_pistol.localPosition = new Vector2(pistol_on_player, 0f);
            spriteRenderer.sprite = look_x;
            spriteRenderer.flipX = false;
            rb2d.AddForce(t_player.right * thrust * Time.deltaTime);

            zwrot = "right";
            knife_local_zwrot = "right";
        }

        //spowolnienie podczas pchania
        if (Input.GetKey("left shift"))
        {
            thrust = 2500;
        }
        else
        {
            thrust = 5000;
        }

        if (Input.GetKeyDown("space"))
        {
            if(shoots_left <= 0 && pistol)
            {
                ammo_audio.clip = no_ammo_clip;
                ammo_audio.Play();
            }
            if (knife)
            {
                animator.SetTrigger("attack");
                StartCoroutine(slaywithknife());
                knife_audio.clip = whoosh_knife;
                knife_audio.Play();
            }
        }

        if (Input.GetKey("escape"))
        {
            Application.LoadLevel("Menu");
        }
        
    }
}


