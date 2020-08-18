using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class missile : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] Rigidbody2D rb;
    GameObject[] enemies;
    bool isSeek = false;
    // Start is called before the first frame update
    public void Go(float dir)
    {
        transform.Rotate(new Vector3(0,0,dir));
        Vector2 ForceDir = new Vector2(Mathf.Cos(transform.rotation.eulerAngles.z*(Mathf.PI/180)), Mathf.Sin(transform.rotation.eulerAngles.z * (Mathf.PI / 180)));
        rb.AddForce(ForceDir * speed, ForceMode2D.Impulse);
    }
    private void Start()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
    }
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
    private void Update()
    {
        if(isSeek == false)
        {
            HeatSeek();
        }
    }
    void HeatSeek()
    {
        GameObject nearest = null;
        float dist = 100;
        for (int i = 0; i < enemies.Length; i++)
        {
            float localdist = Vector2.Distance(enemies[i].transform.position, gameObject.transform.position);
            if(localdist < dist)
            {
                dist = localdist;
                nearest = enemies[i];
            }
        }
        if (dist < 3.5 && isSeek == false)
        {
            isSeek = true;
            rb.velocity = Vector2.zero;
            float Direction = Mathf.Atan2(nearest.transform.position.y - transform.position.y, nearest.transform.position.x - transform.position.x);
            print(Direction * (180 / Mathf.PI));
            transform.eulerAngles = new Vector3(0, 0, Direction * (180 / Mathf.PI));
            rb.AddForce(new Vector2(speed * Mathf.Cos(Direction), speed * Mathf.Sin(Direction)), ForceMode2D.Impulse);
        }
    }
}
