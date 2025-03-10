using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class ParkingTrigger : MonoBehaviour
{
    [SerializeField] GameObject car;
    [SerializeField] bool entered = false;
    [SerializeField] bool inside = false;
    [SerializeField] public bool parked = false;
    [SerializeField] private float EnterTime = 0;
    [SerializeField] private float targetTime = 3f;


    // Public properties to access 'inside' and 'parked' from other scripts
    public bool isInside => inside;
    public bool isParked => parked;


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Car")
        {
            entered = true;
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Car")
        {
            entered = false;
            parked = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Car")
        {
            EnterTime += Time.deltaTime;

            inside = IsInside(other);

            if (!inside)
            {
                EnterTime = 0;
            }

            if (inside && !parked && EnterTime >= targetTime)
            {
                Park();
                //Debug.Log("inside");
            }
        }
    }

    bool IsInside(Collider other)
    {
        Bounds triggerBounds = GetComponent<Collider>().bounds;
        Bounds otherBounds = other.bounds;
        return triggerBounds.Contains(otherBounds.min) && triggerBounds.Contains(otherBounds.max);
    }

    private void Park()
    {
        if (inside)
        {
            parked = true;
        }

        //Debug.Log("hey parked");
    }
}
