using UnityEngine;
using System.Collections;

public class GateController : MonoBehaviour
{
    public float targetAngle = 70f; // Target Z rotation in degrees
    public float speed = 2f;       // Speed of rotation
    private bool isOpen = false;   // State of the gate

    public GameObject Key;
    private KeyScript gameoveScript; // this script handel the collisions so for chekking if the key is collected or not .


    private void Start()
    {
        gameoveScript = Key.GetComponent<KeyScript>();
    }

    void Update()
    {
        // Open the gate when the space key is pressed
        if (gameoveScript.keyCollected && !isOpen)
        {
            StartCoroutine(OpenGate());
        }
    }

    IEnumerator OpenGate()
    {
        isOpen = true;
        Quaternion initialRotation = transform.rotation;
        Quaternion targetRotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, targetAngle);

        float elapsed = 0f;

        while (elapsed < 1f)
        {
            elapsed += Time.deltaTime * speed;
            transform.rotation = Quaternion.Lerp(initialRotation, targetRotation, elapsed);
            yield return null;
        }

        transform.rotation = targetRotation; // Ensure it reaches the exact target
    }
}
