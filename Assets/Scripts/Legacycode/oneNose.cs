using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class oneNose : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position +( Vector3.right * -1 * 0.2f);
        transform.Rotate(new Vector3(0,0,500f) * Time.deltaTime);
    }
    
}
