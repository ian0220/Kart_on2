using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseObject;
    private bool isPaused;

    void Start()
    {
        pauseObject.SetActive(false);
        isPaused = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (isPaused == false)
            {
                pauseObject.SetActive(true);
                isPaused = true;
                Time.timeScale = 0;
            }
        }
    }

    public void ContinueGame()
    {
        pauseObject.SetActive(false);
        isPaused = false;
        Time.timeScale = 1;
    }
}
