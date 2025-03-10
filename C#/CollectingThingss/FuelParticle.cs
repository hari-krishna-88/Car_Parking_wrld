using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelParticle : MonoBehaviour
{
    public ParticleSystem fuelParticle;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Car"))
        {
            fuelParticle.Play();
            Invoke("SetactiveFalse", 1.5f);
        }
    }

    public void SetactiveFalse()
    {
        gameObject.SetActive(false);
    }
}
