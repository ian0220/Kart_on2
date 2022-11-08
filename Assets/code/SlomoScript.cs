using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlomoScript : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            Time.timeScale = 0.3f;
            Debug.Log("Stop");
        }
        else if (Input.GetKeyUp(KeyCode.K))
        {
            Time.timeScale = 1;
            Debug.Log("Continue");
        }
    }
}
