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

    private void Start()
    {
        boostBar.maxValue = slomoDuration;
    }

    void Update()
    {
        boostBar.normalizedValue = slomoTimer;
        if (slomoTimer < slomoDuration && Input.GetKey(KeyCode.K))
        {
            slomoTimer += Time.deltaTime;
            Time.timeScale = slomoSpeed;
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
