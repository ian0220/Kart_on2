using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EmotionManager : MonoBehaviour
{
    [SerializeField] private Slider emotionBar;
    [SerializeField] private int emotion;
    [SerializeField] private int maxEmotion;
    [SerializeField] private int minEmotion;

    void Start()
    {
        emotionBar.maxValue = maxEmotion;
        emotionBar.minValue = minEmotion;
    }

    void Update()
    {
        emotionBar.value = emotion;

        if (emotion > 100)
            emotion = 100;

        if (emotion < -100)
            emotion = -100;
    }

    public void ChangeEmotion(int emotionChange)
    {
        emotion += emotionChange;
    }
}
