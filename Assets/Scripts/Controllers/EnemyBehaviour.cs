using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] float rotationPower = 25f;
    [SerializeField] float velocityMultiplier = 150f;
    [SerializeField] GameObject MissilePrefab;
    [SerializeField] float targetAngle;
    bool isInitialized = false;
    public Flightodes flightode;
    Timer keyTimer1;
    Timer keyTimer2;
    enemy enemyObject;
    Rigidbody2D rb;
    GameObject p;
    Vector2 forceVector;
    bool tolook = true;
    [SerializeField] List<bool> canFire = new List<bool>();
    float secondTurn = 0f;
    [SerializeField] float d = 0f;
    void Start()
    {
        transform.eulerAngles = new Vector3(0, 0, 0);
        rb = gameObject.GetComponent<Rigidbody2D>();
        p = gameObject.GetComponent<enemy>().p;
        enemyObject = gameObject.GetComponent<enemy>();
        keyTimer1 = gameObject.AddComponent<Timer>();
    }
    private void OnBecameVisible()
    {
        Initialize();
    }

    private void Initialize()
    {
        rb.velocity = Vector2.zero;
        isInitialized = true;
        if (flightode == Flightodes.CurveUP)
        {
            Vector3 initialAngle = new Vector3(0, 0, 45f);
            transform.eulerAngles = initialAngle;
            keyTimer1.Duration = 0.2f;
            keyTimer1.Run();
            for(int i = 0; i< 3; i++)
            {
                canFire.Add(false);
            }
        }
        if (flightode == Flightodes.CurveDOWN)
        {
            gameObject.GetComponent<SpriteRenderer>().flipY = true;
            Vector3 initialAngle = new Vector3(0, 0, 315f);
            transform.eulerAngles = initialAngle;
            keyTimer1.Duration = 0.2f;
            keyTimer1.Run();
            for (int i = 0; i < 3; i++)
            {
                canFire.Add(true);
            }
        }
        if (flightode == Flightodes.TakeOFF)
        {
            forceVector = Vector2.right;
            keyTimer1.Duration = 0.15f;
            keyTimer1.Run();
            transform.eulerAngles = new Vector3(0, 0, 359);
            for (int i = 0; i < 2; i++)
            {
                canFire.Add(true);
            }
        }
        if (flightode == Flightodes.TakeDOWN)
        {
            forceVector = Vector2.right;
            keyTimer1.Duration = 0.15f;
            keyTimer1.Run();
            transform.eulerAngles = new Vector3(0, 0, 1);
            for (int i = 0; i < 2; i++)
            {
                canFire.Add(true);
            }
        }
        if (flightode == Flightodes.StraightAndSeek)
        {
            transform.eulerAngles = new Vector3(0, 0, EssentiaFunctions.getAngleTowardsPoint(p, gameObject).z - 180);
            keyTimer1.Duration = 3f;
            keyTimer2 = gameObject.AddComponent<Timer>();
            keyTimer2.Duration = 0.2f;
            for (int i = 0; i < 3; i++)
            {
                canFire.Add(true);
            }
            keyTimer1.Run();
            keyTimer2.Run();
        }
        if (flightode == Flightodes.Straight)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
            canFire.Add(true);
        }
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (isInitialized == true)
        {
            MovementBehaviour();
        }
    }

    private void MovementBehaviour()
    {
        if (enemyObject.isAlive == true)
        {

            if (flightode == Flightodes.CurveUP)
            {
                if (transform.eulerAngles.z != 315)
                {
                    transform.Rotate(new Vector3(0, 0, -rotationPower) * Time.deltaTime);
                }
                forceVector = new Vector2(Mathf.Cos(transform.eulerAngles.z * (Mathf.PI / 180)), Mathf.Sin(transform.eulerAngles.z * (Mathf.PI / 180)));
                rb.velocity = forceVector * -1 * velocityMultiplier * Time.deltaTime;
            }
            if (flightode == Flightodes.CurveDOWN)
            {
                if (transform.eulerAngles.z != 45)
                {
                    transform.Rotate(new Vector3(0, 0, +rotationPower) * Time.deltaTime);
                }
                forceVector = new Vector2(Mathf.Cos(transform.eulerAngles.z * (Mathf.PI / 180)), Mathf.Sin(transform.eulerAngles.z * (Mathf.PI / 180)));
                rb.velocity = forceVector * -1 * velocityMultiplier * Time.deltaTime;
            }
            if (flightode == Flightodes.TakeOFF)
            {
                rb.velocity = -1 * velocityMultiplier * Time.deltaTime * forceVector;
                if (EssentiaFunctions.getRange(p, gameObject) < 10 && transform.eulerAngles.z > 315)
                {
                    transform.Rotate(new Vector3(0, 0, -rotationPower * 1f) * Time.deltaTime);
                    forceVector = new Vector2(Mathf.Cos(transform.eulerAngles.z * (Mathf.PI / 180)), Mathf.Sin(transform.eulerAngles.z * (Mathf.PI / 180)));
                    rb.velocity = forceVector * -1 * velocityMultiplier * Time.deltaTime;
                }
            }
            if (flightode == Flightodes.TakeDOWN)
            {
                rb.velocity = -1 * velocityMultiplier * Time.deltaTime * forceVector;
                if (EssentiaFunctions.getRange(p, gameObject) < 10 && transform.eulerAngles.z < 45)
                {
                    transform.Rotate(new Vector3(0, 0, +rotationPower * 1f) * Time.deltaTime);
                    forceVector = new Vector2(Mathf.Cos(transform.eulerAngles.z * (Mathf.PI / 180)), Mathf.Sin(transform.eulerAngles.z * (Mathf.PI / 180)));
                    rb.velocity = forceVector * -1 * velocityMultiplier * Time.deltaTime;
                }
            }
            if (flightode == Flightodes.StraightAndSeek)
            {

                if (keyTimer1.Finished && tolook == true)
                {
                    tolook = false;
                    print(transform.eulerAngles.z);
                    secondTurn = EssentiaFunctions.getAngleTowardsPoint(p, gameObject).z + 180;
                }
                if (tolook == false)
                {
                    if (secondTurn > 270)
                    {
                        transform.Rotate(new Vector3(0, 0, -rotationPower * 1f) * Time.deltaTime);
                    }
                    if (secondTurn < 90)
                    {
                        transform.Rotate(new Vector3(0, 0, +rotationPower * 1f) * Time.deltaTime);
                    }
                }
                forceVector = new Vector2(Mathf.Cos(transform.eulerAngles.z * (Mathf.PI / 180)), Mathf.Sin(transform.eulerAngles.z * (Mathf.PI / 180)));
                rb.velocity = -1 * velocityMultiplier * Time.deltaTime * forceVector;
            }
            if (flightode == Flightodes.Straight)
            {
                rb.velocity = -1 * Vector2.right * Time.deltaTime * velocityMultiplier;
            }
        }
    }

    private void CombatBehaviour()
    {
        targetAngle = EssentiaFunctions.getAngleTowardsPoint(p, gameObject).z;
        targetAngle = targetAngle < 0 ? targetAngle + 360 : targetAngle;
        d = EssentiaFunctions.getRange(p, gameObject);
        if (flightode == Flightodes.StraightAndSeek)
        {
            if (d < 10 && (targetAngle >= 170 && targetAngle < 190) && keyTimer2.Finished == true)
            {
                Fire();
            }
        }
        if (flightode == Flightodes.Straight)
        {           
            if(d < 20 && (targetAngle >= 170 && targetAngle <= 190))
            {
                Fire();
            }
        }
        if (flightode == Flightodes.CurveUP ||flightode == Flightodes.CurveDOWN || flightode == Flightodes.TakeOFF || flightode == Flightodes.TakeDOWN)
        {
            targetAngle = EssentiaFunctions.getAngleTowardsPoint(p, gameObject).z;
            if (transform.eulerAngles.z < 360 && transform.eulerAngles.z > 270)
            {
                targetAngle = targetAngle + 360 - transform.eulerAngles.z;
                
            }
            if (transform.eulerAngles.z > 0 && transform.eulerAngles.z < 90)
            {
                targetAngle = targetAngle - transform.eulerAngles.z;
                
            }
            if (EssentiaFunctions.getRange(p,gameObject) < 10 && ((targetAngle > 150 && targetAngle <= 180) || (targetAngle < -150 && targetAngle > -180)) && keyTimer1.Finished == true)
            {
                Fire();
            }
        }
    }

    private void Fire()
    {
        canFire.RemoveAt(canFire.Count-1);
        GameObject bullet = Instantiate(MissilePrefab) as GameObject;
        Physics2D.IgnoreCollision(bullet.GetComponent<BoxCollider2D>(), gameObject.GetComponent<BoxCollider2D>(), true);
        bullet.transform.position = transform.Find("FirePoint").position;
        bullet.GetComponent<EnemyMissile>().Go(transform.rotation.eulerAngles.z + 180);
        if(flightode == Flightodes.CurveDOWN || flightode == Flightodes.CurveUP || flightode == Flightodes.TakeDOWN || flightode == Flightodes.TakeOFF)
        {
            keyTimer1.Run();
        }
        if(flightode == Flightodes.StraightAndSeek)
        {
            keyTimer2.Run();
        }
    }
    void Update()
    {
        if (canFire.Count>0 && isInitialized == true)
        {
            CombatBehaviour();
        }
    }
}
