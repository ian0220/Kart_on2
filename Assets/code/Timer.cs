using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] private float timerTime;
    [SerializeField] private float timer;
    [SerializeField] private float endTime;
    [SerializeField] private float worstTime;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI endTimerText;
    [SerializeField] private TextMeshProUGUI worstTimerText;

    private bool goTimer;


    void Start()
    {
        timer = timerTime;
        goTimer = false;
    }

    void Update()
    {
        if (goTimer == true)
            timer += Time.deltaTime;

        timer = Mathf.Round(timer * 100) * 0.01f;
        endTime = Mathf.Round(endTime * 100) * 0.01f;
        worstTime = Mathf.Round(worstTime * 100) * 0.01f;

        timerText.text = timer.ToString();
        endTimerText.text = endTime.ToString();
        worstTimerText.text = worstTime.ToString();

        if (Input.GetKeyDown(KeyCode.O))
            EndTime();

        if (Input.GetKeyDown(KeyCode.P))
        {
            timer = 0;
            goTimer = true;
        }
    }

    public void EndTime()
    {
        goTimer = false;
        endTime = timer;

        if (endTime == 0 || endTime > worstTime)
            worstTime = endTime;

        PlayerPrefs.SetFloat("Worst time", worstTime);
    }
}
