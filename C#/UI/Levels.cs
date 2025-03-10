using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Levels : MonoBehaviour
{
    public void levelOne()
    {
        SceneManager.LoadScene(0);
    }
    public void levelTwo()
    {
        SceneManager.LoadScene(1);
    }
    public void levelThree()
    {
        SceneManager.LoadScene(2);
    }
    
    public void levelFour()
    {
        SceneManager.LoadScene(3);
    }


}
