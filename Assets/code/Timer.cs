using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] private float timerTime;
    [SerializeField] private float timer;
    [SerializeField] private TextMeshProUGUI timerText;

    void Start()
    {
        timer = timerTime;
    }

    void Update()
    {
        timer -= Time.deltaTime;
        timerText.text = timer.ToString();
    }
}
