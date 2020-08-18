using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    Timer SpawnTimer;
    Timer cloudSpawn;
    Timer cloudDuration;
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] GameObject onenose;
    GameObject p;
    [SerializeField] GameObject replay;
    [SerializeField] GameObject star;
    //[SerializeField] GameObject clouds;
    [SerializeField] ParticleSystem clouds;
    float len;
    int chooser = 0;
    bool isCloudsActive = false;
    [SerializeField] GameObject moon;
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    private void Init()
    {
        SpawnTimer = gameObject.AddComponent<Timer>();
        SpawnTimer.Duration = 4f;
        cloudSpawn = gameObject.AddComponent<Timer>();
        cloudDuration = gameObject.AddComponent<Timer>();
        cloudSpawn.Duration = Random.Range(5f, 25f);
        SpawnTimer.Run();
        cloudSpawn.Run();
        len = enemyPrefab.GetComponent<BoxCollider2D>().size.x;
    }

    private void StarCreationLegacy()
    {
        for (int i = 0; i < Random.Range(25, 100); i++)
        {
            Vector3 positiontospawn = new Vector3(Random.Range(0, Screen.width), Random.Range(0, Screen.height), -Camera.main.transform.position.z);
            positiontospawn = Camera.main.ScreenToWorldPoint(positiontospawn);
            GameObject x = Instantiate(star) as GameObject;
            x.transform.position = positiontospawn;
            DontDestroyOnLoad(x);
        }
    }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(moon);
        DontDestroyOnLoad(clouds.gameObject);
    }
    void HandleClouds()
    {
        if (cloudSpawn.Finished == true && isCloudsActive ==false)
        {
            clouds.Play();
            isCloudsActive = true;
            cloudDuration.Duration = Random.Range(0.1f, 4f);
            cloudDuration.Run();
        }
        if(cloudDuration.Finished == true && isCloudsActive == true)
        {
            isCloudsActive = false;
            clouds.Stop();
            cloudSpawn.Duration = Random.Range(5f, 25f);
            cloudSpawn.Run();
        }
    }

    // Update is called once per frame
    void Update()
    {
        HandleClouds();  
    }

    private void CoreGamePlayLegacy()
    {
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            if (p.GetComponent<Ship>().isAlive == false)
            {
                replay = GameObject.Find("Canvas").transform.Find("rep").gameObject;
                replay.SetActive(true);
            }

            if (SpawnTimer.Finished == true && p.GetComponent<Ship>().isAlive == true)
            {
                Vector3 positiontospawn = new Vector3(Screen.width + len / 2, Random.Range(p.GetComponent<Ship>().baseui.sizeDelta.y+p.GetComponent<Ship>().widthOfCollider*1.5f,Screen.height), -Camera.main.transform.position.z);
                positiontospawn = Camera.main.ScreenToWorldPoint(positiontospawn);
                GameObject[] count = GameObject.FindGameObjectsWithTag("Enemy");
                if (count.Length <= 2)
                {
                    chooser = Random.Range(0, 100);
                    print(chooser);
                    if (chooser < 70)
                    {
                        while (Physics2D.OverlapCircle(positiontospawn, 2) != null)
                        {
                            positiontospawn = new Vector3(Screen.width + len / 2, Random.Range(p.GetComponent<Ship>().baseui.sizeDelta.y + p.GetComponent<Ship>().widthOfCollider * 1.5f, Screen.height), -Camera.main.transform.position.z);
                            positiontospawn = Camera.main.ScreenToWorldPoint(positiontospawn);
                        }
                        GameObject x = Instantiate(enemyPrefab) as GameObject;
                        x.transform.position = positiontospawn;

                    }
                    else if (chooser > 80)
                    {
                        GameObject x = Instantiate(onenose) as GameObject;
                        x.transform.position = positiontospawn;
                    }
                }
                SpawnTimer.Duration = Random.Range(0.5f, 1.5f);
                SpawnTimer.Run();
            }
        }
    }
}
