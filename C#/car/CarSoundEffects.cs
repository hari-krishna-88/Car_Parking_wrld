using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using Unity.VisualScripting;
using UnityEngine;

public class CarSoundEffects : MonoBehaviour
{
    private bool played = false;

    public AudioClip MetalbarrierCrahing;
    public AudioClip ConcreateBarrierSoundEff;
    public AudioClip KeyCollected;

    public GameObject key;
    private KeyScript keyScript;
    AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        keyScript = key.GetComponent<KeyScript>();
    }

    private void Update()
    {
        if (keyScript.keyCollected && !played)
        {
            audioSource.PlayOneShot(KeyCollected);
            played = true;
        }
    }

    // this is for the Road Barrier Collision;
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("MetalBarrier"))
        {
            audioSource.PlayOneShot(MetalbarrierCrahing);
        }
        if (collision.gameObject.CompareTag("SideBarrier"))
        {
            audioSource.PlayOneShot(ConcreateBarrierSoundEff);
        }
    }

    
}
