using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text timerText; // Assign a UI Text element to this in the Inspector
    public float startTime = 10f; // Set the countdown start time from the Inspector

    private float remainingTime;
    public GameObject car;
    private CountDown countDownScript;
    public GameObject CountDownShow;
    private OffroadCarController carController;
    public GameObject timer;
    public GameObject PerfectText;
    private bool IsJumpedWorked = false;
   
   

    private bool isTimerRunning = false;

    private void Start()
    {
        countDownScript = car.GetComponent<CountDown>();
        carController = car.GetComponent<OffroadCarController>();
        CountDownShow.SetActive(false);
        remainingTime = startTime; // Initialize the countdown timer
        PerfectText.SetActive(false);
    }

    void Update()
    {
        if (countDownScript.countDownFinished && !isTimerRunning)
        {
            CountDownShow.SetActive(true);
            Invoke("StartTimer", 2.1f); // Start the timer after 2.1 seconds
            isTimerRunning = true;
        }

        //if the car is jumped then i want to stop the timer;
        if (carController.Jumped && !IsJumpedWorked && !carController.NotJumped)
        {
            isTimerRunning = false;
            PerfectText.SetActive(true);
            IsJumpedWorked = true;
            StartCoroutine(StopeTimer());
        }

        if (isTimerRunning && !carController.Jumped)
        {
            TimerRun();
        }

    }

    private void StartTimer()
    {
        Debug.Log("Timer started");
        isTimerRunning = true;
    }

    private void TimerRun()
    {
        if (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime; // Decrease time

            // Prevent negative values and ensure the timer stops at exactly 0.00
            if (remainingTime <= 0.01f)
            {
                remainingTime = 0f;
                isTimerRunning = false;
                Debug.Log("Timer finished");
                carController.StopCar();
                timer.SetActive(false);
            }

            // Calculate seconds and milliseconds
            int seconds = Mathf.FloorToInt(remainingTime);
            int milliseconds = Mathf.FloorToInt((remainingTime - seconds) * 100);

            // Format the timer string
            timerText.text = string.Format("{0:00}:{1:00}", seconds, milliseconds);
        }
    }


    IEnumerator StopeTimer()
    {
        yield return new WaitForSeconds(3f);
        timer.SetActive(false);
    }

    
}
