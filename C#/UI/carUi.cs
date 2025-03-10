using UnityEngine;
using UnityEngine.UI;

public class ImageToggle : MonoBehaviour
{
    public Image notEnabled;
    public Image enabled;

    private OffroadCarController carController;
    public GameObject car;
    
    void Start()
    {
        if (car != null)
        {
            carController = car.GetComponent<OffroadCarController>();
        }
        // Set the GameObject of the notEnabled image active
        notEnabled.gameObject.SetActive(true);

        // Set the GameObject of the enabled image inactive
        enabled.gameObject.SetActive(false);
    }
    private void Update()
    {
        if (carController.is4x4Enabled)
        {
            notEnabled.gameObject.SetActive(false);
            enabled.gameObject.SetActive(true);
        }
        else
        {
            enabled.gameObject.SetActive(false);
            notEnabled.gameObject.SetActive(true);
        }
    }
}
