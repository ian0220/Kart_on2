using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlomoScript : MonoBehaviour
{
    [SerializeField] private float slomoSpeed;
    [SerializeField] private float slomoDuration;
    [SerializeField] private float slomoTimer;

    [SerializeField] private Slider boostBar;
    private bool canSlomo;

    private void Start()
    {
        boostBar.maxValue = slomoDuration;
        canSlomo = true;
    }

    void Update()
    {
        boostBar.value = slomoTimer;

        if (Input.GetKeyUp(KeyCode.K) || slomoTimer > slomoDuration)
            canSlomo = false;

        if (slomoTimer < slomoDuration && Input.GetKey(KeyCode.K) && canSlomo == true)
        {
            slomoTimer += Time.deltaTime;
            Time.timeScale = slomoSpeed;
        }
        else if (slomoTimer > 0)
        {
            slomoTimer -= 0.25f *  Time.deltaTime;
            Time.timeScale = 1;
        }

        if (slomoTimer <= 0)
        {
            slomoTimer = 0;
            canSlomo = true;
        }
    }
}
