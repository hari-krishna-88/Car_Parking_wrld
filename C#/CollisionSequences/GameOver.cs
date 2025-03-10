using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    public GameObject CutSceneGameobjectGameOver;
    public GameObject DirectionCanvas;
    public GameObject CarInformationCanvas;
    private OffroadCarController carController;

    public GameObject Information_Canvas;
    public GameObject Phone_Canvas;
    public GameObject Enable_PhoneCanvas;

    public bool GameEnded = false;

    //whent the game is over then i want o stop the rotation of the cam 
    public CinemachineFreeLook VirtualCamera;
    public CinemachineFreeLook VirtualCamera2;




    private void Start()
    {
        CutSceneGameobjectGameOver.SetActive(false);
        carController = gameObject.GetComponent<OffroadCarController>();
        carController.enabled = false;
    }

    private void Update()
    {
        if (carController.ShowGameOver)
        {
            Invoke("AllGameOver", 2);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Key") && !collision.gameObject.CompareTag("Friendly") && !collision.gameObject.CompareTag("Helipad"))
        {
            AllGameOver();
        }
        GameEnded = true;
        
    }

    public void AllGameOver()
    {
        Information_Canvas.SetActive(false);
        Enable_PhoneCanvas.SetActive(false);

        CutSceneGameobjectGameOver.SetActive(true);
        DirectionCanvas.SetActive(false);
        CarInformationCanvas.SetActive(false);

        //when the Key collide with anything i want to dessable the carcontroller;
        carController.enabled = false;

        VirtualCamera.Follow = null;
        VirtualCamera.LookAt = null;

        VirtualCamera2.Follow = null;
        VirtualCamera2.LookAt = null;
    }
}
