using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructionHandle : MonoBehaviour
{

    [SerializeField] string tagtolook = "";
    [SerializeField] GameObject[] Desprefabs;
    [SerializeField] float time;
    Timer DesTimer;
    // Start is called before the first frame update
    void Start()
    {
        DesTimer = gameObject.AddComponent<Timer>();
        DesTimer.Duration = time;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == tagtolook)
        {
            for (int i = 0; i <= 1; i++)
            {
                print(i);
                GameObject x = Instantiate(Desprefabs[i], new Vector2(transform.position.x, transform.position.y), Quaternion.identity) as GameObject;
                Rigidbody2D b = collision.collider.gameObject.GetComponent<Rigidbody2D>();
                Vector2 forceVector = new Vector2(b.velocity.x + Random.Range(-10f, 10f), b.velocity.y + Random.Range(-10f, 10f)) * 0.5f;
                print(forceVector);
                x.GetComponent<Rigidbody2D>().AddForce(forceVector, ForceMode2D.Impulse);
            }
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
