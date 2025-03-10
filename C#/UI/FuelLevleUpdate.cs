using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FuelLevleUpdate : MonoBehaviour
{
    public CarFuelSystem CarFuelSystem;
    public GameObject car;
    public Image fuelLevl;
    public GameObject FuelCircle_Red, FuelCircle_Green;
    public GameObject full_fuel_text, low_fuel_text, fuel_draining , fuel_Done;

    public Color lowFuelColor = Color.red;
    public Color normalFuelColor = Color.green;

    private Coroutine blinkCoroutine;  // Coroutine for blinking low fuel text

    void Start()
    {
        CarFuelSystem = car.GetComponent<CarFuelSystem>();

        // Set initial UI states
        FuelCircle_Red.SetActive(false);
        FuelCircle_Green.SetActive(true);
        full_fuel_text.SetActive(CarFuelSystem.fuel >= 1.0f);
        low_fuel_text.SetActive(false);
        fuel_draining.SetActive(false);
        fuel_Done.SetActive(false);
    }

    private void Update()
    {
        ChangeColorOfLevel();
    }

    private void ChangeColorOfLevel()
    {
        fuelLevl.fillAmount = CarFuelSystem.fuel;

        if (CarFuelSystem.fuel < 0.2f&&CarFuelSystem.fuel!=0)
        {
            // Start blinking if not already blinking
            if (blinkCoroutine == null)
            {
                blinkCoroutine = StartCoroutine(BlinkLowFuelText());
            }
            fuelLevl.color = lowFuelColor;
            FuelCircle_Green.SetActive(false);
            FuelCircle_Red.SetActive(true);
            full_fuel_text.SetActive(false);
            fuel_draining.SetActive(false);
            fuel_Done.SetActive(false);

        }
        else if (CarFuelSystem.fuel < 0.4f && CarFuelSystem.fuel!=0)
        {
            StopBlinkingLowFuelText();
            fuelLevl.color = lowFuelColor;
            FuelCircle_Green.SetActive(false);
            FuelCircle_Red.SetActive(true);
            full_fuel_text.SetActive(false);
            fuel_draining.SetActive(false);
            low_fuel_text.SetActive(true);
            fuel_Done.SetActive(false);
        }
        else if (CarFuelSystem.fuel == 0)
        {
            //trigger to stop the Key movement and game over .. 
            full_fuel_text.SetActive(false);
            fuel_draining.SetActive(false);
            low_fuel_text.SetActive(false);
            fuel_Done.SetActive(true);
        }
        else
        {
            StopBlinkingLowFuelText();
            fuelLevl.color = normalFuelColor;
            FuelCircle_Green.SetActive(true);
            FuelCircle_Red.SetActive(false);
            full_fuel_text.SetActive(CarFuelSystem.fuel >= 1.0f);
            low_fuel_text.SetActive(false);
            fuel_draining.SetActive(CarFuelSystem.fuel < 1.0f);
            
        }
    }

    private IEnumerator BlinkLowFuelText()
    {
        while (true)
        {
            low_fuel_text.SetActive(!low_fuel_text.activeSelf);  // Toggle visibility
            yield return new WaitForSeconds(0.05f);  // Wait for 0.5 seconds
        }
    }

    private void StopBlinkingLowFuelText()
    {
        if (blinkCoroutine != null)
        {
            StopCoroutine(blinkCoroutine);
            blinkCoroutine = null;
            low_fuel_text.SetActive(false);  // Make sure the text is off when stopping
        }
    }
}
