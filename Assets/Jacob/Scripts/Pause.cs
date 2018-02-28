using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour {

    bool paused = false;
    GameObject UI;

    void Start()
    {
        UI = GameObject.Find("PauseUI");
        UI.SetActive(false);
    }

    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            paused = togglePause();
        }
    }

    bool togglePause()
    {
        if(Time.timeScale == 0f)
        {
            Time.timeScale = 1f;
            UI.SetActive(false);
            return (false);
        }
        else
        {
            Time.timeScale = 0f;
            UI.SetActive(true);
            return (true);
        }
    }
}
