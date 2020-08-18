using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MusicCon : MonoBehaviour
{
    Timer loadTime;
    [SerializeField] AudioSource music;
    [SerializeField] AudioSource sfx;
    private void Awake()
    {
        music = gameObject.GetComponent<AudioSource>();
        music.Play();
        DontDestroyOnLoad(gameObject);
        loadTime = gameObject.AddComponent<Timer>();
        loadTime.Duration = 2f;
        loadTime.Run();
        AudioManager.Initialize(sfx);
    }
    private void Update()
    {
        if(loadTime.Finished == true && SceneManager.GetActiveScene().buildIndex==0)
        {
            int curscene = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(1);
        }
    }

}
