using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarGradient : MonoBehaviour
{
    public Gradient gradient;
    public Image fill;
    public Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        fill.color = gradient.Evaluate(1f);
    }

    // Update is called once per frame
    void Update()
    {
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
