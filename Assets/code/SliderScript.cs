using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderScript : MonoBehaviour
{
   

    public Slider slider1; //connected the slider
    public Image slider1Fill; //connected the Image Fill from the slider

    slider1Fill.color = Color.Lerp(Color.red, Color.green, slider1.value / 100);
}
