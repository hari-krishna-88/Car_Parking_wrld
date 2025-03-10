using UnityEngine;

public class CarGroundClearance : MonoBehaviour
{
    [SerializeField] AudioClip air_suspenssion;

    private WheelCollider[] wheelColliders;
    private float[] originalSuspensionDistances;
    public float clearanceIncreaseAmount = 0.5f;
    public float maxClearance = 2.0f;
    public float smoothTime = 1.0f;

    private bool isIncreased = false;
    private bool isTransitioning = false;

    AudioSource audioSource;

    void Start()
    {
        
        wheelColliders = GetComponentsInChildren<WheelCollider>();

        // Store the original suspension distances
        originalSuspensionDistances = new float[wheelColliders.Length];
        for (int i = 0; i < wheelColliders.Length; i++)
        {
            originalSuspensionDistances[i] = wheelColliders[i].suspensionDistance;
        }

        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (!isTransitioning)
        {
            if (Input.GetKeyDown(KeyCode.L))
            {
                isIncreased = !isIncreased;
                isTransitioning = true;
                audioSource.PlayOneShot(air_suspenssion);
            }
        }

        if (isTransitioning)
        {
            AirSuspension();
        }
    }

    private void AirSuspension()
    {
        bool allWheelsReachedTarget = true;

        for (int i = 0; i < wheelColliders.Length; i++)
        {
            float targetSuspensionDistance = isIncreased ? Mathf.Min(originalSuspensionDistances[i] + clearanceIncreaseAmount, maxClearance) : originalSuspensionDistances[i];
            wheelColliders[i].suspensionDistance = Mathf.Lerp(wheelColliders[i].suspensionDistance, targetSuspensionDistance, smoothTime * Time.deltaTime);

            // Check if the current wheel's suspension distance is close enough to the target
            if (Mathf.Abs(wheelColliders[i].suspensionDistance - targetSuspensionDistance) > 0.01f)
            {
                allWheelsReachedTarget = false;
            }
        }

        // If all wheels have reached their target distances, stop the transition
        if (allWheelsReachedTarget)
        {
            isTransitioning = false;
        }
    }
}
