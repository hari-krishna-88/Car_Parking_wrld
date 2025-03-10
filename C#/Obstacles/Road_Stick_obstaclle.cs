using UnityEngine;

public class Road_Stick_obstaclle : MonoBehaviour
{
    public float moveDistance = 2f; // Fixed distance to move in the y-direction
    public float moveSpeed = 2f; // Speed of the movement
    public bool up = false; // Direction flag
    private Vector3 targetPosition;
    private Vector3 originalPosition; // To store the original position
    private bool isMoving = false; // Flag to check if the object is moving
    private bool hasMovedUp = false; // Flag to check if the object has moved up

    void Start()
    {
        // Store the original position
        originalPosition = transform.position;
    }

    void Update()
    {
        // If movement is not active, start the movement based on 'up'
        if (!isMoving)
        {
            if (up && !hasMovedUp)
            {
                // Move upwards if 'up' is true and it hasn't moved up yet
                targetPosition = transform.position + new Vector3(0, moveDistance, 0);
                hasMovedUp = true; // Mark as moved up
                isMoving = true;
            }
            else if (!up && transform.position != originalPosition)
            {
                // If 'up' is false and the object is not at the original position, move back
                targetPosition = originalPosition;
                isMoving = true;
            }
        }

        // Move towards the target position smoothly
        transform.position = Vector3.Lerp(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        // Check if the GameObject has reached the target position
        if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
        {
            // Once it reaches the target, stop moving
            isMoving = false;
        }
    }
}