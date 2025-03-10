using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AsyncLoader : MonoBehaviour
{
    [Header("Menu Screen")]
    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private GameObject mainMenu;

    [Header("Slider")]
    [SerializeField] private Slider loadinSlider;

    public void LoadLevel(string levelToLoad)
    {
        if (loadingScreen == null || mainMenu == null || loadinSlider == null)
        {
            Debug.LogError("Assign all UI components (loadingScreen, mainMenu, and loadinSlider) in the Inspector.");
            return;
        }

        mainMenu.SetActive(false);
        loadingScreen.SetActive(true);

        StartCoroutine(LoadLevelAsync(levelToLoad));
    }

    IEnumerator LoadLevelAsync(string levelToLoad)
    {
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(levelToLoad);

        // Prevent the scene from activating immediately
        loadOperation.allowSceneActivation = false;

        while (!loadOperation.isDone)
        {
            // Map progress to slider value
            float progressValue = Mathf.Clamp01(loadOperation.progress / 0.9f);
            loadinSlider.value = progressValue;

            // Debug to ensure progress is being tracked
            Debug.Log($"Loading progress: {progressValue * 100}%");

            // Check if loading is almost done
            if (loadOperation.progress >= 0.9f)
            {
                // Keep the slider full while waiting for scene activation
                loadinSlider.value = 1.0f;

                // Activate the scene when ready
                loadOperation.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}
