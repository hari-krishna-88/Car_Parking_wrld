using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
//using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartAndHome : MonoBehaviour
{
    
    public GameObject fadeIn;
    private void Start()
    {
        fadeIn.SetActive(false);
    }
    public void Restart()
    {
        fadeIn.SetActive(true);
        Invoke("RestartAfter1S", 1);
    }
    void RestartAfter1S()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
    }

    public void Home()
    {
        SceneManager.LoadScene(0);
    }


}
