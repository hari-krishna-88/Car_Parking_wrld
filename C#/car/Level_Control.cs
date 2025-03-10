using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level_Control : MonoBehaviour
{
    public int firstLevelIndex = 2; // First level's build index
    public int lastLevelIndex = 5;  // Last level's build index

    public void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        // If the next scene index exceeds the last level index, wrap back to the first level
        if (nextSceneIndex > lastLevelIndex)
        {
            nextSceneIndex = firstLevelIndex;
        }

        Debug.Log($"Loading Scene {nextSceneIndex} from Scene {currentSceneIndex}");
        SceneManager.LoadScene(nextSceneIndex);
    }

    public void Home()
    {
        SceneManager.LoadScene(0); // Load the home screen
    }
}
