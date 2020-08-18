using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GamePlay : MonoBehaviour
{
    public GameObject EndTrigger;
    public GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        EndTrigger = GameObject.FindGameObjectWithTag("END");
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
    }
}
