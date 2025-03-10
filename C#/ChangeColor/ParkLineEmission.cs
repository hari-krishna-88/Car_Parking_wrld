using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class ParkLineEmission : MonoBehaviour
{
    private ParkingTrigger parkingTrigger;

    public Color newColor;
    private Renderer[] renderers; 
    private Color originalColor;
    public Color parkedColor;
    
    
    void Start()
    {
        parkingTrigger = FindObjectOfType<ParkingTrigger>();
        renderers =GetComponentsInChildren<Renderer>();
        
        foreach(Renderer rend in renderers)
        {
            originalColor = rend.material.color;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (parkingTrigger != null)
        {
            bool inside = parkingTrigger.isInside;
            bool parked = parkingTrigger.isParked;

            if (inside)
            {
                Debug.Log("inside");
                foreach (Renderer rend in renderers)
                {
                    rend.material.color = newColor;
                }

            }
            else
            {
                foreach(Renderer rend in renderers)
                {
                    rend.material.color = originalColor;
                }
            }
            if (parked)
            {
                foreach(Renderer rend in renderers)
                {
                    rend.material.color = parkedColor;
                }
            }
            else
            {
                if (!inside)
                {
                    foreach (Renderer rend in renderers)
                    {
                        rend.material.color = originalColor;
                    }
                }
            }
        }
    }
}
