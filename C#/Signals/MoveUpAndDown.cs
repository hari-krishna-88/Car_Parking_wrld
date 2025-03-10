using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveUpAndDown : MonoBehaviour
{
    public float speed = 2f;  // Speed of movement
    public float distance = 3f;  // Distance to move back and forth

    private Vector3 startPosition;
    public bool KeyCollected = false;

    void Start()
    {
        // Store the initial position of the GameObject
        startPosition = transform.position;
    }

    void Update()
    {
        // Move the GameObject back and forth along the X-axis
        float movement = Mathf.PingPong(Time.time * speed, distance);
        transform.position = startPosition + new Vector3(0, movement, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Car" && !KeyCollected)
        {
            KeyCollected = true;
            Invoke("DisableArrow", 0.3f);
        }
    }


    private void DisableArrow()
    {
        gameObject.SetActive(false);
    }
}
