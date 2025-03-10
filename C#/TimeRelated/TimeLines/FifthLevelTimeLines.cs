using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FifthLevelTimeLines : MonoBehaviour
{
    OffroadCarController carController;
    public GameObject cam , TimeLine , freelook;


    private void Awake()
    {
        carController = GetComponent<OffroadCarController>();
        cam.SetActive(false);
        TimeLine.SetActive(false);
    }

    private void Update()
    {
        if (carController.KeyCollected)
        {
            Invoke("PlayBridgeTimeLine", 2f);
          //  Debug.Log("key collected true");
        }
    }

    public void PlayBridgeTimeLine()
    {
        Debug.Log("this is called");
        StartCoroutine(carController.StopCarGradually());
        freelook.SetActive(false);
        cam.SetActive(true);
        TimeLine.SetActive(true);
        Invoke("StopBridgeTimeLine", 6);
    }

    public void StopBridgeTimeLine()
    {
        cam.SetActive(false);
        freelook.SetActive(true);
        TimeLine.SetActive(false);
        Debug.Log("the timeline is stoped");
    }
}
