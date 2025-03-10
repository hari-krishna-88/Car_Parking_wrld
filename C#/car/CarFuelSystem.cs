using UnityEngine;

public class CarFuelSystem : MonoBehaviour
{
    public float maxFuel = 1f;      // Max fuel level
    public float minFuel = 0f;      // Min fuel level
    public float fuel = 1f;         // Current fuel level
    public float fuelConsumptionRate = 0.3f; // Fuel consumed per acceleration update


    private Rigidbody carRigidbody;
    private Vector3 lastVelocity;

    OffroadCarController carController;

    public AudioSource audioSource;
    public AudioClip fuelPickUpSound;

    void Start()
    {
        carRigidbody = GetComponent<Rigidbody>();
        lastVelocity = carRigidbody.velocity; // Initial velocity
        carController = gameObject.GetComponent<OffroadCarController>();
    }

    void Update()
    {
        FuelLevelUpdate();
        
        if(fuel == minFuel)
        {
            FuelDone();
        }
    }

    private void FuelLevelUpdate()
    {
        // Calculate acceleration (change in velocity)
        Vector3 currentVelocity = carRigidbody.velocity;
        float acceleration = (currentVelocity - lastVelocity).magnitude / Time.deltaTime;

        // If there is acceleration, consume fuel
        if (acceleration > 0.1f) // Adjust threshold as needed
        {
            fuel -= fuelConsumptionRate * acceleration * Time.deltaTime;
        }

        // Clamp the fuel level between minFuel and maxFuel
        fuel = Mathf.Clamp(fuel, minFuel, maxFuel);

        // Update last velocity for the next frame
        lastVelocity = currentVelocity;

       // Debug.Log("the fule level is "+fuel);
    }
    
    // FUEL COLLECTION

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "FUEL")
        {
            audioSource.PlayOneShot(fuelPickUpSound);
            fuel = fuel + 0.4f;
        }
    }


    private void FuelDone()
    {
        Debug.Log("car stopping");
        carController.StopCar();
    }
}
