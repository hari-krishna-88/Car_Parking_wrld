using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Winned_Sequence : MonoBehaviour
{
    public GameObject Winned_Ui;
    public GameObject ParkingCube;
    private ParkingTrigger parkingTrigger;

    public GameObject Car_Information_UI;

    //for to stop the roation of the cam 
    public CinemachineFreeLook FreeLook;
    public CinemachineFreeLook FreeLook2;

    //for to dissble unwanted thing in ui;
    public GameObject unwanted1;
                                    
    public GameObject unwanted2;

    //for the reference of car controller script . 
    OffroadCarController carController;
    public GameObject car;
    Rigidbody carRigidbody;


    //for the coins
    public CoinData coinData;
    public int coinEarnedPerLevel = 242;
    bool isCoinsCollected = true;


    private void Start()
    {
        Winned_Ui.SetActive(false);
        parkingTrigger = ParkingCube.GetComponent<ParkingTrigger>();
        carController = gameObject.GetComponent<OffroadCarController>();
        carRigidbody = car.GetComponent<Rigidbody>();   
    }
    private void Update()
    {
        if (parkingTrigger.parked)
        {
            //when car is parked then i want to stop the movement 
            if (carRigidbody != null)
            {
                carRigidbody.velocity = Vector3.zero; // Stop linear movement
                carRigidbody.angularVelocity = Vector3.zero; // Stop rotation
                carRigidbody.isKinematic = true; // Temporarily make it non-physical (optional)
            }

            Winned_Ui.SetActive(true);
            Car_Information_UI.SetActive(false);
            //for to stop the cam rotatoin 
            FreeLook.Follow = null;
            FreeLook.LookAt = null;
            FreeLook2.Follow = null;
            FreeLook2.LookAt = null;
            unwanted1.SetActive(false);
            unwanted2.SetActive(false);
            carController.enabled = false;

            //for to add the coins 
            if (isCoinsCollected)
            {
                coinData.totalCoins += coinEarnedPerLevel;
                isCoinsCollected = false;
            }
            
        }
    }

}
