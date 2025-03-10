using System;
using UnityEngine;

public class SportsCarController : MonoBehaviour
{
    // Public variables for tuning
    public float motorForce = 3000f;        // High torque for quick acceleration
    public float steeringAngle = 35f;      // Sharp and precise steering
    public float brakeForce = 5000f;       // Strong braking for rapid deceleration
    public WheelCollider frontLeftWheel, frontRightWheel, rearLeftWheel, rearRightWheel;  // Wheel colliders
    public Transform frontLeftTransform, frontRightTransform, rearLeftTransform, rearRightTransform;  // Visual wheel transforms

    // Private variables for internal calculations
    private float currentSteerAngle;
    private float currentMotorForce;
    private float currentBrakeForce;
    private bool isBraking = false;

    private Rigidbody carRigidbody;

    private void Awake()
    {
        // Initialize and configure the Rigidbody
        carRigidbody = GetComponent<Rigidbody>();
        carRigidbody.mass = 1500f;  // Optimized mass for a sports car
        carRigidbody.drag = 0.05f;  // Minimal drag for high speed
        carRigidbody.angularDrag = 2f;  // Enhanced stability during turns
    }

    private void Start()
    {
        // Lower center of mass to improve stability
        carRigidbody.centerOfMass = new Vector3(0, -0.5f, 0);
    }

    private void FixedUpdate()
    {
        HandleInput();
        ApplyMovement();
        UpdateWheelVisuals();
        ApplyAntiRollStabilization();
    }

    private void HandleInput()
    {
        // Process throttle input (W/S or Up/Down Arrow keys)
        currentMotorForce = motorForce * Input.GetAxis("Vertical");

        // Process steering input (A/D or Left/Right Arrow keys)
        currentSteerAngle = steeringAngle * Input.GetAxis("Horizontal");

        // Determine braking state (Space key)
        if (Input.GetKey(KeyCode.Space))
        {
            isBraking = true;
            currentBrakeForce = brakeForce;
        }
        else
        {
            isBraking = false;
            currentBrakeForce = 0f;
        }
    }

    private void ApplyMovement()
    {
        // Apply motor torque to rear wheels
        if (!isBraking)
        {
            rearLeftWheel.motorTorque = currentMotorForce;
            rearRightWheel.motorTorque = currentMotorForce;
        }
        else
        {
            // Disable motor torque while braking
            rearLeftWheel.motorTorque = 0f;
            rearRightWheel.motorTorque = 0f;
        }

        // Apply steering to front wheels
        frontLeftWheel.steerAngle = currentSteerAngle;
        frontRightWheel.steerAngle = currentSteerAngle;

        // Apply braking torque to all wheels
        ApplyBrakeTorque(frontLeftWheel);
        ApplyBrakeTorque(frontRightWheel);
        ApplyBrakeTorque(rearLeftWheel);
        ApplyBrakeTorque(rearRightWheel);
    }

    private void ApplyBrakeTorque(WheelCollider wheel)
    {
        wheel.brakeTorque = currentBrakeForce;
    }

    private void UpdateWheelVisuals()
    {
        // Sync collider position and rotation with visual wheels
        UpdateWheelPose(frontLeftWheel, frontLeftTransform);
        UpdateWheelPose(frontRightWheel, frontRightTransform);
        UpdateWheelPose(rearLeftWheel, rearLeftTransform);
        UpdateWheelPose(rearRightWheel, rearRightTransform);
    }

    private void UpdateWheelPose(WheelCollider collider, Transform wheelTransform)
    {
        // Retrieve wheel collider's world position and rotation
        Vector3 position;
        Quaternion rotation;
        collider.GetWorldPose(out position, out rotation);

        // Align the visual wheel with the collider
        wheelTransform.position = position;
        wheelTransform.rotation = rotation;
    }

    private void ApplyAntiRollStabilization()
    {
        // Minimize body roll during high-speed turns
        StabilizeAxle(frontLeftWheel, frontRightWheel);
        StabilizeAxle(rearLeftWheel, rearRightWheel);
    }

    private void StabilizeAxle(WheelCollider leftWheel, WheelCollider rightWheel)
    {
        WheelHit hit;
        float leftTravel = 1f, rightTravel = 1f;

        bool isLeftGrounded = leftWheel.GetGroundHit(out hit);
        if (isLeftGrounded)
        {
            leftTravel = (-leftWheel.transform.InverseTransformPoint(hit.point).y - leftWheel.radius) / leftWheel.suspensionDistance;
        }

        bool isRightGrounded = rightWheel.GetGroundHit(out hit);
        if (isRightGrounded)
        {
            rightTravel = (-rightWheel.transform.InverseTransformPoint(hit.point).y - rightWheel.radius) / rightWheel.suspensionDistance;
        }

        float antiRollForce = (leftTravel - rightTravel) * 10000f;

        if (isLeftGrounded)
        {
            carRigidbody.AddForceAtPosition(leftWheel.transform.up * -antiRollForce, leftWheel.transform.position);
        }

        if (isRightGrounded)
        {
            carRigidbody.AddForceAtPosition(rightWheel.transform.up * antiRollForce, rightWheel.transform.position);
        }
    }
}
