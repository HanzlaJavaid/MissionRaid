using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
public class PLLA : MonoBehaviour,IPointerDownHandler,IPointerUpHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        SceneManager.LoadScene(2);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
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
