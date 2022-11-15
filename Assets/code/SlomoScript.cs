using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlomoScript : MonoBehaviour
{
    [SerializeField] private float slomoSpeed;
    [SerializeField] private float slomoDuration;
    [SerializeField] private float slomoTimer;

    void Update()
    {
        if (slomoTimer < slomoDuration && Input.GetKey(KeyCode.K))
        {
            slomoTimer += Time.deltaTime;
            Time.timeScale = slomoSpeed;

            //if (Input.GetKeyDown(KeyCode.K))
            //{
            //    Debug.Log("Stop");
            //    Time.timeScale = slomoSpeed;
            //}
            //else if (Input.GetKeyUp(KeyCode.K))
            //{
            //    Time.timeScale = 1;
            //    Debug.Log("Continue");
            //}
        }
        else if (slomoTimer > 0)
        {
            slomoTimer -= Time.deltaTime;
            Time.timeScale = 1;
        }

        if (slomoTimer <= 0)
            slomoTimer = 0;
    }
}
