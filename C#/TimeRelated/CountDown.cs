using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class CountDown : MonoBehaviour
{
    public GameObject one, two, three, go;
    private OffroadCarController carController;

    //this is for to activate the timer
    public bool countDownFinished = false;


    private void Start()
    {
        one.SetActive(false);
        two.SetActive(false);
        three.SetActive(false);
        go.SetActive(false);
        StartCoroutine(CountDonws());
        carController = gameObject.GetComponent<OffroadCarController>();
        carController.enabled = false;
    }

    private void Update()
    {
        
    }

    private IEnumerator CountDonws()
    {
        three.SetActive(true);
        yield return new WaitForSeconds(1);
        three.SetActive(false);
        two.SetActive(true);
        yield return new WaitForSeconds(1);
        two.SetActive(false);
        one.SetActive(true);
        yield return new WaitForSeconds(1);
        one.SetActive(false);
        go.SetActive(true);
        yield return new WaitForSeconds(1);
        go.SetActive(false);
        carController.enabled = true;
        countDownFinished = true;
    }
}
