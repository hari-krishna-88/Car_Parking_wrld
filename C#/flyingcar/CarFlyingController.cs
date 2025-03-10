using UnityEngine;

public class CarFlyingController : MonoBehaviour
{
    public float upwardForce = 10f;        // Force for moving upward
    public float downwardForce = 10f;      // Force for moving downward
    public float movementSpeed = 10f;      // Speed for forward, left, and right movement
    public float maxHeight = 20f;          // Maximum height the car can reach
    public float minHeight = 1f;           // Minimum height to stay above the ground
    public float floorCheckDistance = 2f;  // Desired distance to maintain above the floor
    public float floorAdjustmentSpeed = 5f; // Speed of adjustment to maintain floor distance
    public float rayOriginYOffset = 1f;    // Y offset for ray origin
    public float idleAmplitude = 0.2f;     // Amplitude of the idle floating motion
    public float idleFrequency = 1f;       // Frequency of the idle floating motion
    public float movementSmoothTime = 0.3f; // Time to smooth the movement (for linearity)

    private Rigidbody carRigidbody;
    private float idleOffset = 0f;         // Offset to create idle up/down movement
    private float lastTime = 0f;           // Time tracker for the idle motion

    private Vector3 currentVelocity = Vector3.zero; // To store the current velocity for smooth movement

    private void Awake()
    {
        carRigidbody = GetComponent<Rigidbody>();
    }

    void Start()
    {
        

        if (carRigidbody == null)
        {
            // Debug.LogError("No Rigidbody found! Please attach a Rigidbody to the car.");
        }

        // Lock rotation to prevent free rotation
        
    }

    void FixedUpdate()
    {
        HandleMovement();
        MaintainFloorDistance();
    }

    private void HandleMovement()
    {
        // Start with a zero velocity
        Vector3 desiredVelocity = Vector3.zero;

        // Move up when the space key is pressed and if below the maximum height
        if (Input.GetKey(KeyCode.Space) && transform.position.y < maxHeight)
        {
            desiredVelocity.y = upwardForce;
        }
        // Move down when the S key is pressed and if above the minimum height
        else if (Input.GetKey(KeyCode.S) && transform.position.y > minHeight)
        {
            desiredVelocity.y = -downwardForce;
        }

        // Move along the X-axis when W is pressed
        if (Input.GetKey(KeyCode.W))
        {
            desiredVelocity.x = -movementSpeed; // Moving along the X-axis
        }

        // Move along the Z-axis when A is pressed
        if (Input.GetKey(KeyCode.A))
        {
            desiredVelocity.z = -movementSpeed; // Moving along the Z-axis
        }
        // Move along the Z-axis when D is pressed
        else if (Input.GetKey(KeyCode.D))
        {
            desiredVelocity.z = movementSpeed; // Moving along the Z-axis
        }

        // Smooth the movement by gradually changing velocity
        carRigidbody.velocity = Vector3.SmoothDamp(carRigidbody.velocity, desiredVelocity, ref currentVelocity, movementSmoothTime);
    }

    private void MaintainFloorDistance()
    {
        // Define the ray for floor detection
        Vector3 rayOrigin = new Vector3(transform.position.x, transform.position.y + rayOriginYOffset, transform.position.z);
        Ray ray = new Ray(rayOrigin, Vector3.down);

        // Draw the ray in the Scene view for debugging
        Debug.DrawRay(ray.origin, ray.direction * (floorCheckDistance + 1f), Color.red);

        // Perform the Raycast
        if (Physics.Raycast(ray, out RaycastHit hit, floorCheckDistance + 1f))
        {
            // Debugging output to check if the ray hits the ground and the distance
            // Debug.Log("Ray hit at: " + hit.point + ", Distance: " + hit.distance);

            // Adjust the car's height based on the floor hit point
            if (!hit.collider.gameObject.CompareTag("Helipad"))
            {
                float targetHeight = hit.point.y + floorCheckDistance;

                // Smoothly adjust the car's height to maintain the floor distance
                float adjustedY = Mathf.Lerp(transform.position.y, targetHeight, Time.fixedDeltaTime * floorAdjustmentSpeed);
                transform.position = new Vector3(transform.position.x, adjustedY, transform.position.z);
            }
           
        }
        else
        {
            // Debug.Log("Ray did not hit anything.");
        }

        // Idle motion when no input is given
        if (!Input.anyKey) // If no movement keys are pressed
        {
            lastTime += Time.fixedDeltaTime;
            idleOffset = Mathf.Sin(lastTime * idleFrequency) * idleAmplitude;
            float idleHeight = transform.position.y + idleOffset;

            // Smooth idle height adjustment
            float adjustedY = Mathf.Lerp(transform.position.y, idleHeight, Time.fixedDeltaTime * floorAdjustmentSpeed);
            transform.position = new Vector3(transform.position.x, adjustedY, transform.position.z);
        }
    }
    private void OnEnable()
    {
        carRigidbody.constraints = RigidbodyConstraints.FreezeRotation;
    }
}