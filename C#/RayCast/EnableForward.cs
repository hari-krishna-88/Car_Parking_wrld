using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class EnableDirection : MonoBehaviour
{
    public bool hasHit = false;
    [SerializeField] private int Raydistance = 10;

    public GameObject Enable;
    public GameObject Dissable1;
    public GameObject Dissable2;
    public GameObject Dissable3;
    public GameObject For_Messeges;
    public GameObject second_Messege;
    public GameObject car;

    


    // Start is called before the first frame update
    void Start()
    {
        Enable.SetActive(false);
        Dissable1.SetActive(false);
        Dissable2.SetActive(false);
        Dissable3.SetActive(false);
        For_Messeges.SetActive(false);
        second_Messege.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        CastRay();  
    }

    private void CastRay()
    {
        if (hasHit) return;

        RaycastHit hit;
        Vector3 position = transform.position;
        Vector3 direction = transform.TransformDirection(Vector3.forward);

        if (Physics.Raycast(position, direction, out hit, Raydistance))
        {
            if (hit.collider.CompareTag("Car"))
            {
                //Debug.Log("Hited WITH A CAR ");
                hasHit = true;
                EnableAndDirection();
            }
        }
        Debug.DrawRay(position, direction * Raydistance, Color.green);
    }

    private void EnableAndDirection()
    {
        Enable.SetActive(true);
        Dissable1.SetActive(false);
        Dissable2.SetActive(false);
        Dissable3.SetActive(false);
        Invoke("ShowMessege", 0f);
        Invoke("ShowSecondMessege", 9f);
    }
    private void ShowMessege()
    {
        For_Messeges.SetActive(true);

        // Stop the Rigidbody's movement
        Rigidbody carRigidbody = car.GetComponent<Rigidbody>();
        if (carRigidbody != null)
        {
            carRigidbody.velocity = Vector3.zero; // Stop linear movement
            carRigidbody.angularVelocity = Vector3.zero; // Stop rotation
            carRigidbody.isKinematic = true; // Temporarily make it non-physical (optional)
        }
    }

    private void ShowSecondMessege()
    {
        second_Messege.SetActive(true);
    }
}
