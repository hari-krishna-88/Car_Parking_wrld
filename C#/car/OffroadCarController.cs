using System;
using UnityEngine;
using System.Collections;

public class OffroadCarController : MonoBehaviour
{


    public float motorForce = 1500f;       // Force applied to move forward/backward
    public float steeringAngle = 30f;      // Maximum angle for steering
    public float breakForce = 3000f;
    public WheelCollider frontLeftWheel, frontRightWheel, rearLeftWheel, rearRightWheel;  // Wheel colliders
    public Transform frontLeftTransform, frontRightTransform, rearLeftTransform, rearRightTransform;  // Wheel transforms for rotation



    private float currentSteerAngle;
    private float currentMotorForce;
    public float currentBreakForce;

    public bool Isbreaking = false;
    public bool is4x4Enabled = false;
    private bool FuelIsEmpty = false;
    public bool ShowGameOver = false;
    public bool Jumped = false;
    public bool NotJumped = false;
    public bool KeyCollected = false;

    private Rigidbody carRigidbody;  // Reference to Rigidbody

    private void Awake()
    {
        // Assign carRigidbody in Awake to ensure it's ready before OnEnable
        carRigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
       

        // Set the center of mass of the Key to the center (or adjust if needed)
        carRigidbody.centerOfMass = Vector3.zero;  // Adjust the Vector3 if your Key's center of mass needs an offset

    }
    private void FixedUpdate()
    {
        GetInput();

        if (!FuelIsEmpty)
        {
            ApplyMovement();
        }

        UpdateWheels();
    }

    private void GetInput()
    {
        // Set motor force based on vertical input (W/S or Up/Down Arrow)
        currentMotorForce = motorForce * Input.GetAxis("Vertical");

        // Set steering angle based on horizontal input (A/D or Left/Right Arrow)
        currentSteerAngle = steeringAngle * Input.GetAxis("Horizontal");


        // Check the player applaying the break or not(Space)
        if (Input.GetKey(KeyCode.Space))
        {
            Break();
            Isbreaking = true;
        }
        else
        {
            currentBreakForce = 0f;
            Isbreaking = false;
        }

        // Toggle 4x4 mode
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            is4x4Enabled = !is4x4Enabled;
        }
    }



    private void ApplyMovement()
    {
        // Apply motor force to rear wheels if not braking
        if (!Isbreaking)
        {
            rearLeftWheel.motorTorque = currentMotorForce;
            rearRightWheel.motorTorque = currentMotorForce;

            // If 4x4 is enabled, also apply motor force to the front wheels
            if (is4x4Enabled)
            {
                frontLeftWheel.motorTorque = currentMotorForce;
                frontRightWheel.motorTorque = currentMotorForce;
            }
        }
        else
        {
            // Set motor torque to 0 when braking
            rearLeftWheel.motorTorque = 0f;
            rearRightWheel.motorTorque = 0f;
            frontLeftWheel.motorTorque = 0f;
            frontRightWheel.motorTorque = 0f;
        }

        // Apply steering angle to front wheels
        frontLeftWheel.steerAngle = currentSteerAngle;
        frontRightWheel.steerAngle = currentSteerAngle;

        // Apply brake force to all wheels
        frontLeftWheel.brakeTorque = currentBreakForce;
        frontRightWheel.brakeTorque = currentBreakForce;
        rearLeftWheel.brakeTorque = currentBreakForce;
        rearRightWheel.brakeTorque = currentBreakForce;
    }


    public void Break()
    {
        currentBreakForce = breakForce;
    }

    private void UpdateWheels()
    {
        // Rotate wheels visually based on movement and steering
        UpdateWheelPose(frontLeftWheel, frontLeftTransform);
        UpdateWheelPose(frontRightWheel, frontRightTransform);
        UpdateWheelPose(rearLeftWheel, rearLeftTransform);
        UpdateWheelPose(rearRightWheel, rearRightTransform);
    }

    private void UpdateWheelPose(WheelCollider collider, Transform trans)
    {
        // Position and rotation from collider for visual alignment
        Vector3 pos;
        Quaternion rot;
        collider.GetWorldPose(out pos, out rot);
        trans.position = pos;
        trans.rotation = rot;
    }
    private void OnEnable()
    {
        carRigidbody.constraints = RigidbodyConstraints.None;
    }

    // Call this method to stop the car smoothly
    public void StopCar()
    {
        FuelIsEmpty = true;
        StartCoroutine(StopCarGradually());
       // Debug.Log("stop cat called");
        ShowGameOver = true;
    }

    public IEnumerator StopCarGradually()
    {
        float elapsedTime = 0f;
        float initialVelocity = carRigidbody.velocity.magnitude; // Get initial velocity magnitude

        // While the car is still moving
        while (elapsedTime < 2f && carRigidbody.velocity.magnitude > 0.1f) // Stop if speed is almost zero
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / 2f; // Normalize time (0 to 1)

            // Gradually apply braking force (smooth braking)
            currentBreakForce = Mathf.Lerp(0f, breakForce, t);

            // Set motor force to zero as we decelerate
            currentMotorForce = Mathf.Lerp(motorForce * Input.GetAxis("Vertical"), 0f, t); // Gradually reduce motor force based on input

            // Apply the current forces to the wheels
            ApplyMovement();

            

            yield return null;
        }

        // Ensure the car fully stops
        currentMotorForce = 0f;
        currentBreakForce = breakForce;

    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Jumped"))
        {
            Jumped = true;
            Debug.Log("jumped");
        }
        if (other.gameObject.CompareTag("NotJumped"))
        {
            NotJumped = true;
            StopCar();
            Debug.Log("not jumped");
        }
        if (other.gameObject.CompareTag("Key"))
        {
            KeyCollected = true;
        }
    }


}