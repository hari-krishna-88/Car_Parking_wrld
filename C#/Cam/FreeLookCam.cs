using Cinemachine;
using UnityEngine;

public class FreeLookCam : MonoBehaviour
{
    private CinemachineFreeLook freeLookCam;
    public Rigidbody carRigidbody; // Assign your car's Rigidbody in the Inspector

    void Start()
    {
        freeLookCam = GetComponent<CinemachineFreeLook>();
    }

    void Update()
    {
        if (carRigidbody != null)
        {
            float carSpeed = carRigidbody.velocity.z; // Get the car's speed along the Z-axis
            float speedMultiplier = (carSpeed < 0) ? 5f : 1f; // Increase camera speed when reversing
            freeLookCam.m_XAxis.m_MaxSpeed = 300 * speedMultiplier;
        }
    }
}
