using UnityEngine;

public class ResetPlane : MonoBehaviour
{
    public float resetPlaneY = -100;

    private Vector3 originalPosition;

    private void Awake()
    {
        originalPosition = transform.position;
    }

    private void Update()
    {
        if (transform.position.y < resetPlaneY)
        {
            transform.position = originalPosition;
        }
    }
}
