using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Settings : MonoBehaviour
{
    public GameObject SettingsPopUp;
    public PlayableDirector director; // for time line 

    private bool CloseButtonPresssed = false;
    

    private void Awake()
    {
        SettingsPopUp.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SettingsSelected()
    {
       // CloseButtonPresssed = false;
        SettingsPopUp.SetActive(true);
        //Invoke("PlayableDirectorController", 0.88f);
       // Debug.Log("the setingsSelected");
    }
    public void Close()
    {
        director.Play();
        CloseButtonPresssed = true;
        SettingsPopUp.SetActive(false);
    }

    public void PlayableDirectorController()
    {
        Debug.Log("paused");
        director.Pause();
    }
}
