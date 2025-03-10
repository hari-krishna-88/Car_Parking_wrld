using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RoadStick_Remove : MonoBehaviour
{
    public float targetY = 5f;
    public float speed = 2f;
    private float startY;
    private float t = 0f;

    private void Start()
    {
        startY = transform.position.y;
    }

    private void Update()
    {
        t += Time.deltaTime * speed;
        float newY = Mathf.Lerp(startY, targetY, t);
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);

        if (Mathf.Abs(transform.position.y - targetY) < 0.01f)
        {
            enabled = false;
        }
    }
}
