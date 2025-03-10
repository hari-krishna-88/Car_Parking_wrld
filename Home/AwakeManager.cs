using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AwakeManager : MonoBehaviour
{
    public GameObject toRotate; // Parent GameObject for rotation
    public float rotateSpeed; // Speed of rotation
    public VehicleList listOfVehicles; // List of vehicle prefabs
    public int vehiclePointer = 0; // Pointer to the current vehicle index

    private void Awake()
    {
        // Initialize the vehicle pointer
        PlayerPrefs.SetInt("pointer", 0);
        vehiclePointer = PlayerPrefs.GetInt("pointer");

        // Instantiate the initial vehicle
        GameObject childObject = Instantiate(listOfVehicles.vehicle[vehiclePointer], Vector3.zero, Quaternion.identity);
        childObject.transform.parent = toRotate.transform;
        childObject.tag = "Car"; // Ensure it has the correct tag
    }

    private void FixedUpdate()
    {
        // Rotate the platform
        toRotate.transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime);
    }

    public void RightButton()
    {
        if (vehiclePointer < listOfVehicles.vehicle.Length - 1)
        {
            // Get the current rotation of the previous car
            Quaternion currentRotation = toRotate.transform.GetChild(0).rotation;

            // Destroy the old vehicle
            Destroy(GameObject.FindGameObjectWithTag("Car"));

            // Increment the pointer and update PlayerPrefs
            vehiclePointer++;
            PlayerPrefs.SetInt("pointer", vehiclePointer);

            // Instantiate the new vehicle and apply the saved rotation
            GameObject childObject = Instantiate(listOfVehicles.vehicle[vehiclePointer], Vector3.zero, currentRotation);
            childObject.transform.parent = toRotate.transform;
            childObject.tag = "Car";
        }
    }

    public void LeftButton()
    {
        if (vehiclePointer > 0)
        {
            // Get the current rotation of the previous car
            Quaternion currentRotation = toRotate.transform.GetChild(0).rotation;

            // Destroy the old vehicle
            Destroy(GameObject.FindGameObjectWithTag("Car"));

            // Decrement the pointer and update PlayerPrefs
            vehiclePointer--;
            PlayerPrefs.SetInt("pointer", vehiclePointer);

            // Instantiate the new vehicle and apply the saved rotation
            GameObject childObject = Instantiate(listOfVehicles.vehicle[vehiclePointer], Vector3.zero, currentRotation);
            childObject.transform.parent = toRotate.transform;
            childObject.tag = "Car";
        }
    }
}
