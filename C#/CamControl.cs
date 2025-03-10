using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamControl : MonoBehaviour
{
    public GameObject freeLookCam1; // Assign the first FreeLook camera in the Inspector
    public GameObject freeLookCam2; // Assign the second FreeLook camera in the Inspector

    private bool camSwitched = true;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (camSwitched)
            {
                // Toggle the active state of the cameras
                freeLookCam1.SetActive(!freeLookCam1.activeSelf);
                freeLookCam2.SetActive(!freeLookCam2.activeSelf);
            }
            else
            {
                freeLookCam2.SetActive(!freeLookCam1.activeSelf);
                freeLookCam1.SetActive(!freeLookCam2.activeSelf);
            }

            camSwitched = !camSwitched;
        }
    }
}
