using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeftDirection : MonoBehaviour
{
    public RectTransform imageTransform; // Assign the RectTransform of the image in the Inspector
    public float moveDistance = 50f; // Distance to move up and down
    public float moveSpeed = 2f; // Speed of the movement

    public GameObject Dissable;
    public GameObject Dissable2;
    public GameObject Enable;

    private Vector2 initialPosition;

    void Start()
    {
        initialPosition = imageTransform.anchoredPosition; // Store the initial position of the image
    }

    void Update()
    {
        // Calculate the new position with a smooth up and down movement using Mathf.Sin
        float yOffset = Mathf.Sin(Time.time * moveSpeed) * moveDistance;
        imageTransform.anchoredPosition = initialPosition + new Vector2(yOffset,0);
    }
}
