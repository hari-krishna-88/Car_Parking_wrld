using System;
using UnityEngine;

public class EmitColorOnKeyPress : MonoBehaviour
{
    // Reference to the material
    public Material BrekLight;
    public Material HeadLightMaterial;


    // Color to emit when Space is pressed
    public Color emissionColorBreakLight = Color.red;
    public Color emissionColorHeadLight = Color.white;

    private OffroadCarController controller;

    private bool isHeadLightOn = false;

    private void Start()
    {
        controller = GetComponent<OffroadCarController>();
    }

    void Update()
    {
        BreakLight();
        HeadLight();
    }


    private void BreakLight()
    {
        if (controller.Isbreaking)
        {
            BrekLight.SetColor("_EmissionColor", emissionColorBreakLight);
            // Enable emission
            BrekLight.EnableKeyword("_EMISSION");
        }
        else
        {

            // Disable emission
            BrekLight.DisableKeyword("_EMISSION");
        }
    }

    private void HeadLight()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            isHeadLightOn = !isHeadLightOn;
            if (isHeadLightOn)
            {
                HeadLightMaterial.SetColor("_EmissionColor", emissionColorHeadLight);
                HeadLightMaterial.EnableKeyword("_EMISSION");
            }
            else
            {
                HeadLightMaterial.SetColor("_EmissionColor", Color.black);
                HeadLightMaterial.DisableKeyword("_EMISSION");
            }
            
        }
    }
}
