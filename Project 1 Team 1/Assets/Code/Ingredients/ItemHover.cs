using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHover : MonoBehaviour
{
    public float amplitude = 0.5f;
    public float frequency = 1;

    Vector3 originalPos;

    void Start()
    {
        originalPos = transform.position;
    }

    void Update()
    {
        float b = (2 * Mathf.PI) / frequency;
        float a = amplitude;
        Vector3 newPos = originalPos;
        newPos.y += (a * Mathf.Sin(Time.time * b));
        transform.position = newPos;
    }
}
