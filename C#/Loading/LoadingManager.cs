using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class LoadingManager : MonoBehaviour
{
    public Canvas loadingCanvas;  // Reference to the loading canvas
    public Slider progressBar;    // Reference to the slider
    public float loadSpeed = 0.1f; // Speed at which the progress bar fills up

    public void LoadLevel(int sceneIndex)
    {
        StartCoroutine(LoadSceneAsync(sceneIndex));
    }

    IEnumerator LoadSceneAsync(int sceneIndex)
    {
        // Activate the loading canvas immediately
        loadingCanvas.gameObject.SetActive(true);

        // Start the asynchronous scene loading
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        // Disable scene activation until the slider reaches its maximum
        operation.allowSceneActivation = false;

        // Progress slider independently of the scene loading
        float sliderProgress = 0f;
        while (!operation.isDone)
        {
            // Gradually fill the slider until the scene is loaded
            if (sliderProgress < 0.9f)
            {
                sliderProgress += loadSpeed * Time.deltaTime;
                progressBar.value = sliderProgress;
            }

            // Once the scene loading reaches 90%, let the slider finish filling
            if (operation.progress >= 0.9f)
            {
                progressBar.value = 1f; // Set the slider to 100% when the scene is ready
                operation.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}
