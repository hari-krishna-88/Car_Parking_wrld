using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Fly_to_Drive_Transision : MonoBehaviour
{

    public ParticleSystem one, two, three, four;

    private CarFlyingController flyingController;
    private CarAnimationController carAnimationController;
    private OffroadCarController offroadCarController;
    Animator carAnimator;
    AudioSource audioSource;

    public AudioClip thrusterIdal;

    private bool ToggelFlyModeOrDrive = true;
    public bool driveMode = true;
    public bool flyingMode = false;

    private void Start()
    {
        flyingController = gameObject.GetComponent<CarFlyingController>();
        carAnimationController = gameObject.GetComponent<CarAnimationController>();
        offroadCarController = gameObject.GetComponent<OffroadCarController>();
        carAnimator = gameObject.GetComponent<Animator>();
        audioSource = gameObject.GetComponent<AudioSource>();

        flyingController.enabled = false;
        carAnimationController.enabled = false;
        offroadCarController.enabled = true;
        carAnimator.enabled = false;

        one.Stop();
        two.Stop();
        three.Stop();
        four.Stop();
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (ToggelFlyModeOrDrive)
            {
                carAnimator.enabled = true;
                flyingController.enabled = true;
                carAnimationController.enabled = true;
                offroadCarController.enabled = false;

                flyingMode = true;
                driveMode = false;
                Invoke("ThrusterOn", 1f);
            }

            if (!ToggelFlyModeOrDrive)
            {
                flyingMode = false;
                driveMode = true;
                Invoke("FlyModeOff", 1f);
                ThrusterOff();
            }

            ToggelFlyModeOrDrive = !ToggelFlyModeOrDrive;
        }
    }

    private void FlyModeOff()
    {
        carAnimator.enabled = false;
        flyingController.enabled = false;
        carAnimationController.enabled = false;
        offroadCarController.enabled = true;
    }

    private void ThrusterOn()
    {
        one.Play();
        two.Play();
        three.Play();
        four.Play();

        // Assign the thruster idle sound and play it in a loop
        audioSource.clip = thrusterIdal;
        audioSource.loop = true; // Enable looping
        audioSource.Play();
    }

    private void ThrusterOff()
    {
        one.Stop();
        two.Stop();
        three.Stop();
        four.Stop();

        // Stop the looping audio
        if (audioSource.clip == thrusterIdal)
        {
            audioSource.Stop();
            audioSource.loop = false; // Disable looping if necessary
        }
    }

}