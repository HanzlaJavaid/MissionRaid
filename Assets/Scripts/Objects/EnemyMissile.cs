using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMissile : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] Rigidbody2D rb;
    GameObject p;
    bool isSeek = false;
    // Start is called before the first frame update
    private void Start()
    {
        p = GameObject.FindGameObjectWithTag("Player");
    }
    public void Go(float dir)
    {
        transform.Rotate(new Vector3(0, 0, dir));
        Vector2 ForceDir = new Vector2(Mathf.Cos(transform.rotation.eulerAngles.z * (Mathf.PI / 180)), Mathf.Sin(transform.rotation.eulerAngles.z * (Mathf.PI / 180)));
        rb.AddForce(ForceDir * speed , ForceMode2D.Impulse);
    }
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
    private void Update()
    {
        if (isSeek == false)
        {
            HeatSeek();
        }
        if (transform.position.x < (Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).x - (gameObject.GetComponent<BoxCollider2D>().size.x) / 2))
        {
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "score" || collision.gameObject.tag == "Enemy")
        {
            Physics2D.IgnoreCollision(collision.gameObject.GetComponent<BoxCollider2D>(), gameObject.GetComponent<BoxCollider2D>(), true);
        }
    }
    void HeatSeek()
    {
        float dist = EssentiaFunctions.getRange(p, gameObject);
        if(dist < 2 && isSeek == false)
        {
            isSeek = true;
            rb.velocity = Vector2.zero;
            transform.eulerAngles = EssentiaFunctions.getAngleTowardsPoint(p, gameObject);
            float newDir = transform.eulerAngles.z * (Mathf.PI / 180);
            Vector2 forcevector = new Vector2(Mathf.Cos(newDir), Mathf.Sin(newDir));
            rb.AddForce(forcevector * speed, ForceMode2D.Impulse);
        }
    }
}
