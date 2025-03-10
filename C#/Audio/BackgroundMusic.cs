using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    private static BackgroundMusic instance;
    private AudioSource audioSource;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            audioSource = GetComponent<AudioSource>();

            // Ensure the audio starts playing
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else
        {
            Destroy(gameObject);  // Destroy duplicate instances when reloading
        }
    }
}
