using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LoadNextLevel : MonoBehaviour
{
    void LoadLevel()
    {
        if (gameObject.tag == "levelcomplete")
        {
            if (SceneManager.GetActiveScene().buildIndex != 3)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
            else if (SceneManager.GetActiveScene().buildIndex == 3)
            {
                SceneManager.LoadScene(1);
            }
        }
        else if(gameObject.tag == "gameover")
        {
            print("nothing to do");
        }
    }
}
