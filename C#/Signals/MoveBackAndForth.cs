using UnityEngine;

public class MoveBackAndForth : MonoBehaviour
{
    public float speed = 2f;  // Speed of movement
    public float distance = 3f;  // Distance to move back and forth

    private Vector3 startPosition;

    void Start()
    {
        // Store the initial position of the GameObject
        startPosition = transform.position;
    }

    void Update()
    {
        // Move the GameObject back and forth along the X-axis
        float movement = Mathf.PingPong(Time.time * speed, distance);
        transform.position = startPosition + new Vector3(movement, 0, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Car")
        {
            Invoke("DisableArrow", 0.3f);
        }
    }


    private void DisableArrow()
    {
        gameObject.SetActive(false);
    }
}
