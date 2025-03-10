using UnityEngine;

public class KeyScript : MonoBehaviour
{
    public bool keyCollected = false;
    public GameObject CollectKey;
    public GameObject KeyColledted;

    //particle system for key collection . 
    public ParticleSystem keyCollectParticleSystem;



    private void Start()
    {
        KeyColledted.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        keyCollected = true;
        // Check which object triggered the key
        Debug.Log("Triggered by: " + other.gameObject.name);  // Log to check
        if (other.gameObject.CompareTag("Car"))
        {
            keyCollected = true;
           // Debug.Log("Key collected");
            Destroy(gameObject);
            CollectKey.SetActive(false);
            KeyColledted.SetActive(true);
            //for particle system
            keyCollectParticleSystem.Play();
            
        }
    }

   
}

