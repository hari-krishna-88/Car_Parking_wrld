using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision_Sounds : MonoBehaviour
{

    [SerializeField] AudioClip MatelBarrierSound;
    [SerializeField] AudioClip ConcreateBarrier;
    [SerializeField] AudioClip Others;

    AudioSource audiosource;
    OffroadCarController carcontroller;

    // Start is called before the first frame update
    void Start()
    {
        audiosource = GetComponent<AudioSource>();
        carcontroller = GetComponent<OffroadCarController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("MetalBarrier"))
        {
            audiosource.PlayOneShot(MatelBarrierSound);
            carcontroller.StopCar();
        }
        else if (collision.gameObject.CompareTag("SideBarrier"))
        {
            audiosource.PlayOneShot(ConcreateBarrier);
            carcontroller.StopCar();
        }
        else
        {
            if (!collision.gameObject.CompareTag("Friendly"))
            {
                audiosource.PlayOneShot(ConcreateBarrier);
                carcontroller.StopCar();
            }
        }
    }
}
