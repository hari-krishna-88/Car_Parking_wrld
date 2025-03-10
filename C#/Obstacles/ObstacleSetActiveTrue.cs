using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Collections;
using UnityEngine;

public class ObstacleSetActiveTrue : MonoBehaviour
{
    [SerializeField]
    private int Raydistance = 10;

    [SerializeField]
    private GameObject ObstacleForCloseTheWay;

    private void Start()
    {
        ObstacleForCloseTheWay.SetActive(true);
    }
    private void Update()
    {
        CastRay();
    }

    
    private void CastRay()
    { 
        RaycastHit hit;
        Vector3 position = transform.position;
        Vector3 direction = transform.TransformDirection(Vector3.forward);

        if (Physics.Raycast(position, direction, out hit, Raydistance))
        {
            if (hit.collider.CompareTag("MainCar"))
            {
                ObstacleForCloseTheWay.SetActive(false);
            }
        }
        Debug.DrawRay(position, direction * Raydistance, Color.green);
    }
}
