using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AgresiveEnemy : MonoBehaviour
{
    private const float NEXT_WAYPOINT_THRESHOLD = 0.075f;

    [Header("GameObject")]
    [SerializeField]
    private GameObject ag_enemy;

    [Header("LayerMask")]
    private LayerMask wall;

    [Header("Sprites")]
    [SerializeField]
    private SpriteRenderer agresive_enemy;
    [SerializeField]
    private SpriteRenderer agresive_enemy_healthbar;
    [SerializeField]
    private Sprite evil;
    [SerializeField]
    private Sprite joker;
    [SerializeField]
    private Canvas healthbar_canvas;

    [Header("Transform")]    
    private Transform t_player;
    [SerializeField]
    private Transform t_agresive_enemy;    

    private Collider2D c2d_player;
    
    [Header("RigidBody2D")]
    [SerializeField]
    private Rigidbody2D rb2d_ag_enemy;    

    [Header("Vector")]
    [SerializeField]
    private Vector2[] path;
    [SerializeField]
    private Vector3 posAfterCol;

    [Header("Audio")]
    [SerializeField]
    private AudioSource cruel_death;

    [Header("Local Variables")]
    [SerializeField]
    private float speed = 100f;
    [SerializeField]
    private float speedWhileChasing = 100f;
    [SerializeField]
    private int enemy_health = 2;
    private int enemy_health_actual = 2;
    private int pathIndex = 0;
    private bool reverse = false;
    private bool isAngry = false;

    [SerializeField]
    private Image healthbar;

    public PlayerInteraction pi;

    public Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        t_player = GameObject.FindWithTag("Player").transform;
        c2d_player = t_player.GetComponent<Collider2D>();
        pi = t_player.GetComponent<PlayerInteraction>();
        wall = LayerMask.GetMask("ToStopLaser");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isAngry)
        {
            if (Vector2.Distance(t_player.position, t_agresive_enemy.position) > 9)            
                healthbar_canvas.GetComponent<Canvas>().enabled = false;            
            else            
                healthbar_canvas.GetComponent<Canvas>().enabled = true;
            
            if (Vector2.Distance(t_player.position, t_agresive_enemy.position) > 5)
            {
                GoBackToPath();
            }            
            else
            {

                agresive_enemy.sprite = joker;

                Vector2 dir = (Vector2)t_player.position - (Vector2)transform.position;
                dir.Normalize();

                rb.AddForce(dir * speedWhileChasing * Time.fixedDeltaTime);

                if (Vector2.Distance(transform.position, t_player.position) < NEXT_WAYPOINT_THRESHOLD)
                {
                    transform.position = path[pathIndex];

                }

            }
        }
        else
        {
            GoBackToPath();
        }
        
        if (rb2d_ag_enemy.IsTouching(c2d_player))
        {
            cruel_death.Play();
            t_player.position = posAfterCol;
            pi.health--;
            pi.onlyOnce = true;
            pi.checkOneMoreThing = true;
        }
        

        if (rb2d_ag_enemy.IsTouchingLayers(wall) && rb2d_ag_enemy.velocity == Vector2.zero)
        {            
            StartCoroutine(OhGodIHitWall());
        }
      
    }
    public void TakeDamage (int damage)
    {
        enemy_health_actual -= damage;

        if(enemy_health_actual <= 0)
        {
            StartCoroutine(ShowRedBeforeDead());
        }

        float hp_perc = (float)enemy_health_actual / (float)enemy_health;


        healthbar.fillAmount = hp_perc;
        healthbar.color = Color.Lerp(Color.red, Color.green, hp_perc);

        
    }
    IEnumerator ShowRedBeforeDead()
    {        
        yield return new WaitForSeconds(0.1f);
        pi.enemys_slayed++;
        Destroy(gameObject);
    }
    IEnumerator OhGodIHitWall()
    {
        isAngry = true;
        yield return new WaitForSeconds(1.5f);
        isAngry = false;
        
    }

    void GoBackToPath()
    {
        agresive_enemy.sprite = evil;

        Vector2 dir = path[pathIndex] - (Vector2)transform.position;
        dir.Normalize();

        rb.AddForce(dir * speed * Time.fixedDeltaTime);

        if (Vector2.Distance(transform.position, path[pathIndex]) < NEXT_WAYPOINT_THRESHOLD)
        {
            transform.position = path[pathIndex];

            pathIndex += reverse ? -1 : 1;

            if (pathIndex >= path.Length - 1 || pathIndex <= 0)
            {
                reverse = !reverse;
            }
        }
        if(rb2d_ag_enemy.velocity == Vector2.zero)
        {
            transform.position = new Vector3(51f, -13f, 15f);
        }
    }
}
