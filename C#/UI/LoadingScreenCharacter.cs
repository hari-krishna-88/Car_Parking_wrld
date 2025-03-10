using UnityEngine;

public class LoadingScreenCharacterUI : MonoBehaviour
{
    public float moveSpeed = 200f;            // Speed of the character movement
    public RectTransform targetImage;         // The target image RectTransform
    private RectTransform rectTransform;      // The RectTransform of the character
    private Animator animator;                // Animator component for character animations
    private bool gameLoaded = false;          // To check if the game has loaded

    void Start()
    {
        // Get the RectTransform component of the UI element
        rectTransform = GetComponent<RectTransform>();

        // Get the Animator component attached to the character
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Check if the "D" key is being pressed and the game hasn't loaded
        if (Input.GetKey(KeyCode.D) && !gameLoaded)
        {
            // Move the character to the right in the UI
            rectTransform.anchoredPosition += Vector2.right * moveSpeed * Time.deltaTime;
            Debug.Log("Character Position: " + rectTransform.anchoredPosition);

            // Enable the animator component to play the animation
            if (!animator.enabled)
            {
                animator.enabled = true;
            }

            // Check if the character has reached or touched the target image
            if (IsTouchingTarget())
            {
                gameLoaded = true;
                animator.enabled = false; // Disable the animator when reaching the target
                LoadGame(); // Call the load game function
            }
        }
        else
        {
            // Disable the animator component when the character is not moving
            if (animator.enabled)
            {
                animator.enabled = false;
            }
        }
    }

    // Function to load the game or next scene
    void LoadGame()
    {
        // Add your loading logic here
        Debug.Log("Game Loaded!"); // Replace this with your actual loading code
    }

    // Function to check if the character is touching the target image
    bool IsTouchingTarget()
    {
        // Get the width of both the character and target images
        float characterRightEdge = rectTransform.anchoredPosition.x + rectTransform.rect.width / 2;
        float targetLeftEdge = targetImage.anchoredPosition.x - targetImage.rect.width / 2;

        // Check if the character's right edge has reached the target's left edge
        return characterRightEdge >= targetLeftEdge;
    }
}
