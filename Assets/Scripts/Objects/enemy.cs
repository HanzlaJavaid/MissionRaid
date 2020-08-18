using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;
public class enemy : MonoBehaviour
{
    public GameObject p;
    [SerializeField] Rigidbody2D rb;
    Timer Initialization;
    [SerializeField] Light2D jetFire;
    [SerializeField] ParticleSystem exp;
    [SerializeField] ParticleSystem smoke;
    ParticleSystem smallsmoke;
    public bool isAlive = true;
    float health = 100f;
    bool audioPasstoPlay = true;
    // Start is called before the first frame update
    void Start()
    {
        Initialization = gameObject.AddComponent<Timer>();
        Initialization.Duration = 1.5f;
        Initialization.Run();
        smallsmoke = transform.Find("smallsmoke").GetComponent<ParticleSystem>();
    }  

    // Update is called once per frame
    void Update()
    {
        if (isAlive == true)
        { 
            ReportDamage();
            if(health <= 0)
            {
                SelfDestroy();
            }
        }
        else
        {
            gameObject.layer = 10;
        }
        //if(EssentiaFunctions.getRange(p,gameObject)<5f && audioPasstoPlay == true)
        {
           // AudioManager.Play(AudioClipName.pass1);
           // audioPasstoPlay = false;
        }
        
    }


    private void ReportDamage()
    {
        if (transform.position.x < (Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).x - (gameObject.GetComponent<BoxCollider2D>().size.x) / 2))
        {
            p.GetComponent<Ship>().damageReport();
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        HandleCollision(collision);
    }

    private void HandleCollision(Collision2D collision)
    {
        if (collision.gameObject.tag == "missile")
        {
            Destroy(collision.collider.gameObject);
            SelfDestroy();

        }
        if (collision.gameObject.tag == "Enemy" && collision.gameObject.GetComponent<enemy>().isAlive == false)
        {
            SelfDestroy();
        }
        if (collision.gameObject.tag == "Player")
        {
            SelfDestroy();
        }
    }

    private void SelfDestroy()
    {
        exp.Play();
        smoke.Play();
        rb.gravityScale = 1;
        isAlive = false;
        jetFire.intensity = 0;
        gameObject.layer = 10;
        AudioManager.Play(AudioClipName.die);
    }

    

    private void OnParticleCollision(GameObject other)
    {
        if (other.tag == "part")
        {
            health -= 25f;
            smallsmoke.Play();
        }
    }
}
