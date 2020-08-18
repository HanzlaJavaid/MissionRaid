using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCon : MonoBehaviour
{
    [SerializeField] GameObject Cam;
    [SerializeField] float speed = 10f;
    // Update is called once per frame
    void FixedUpdate()
    {
        Cam.transform.position = new Vector3(Cam.transform.position.x + Time.deltaTime*speed, Cam.transform.position.y,Camera.main.transform.position.z);
    }
}
