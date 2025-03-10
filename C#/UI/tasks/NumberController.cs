using Cinemachine;
using System.Net.NetworkInformation;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI.Table;

public class NumberController : MonoBehaviour
{
    public TextMeshProUGUI numberText, FirstNum, SecondNum; // Assign the text component in the Inspector
    private int currentNumber = 0; // Starting number

    public int a = 0;
    public int b = 0;
    public int sum = 0;

    public GameObject mobileCanvas;

    public GameObject road_sticks_obstacles;
    private RoadStick_Remove Down_RoadSticks_Script;
    public AudioClip number_clicking;
    public AudioClip phone_gone_clip;

    AudioSource phones_audiosource;

    //when mini task is started then i don't wanna move the ca . 
    public CinemachineFreeLook freelookcam;
    public CinemachineFreeLook freelookcam2;
    public GameObject car;
    OffroadCarController carController;
    public bool correctAnswer = false;

    public GameObject wrongAnswer_TimeLine;

    void Start()
    {
        a = Random.Range(1, 5);
        b = Random.Range(5, 10);

        UpdateText(); // Initialize the text
        Down_RoadSticks_Script = road_sticks_obstacles.GetComponent<RoadStick_Remove>();
        phones_audiosource = GetComponent<AudioSource>();
        freelookcam.Follow = null;
        freelookcam.LookAt = null;
        freelookcam2.Follow = null;
        freelookcam2.LookAt = null;
        carController = car.GetComponent<OffroadCarController>();
        carController.enabled = false;
        // Stop the Rigidbody's movement
        Rigidbody carRigidbody = car.GetComponent<Rigidbody>();
        if (carRigidbody != null)
        {
            carRigidbody.velocity = Vector3.zero; // Stop linear movement
            carRigidbody.angularVelocity = Vector3.zero; // Stop rotation
            carRigidbody.isKinematic = true; // Temporarily make it non-physical (optional)
        }
    }

    public void IncreaseNumber()
    {
        if (currentNumber < 15)
        {
            currentNumber++;
            UpdateText();
            phones_audiosource.PlayOneShot(number_clicking);
        }
    }

    public void DecreaseNumber()
    {
        if (currentNumber > 1)
        {
            currentNumber--;
            UpdateText();
            phones_audiosource.PlayOneShot(number_clicking);
        }
    }

    public void OkButton()
    {
        if (currentNumber == a + b)
        {
            phones_audiosource.PlayOneShot(phone_gone_clip);

            Invoke("EndSequence", 0.3f);
            //show some password cracked sequnce in the mobile phone(mobile unlocked)...

        }
        else
        {
            WrongAnswer();
            return;
        }
    }

    private void UpdateText()
    {
        numberText.text = currentNumber.ToString();
        FirstNum.text = a.ToString();
        SecondNum.text = b.ToString();
    }

    private void EndSequence()
    {
        mobileCanvas.SetActive(false);

        correctAnswer = true;

        Invoke("CallRoadObstcleDown", 1.5f);

    }


    private void FindSum()
    {
        sum = a + b;
    }

    private void CallRoadObstcleDown()
    {
        Down_RoadSticks_Script.enabled = true;
        Invoke("RetriveCarControlls", 2);
    }

    private void RetriveCarControlls()
    {
        freelookcam.Follow = car.transform;
        freelookcam.LookAt = car.transform;
        freelookcam2.Follow = car.transform;
        freelookcam2.LookAt = car.transform;
        carController.enabled = true;

        // Re-enable Rigidbody physics
        Rigidbody carRigidbody = car.GetComponent<Rigidbody>();
        if (carRigidbody != null)
        {
            carRigidbody.isKinematic = false; // Restore normal physics
        }
    }

    public void WrongAnswer()
    {
        wrongAnswer_TimeLine.SetActive(true);
        Invoke("DisableWrongAnswerGameobject", 1);
    }

    public void DisableWrongAnswerGameobject()
    {
        wrongAnswer_TimeLine.SetActive(false);
    }
}