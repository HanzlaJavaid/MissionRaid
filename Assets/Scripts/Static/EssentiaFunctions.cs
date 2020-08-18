using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EssentiaFunctions{
    public static Vector3 getAngleTowardsPoint(GameObject targetObject,GameObject baseObject)
    {
        float dir = Mathf.Atan2(targetObject.transform.position.y - baseObject.transform.position.y, targetObject.transform.position.x - baseObject.transform.position.x);
        return new Vector3(0, 0, dir*Mathf.Rad2Deg);
       
    }
    public static float getRange(GameObject targetObject,GameObject baseObject)
    {
        //GameObject cam = GameObject.FindGameObjectWithTag("MainCamera");
        //Vector2 a = new Vector2(targetObject.transform.localPosition.x + cam.transform.position.x, targetObject.transform.localPosition.y + cam.transform.position.y);
        return Vector2.Distance(targetObject.transform.position, baseObject.transform.position);
    }
}
