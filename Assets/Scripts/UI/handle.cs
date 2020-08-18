using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class handle : MonoBehaviour
{
    [SerializeField] RectTransform bar;

    public void SetSize(float t)
    {
        bar.sizeDelta = new Vector2(t * 100, 10);
    }
}
