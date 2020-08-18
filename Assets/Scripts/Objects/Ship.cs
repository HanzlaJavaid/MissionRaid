using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Experimental.Rendering.LWRP;
public class Ship : MonoBehaviour
{
    ///Inputs
    public FixedJoystick Joystick;
    public JoyButton button;
    public JoyButton button2;
    ///UI
    [SerializeField] Text scoreCard;
    public handle Bar;
    public handle healthBar;
    [SerializeField] GameObject rep;
    //Particles
    [SerializeField] ParticleSystem exp;
    [SerializeField] Light2D jetFire;
    [SerializeField] ParticleSystem sm;
    ///Variables
    [SerializeField] float Speed = 1f;
    float Health = 100f;
    [SerializeField] float pitchFactor = 1f;
    [SerializeField] float jetPower = 1f;
    float ScoreTime;
    public float widthOfCollider;
    float[] defaults = new float[3];
    ///Components
    [SerializeField] Rigidbody2D rb;
    ///Bullets
    GameObject casualFire;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] public RectTransform baseui;
    ///Bools
    public bool isAlive = true;
    bool locked = false;
    ///Timers
    Timer ReloadTimer;
    ///States
    PlayerStates CurrentState = PlayerStates.start;
    [SerializeField] GameObject end;

    ///HandleFunctions
    private void Init()
    {
        widthOfCollider = gameObject.GetComponent<BoxCollider2D>().size.x;
        ReloadTimer = gameObject.AddComponent<Timer>();
        ReloadTimer.Duration = 2f;
        ReloadTimer.Run();
        casualFire = transform.Find("bullet").gameObject;
        defaults[0] = jetFire.pointLightInnerRadius;
        defaults[1] = jetFire.pointLightOuterRadius;
        defaults[2] = jetFire.intensity;
        sm = transform.Find("Smoke").GetComponent<ParticleSystem>();
    }
    private void HandleUI()
    {
        Bar.SetSize(ReloadTimer.CurrentTime * 2);
        healthBar.SetSize(Health / 25);
        scoreCard.text = ((int)(ScoreTime)).ToString();
    }

    void jetHandle()
    {
        if (locked == false)
        {
            if (Joystick.Vertical > 0 && transform.position.y <= Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height)).y - widthOfCollider / 3 && rb.velocity.y > 0)
            {
                transform.Rotate(new Vector3(0, 0, pitchFactor * rb.velocity.y / 8) * Time.deltaTime);
            }
            if (Joystick.Vertical < 0 && transform.position.y >= Camera.main.ScreenToWorldPoint(baseui.sizeDelta).y + widthOfCollider && rb.velocity.y < 0)
            {
                transform.Rotate(new Vector3(0, 0, pitchFactor * rb.velocity.y / 8) * Time.deltaTime);
            }
        }
        if (Joystick.Vertical == 0 || transform.position.y >= Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height)).y - widthOfCollider / 3 || transform.position.y <= Camera.main.ScreenToWorldPoint(baseui.sizeDelta).y + widthOfCollider)
        {
            if (transform.rotation.eulerAngles.z > 0 && transform.rotation.eulerAngles.z < 90)
            {
                transform.Rotate(new Vector3(0, 0, -pitchFactor * 4f) * Time.deltaTime);
            }
            if (transform.rotation.eulerAngles.z < 360 && transform.rotation.eulerAngles.z > 270)
            {
                transform.Rotate(new Vector3(0, 0, +pitchFactor * 4f) * Time.deltaTime);
            }
        }
        if (rb.velocity.x > 0 && jetFire.intensity <= 250 && jetFire.pointLightOuterRadius <= 10)
        {
            if (jetFire.intensity <= 0.0f || jetFire.pointLightOuterRadius <= 0.1f)
            {
                jetFire.intensity += 30 * jetPower;
                jetFire.pointLightOuterRadius = defaults[1];
            }
            jetFire.intensity += 15 * jetPower;
            jetFire.pointLightOuterRadius += 0.03f * jetPower;
        }
        if (rb.velocity.x < 0)
        {
            if (jetFire.intensity > 0)
            {
                jetFire.intensity -= 15 * jetPower;
            }
            if (jetFire.pointLightOuterRadius > 0)
            {
                jetFire.pointLightOuterRadius -= 0.03f * jetPower;
            }
        }
        else if (rb.velocity.x == 0)
        {
            if (jetFire.intensity > defaults[2])
            {
                jetFire.intensity -= 10 * jetPower;
            }
            if (jetFire.intensity < defaults[2])
            {
                jetFire.intensity += 10 * jetPower;
            }
            if (jetFire.pointLightOuterRadius > defaults[1])
            {
                jetFire.pointLightOuterRadius -= 0.1f * jetPower;
            }
            if (jetFire.pointLightOuterRadius < defaults[1])
            {
                jetFire.pointLightOuterRadius += 0.1f * jetPower;
            }
        }
    }

    void HandleInput()
    {
        if (CurrentState == PlayerStates.alive)
        {
            HandleFire();
            rb.velocity = Vector2.up * (Joystick.Vertical + Input.GetAxis("Vertical")) * Speed + Vector2.right * (Joystick.Horizontal + Input.GetAxis("Horizontal")) * Speed;
        }
    }
    void HandleFire()
    {
        if (CurrentState!=PlayerStates.alive)
        {
            casualFire.SetActive(false);
        }
        if (button.pressed && ReloadTimer.Finished)
        {
            GameObject bullet = Instantiate(bulletPrefab) as GameObject;
            Physics2D.IgnoreCollision(bullet.GetComponent<BoxCollider2D>(), gameObject.GetComponent<BoxCollider2D>(), true);
            bullet.transform.position = transform.Find("FirePoint").position;
            bullet.GetComponent<missile>().Go(transform.rotation.eulerAngles.z);
            ReloadTimer.Run();
            AudioManager.Play(AudioClipName.shoot);
        }
        if (button2.pressed)
        {
            casualFire.SetActive(true);
        }
        else
        {
            casualFire.SetActive(false);
        }
    }

    ///GeneralFunctions
    void Bound()
    {
        if (CurrentState == PlayerStates.alive)
        {
            if (transform.position.x >= Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x - widthOfCollider / 2)
            {
                transform.position = new Vector2(Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x - widthOfCollider / 2, transform.position.y);
                rb.velocity = Vector2.zero;
            }
            if (transform.position.x <= Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).x + widthOfCollider / 2)
            {
                transform.position = new Vector2(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).x + widthOfCollider / 2, transform.position.y);
                rb.velocity = Vector2.zero;
            }
            if (transform.position.y >= Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height)).y - widthOfCollider / 3)
            {
                transform.position = new Vector2(transform.position.x, Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height)).y - widthOfCollider / 3);
                rb.velocity = Vector2.zero;
            }
            if (transform.position.y <= Camera.main.ScreenToWorldPoint(baseui.sizeDelta).y + widthOfCollider)
            {
                transform.position = new Vector2(transform.position.x, Camera.main.ScreenToWorldPoint(baseui.sizeDelta).y + widthOfCollider);
                rb.velocity = Vector2.zero;
            }
            else
            {
                locked = false;
            }
        }
    }

    void EndSequence()
    { 
        transform.Rotate(new Vector3(0, 0, pitchFactor * 10f * Time.deltaTime));
        Vector2 forceVector = new Vector2(Mathf.Cos(transform.eulerAngles.z * (Mathf.PI / 180)), Mathf.Sin(transform.eulerAngles.z * (Mathf.PI / 180)));
        rb.velocity = forceVector * 1000 * Time.deltaTime;
        GameObject g = transform.parent.gameObject.transform.Find("backgrounds").gameObject;
        g.transform.position = new Vector2(g.transform.position.x, g.transform.position.y - 1f);
    }
    private void SelfDestroy()
    {
        jetFire.intensity = 0;
        exp.Play();
        sm.Play();
        rb.gravityScale = 1;
        CurrentState = PlayerStates.dead;
        rep.SetActive(true);
    }
    void StartSeq()
    {
        if(transform.position.x <= Camera.main.ScreenToWorldPoint( new Vector2(Screen.width/2,0)).x && CurrentState == PlayerStates.start)
        {
            rb.velocity = Vector2.right * 700 * Time.deltaTime;
        }
        else
        {
            CurrentState = PlayerStates.alive;
        }
    }
    public void damageReport()
    {
        Health -= 25f;
    }

    private void Start()
    {
        Init();
    }

    

    private void Update()
    {
        HandleUI();
        if (Health <= 0 && CurrentState == PlayerStates.alive)
        {
            SelfDestroy();
        }
        if (CurrentState == PlayerStates.alive)
        {
            ScoreTime += Time.deltaTime;
            jetHandle();
            Bound();
            HandleInput();
        }
        if(CurrentState == PlayerStates.levelcomplete)
        {
            EndSequence();
        }
        if(CurrentState == PlayerStates.start)
        {
            StartSeq();
        }

    }

    

    
   
    private void OnCollisionStay2D(Collision2D collision)
    {
        SelfDestroy();
        if (collision.gameObject.tag == "eMissile")
        {
            Destroy(collision.collider.gameObject);
            AudioManager.Play(AudioClipName.playerdie);
        }
    }

    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "score")
        {
            Destroy(collision.gameObject);
            ScoreTime += 10;
        }
        if(collision.gameObject.tag == "END")
        {
            if(CurrentState == PlayerStates.alive)
            {
                AudioManager.Play(AudioClipName.blast);
                Destroy(collision.gameObject);
                CurrentState = PlayerStates.levelcomplete;
                end.SetActive(true);
            }
        }
    }
    
}
