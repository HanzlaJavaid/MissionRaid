using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class replay : MonoBehaviour,IPointerUpHandler,IPointerDownHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
