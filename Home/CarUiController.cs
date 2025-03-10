using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarUIController : MonoBehaviour
{
    public List<GameObject> carPrefabs; // Assign your car prefabs in the inspector
    public Transform carParent; // Parent Transform for rotating car and platform
    public GameObject platformPrefab; // Assign your platform prefab in the inspector
    public Button leftButton;  // Assign the left button
    public Button rightButton; // Assign the right button
    public float rotationSpeed = 10f; // Speed of rotation for the car and platform

    private int currentCarIndex = 0; // Index of the current car
    private GameObject currentCar;  // Currently displayed car
    private GameObject currentPlatform; // Currently displayed platform
    private float rotationInput = 1f; // Default rotation direction (1 for clockwise)

    void Start()
    {
        if (carPrefabs.Count > 0 && platformPrefab != null)
        {
            LoadCar(currentCarIndex); // Load the first car initially
        }

        // Add listeners to the buttons
        leftButton.onClick.AddListener(ChangeCarLeft);
        rightButton.onClick.AddListener(ChangeCarRight);
    }

    void Update()
    {
        // Continuously rotate the carParent based on rotationInput
        if (carParent != null && rotationInput != 0)
        {
            carParent.Rotate(Vector3.up, rotationInput * rotationSpeed * Time.deltaTime);
        }
        Debug.Log("the indext of the currenct car is " + currentCarIndex);
    }

    public void ChangeCarLeft()
    {
        // Decrement the index and loop around if necessary
        currentCarIndex = (currentCarIndex - 1 + carPrefabs.Count) % carPrefabs.Count;
        LoadCar(currentCarIndex);
    }

    public void ChangeCarRight()
    {
        // Increment the index and loop around if necessary
        currentCarIndex = (currentCarIndex + 1) % carPrefabs.Count;
        LoadCar(currentCarIndex);
    }

    private void LoadCar(int index)
    {
        // Destroy the current car and platform if they exist
        if (currentCar != null) Destroy(currentCar);
        if (currentPlatform != null) Destroy(currentPlatform);

        // Instantiate the platform and make it a child of carParent
        currentPlatform = Instantiate(platformPrefab, carParent.position, carParent.rotation, carParent);

        // Instantiate the car and make it a child of carParent
        currentCar = Instantiate(carPrefabs[index], carParent.position, carParent.rotation, carParent);

        // Adjust the car's position to sit on the platform
        Vector3 carPositionOffset = new Vector3(0, currentPlatform.transform.localScale.y / 2, 0);
        currentCar.transform.localPosition = carPositionOffset; // Local position to adjust only relative to the platform
    }

    public void RotateLeft()
    {
        rotationInput = -1f; // Set negative input for left rotation
    }

    public void RotateRight()
    {
        rotationInput = 1f; // Set positive input for right rotation
    }

    public void StopRotation()
    {
        rotationInput = 0f; // Stop rotation
    }
}
