using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Collections;
using UnityEngine;

public class Enable_Obstacle : MonoBehaviour
{
    private bool hasHit = false;
    [SerializeField]
    private int Raydistance = 10;

    private Road_Stick_obstaclle obstacle_Script;
    public GameObject obstacles;

    private void Start()
    {
        obstacle_Script = obstacles.GetComponent<Road_Stick_obstaclle>();
    }
    private void Update()
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
                obstacle_Script.up = true;
                
            }
        }
        Debug.DrawRay(position, direction * Raydistance, Color.green);
    }
}
