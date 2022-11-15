using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinSwitch : MonoBehaviour
{
    [SerializeField] private int skinNumber;

    [SerializeField] private Material skinOne;
    [SerializeField] private Material skinTwo;
    [SerializeField] private Material skinThree;

    private Material currentSkin;

    void Start()
    {
        skinNumber = 1;
    }

    void Update()
    {
        gameObject.GetComponent<Renderer>().material = currentSkin;

        switch (skinNumber)
        {
            case 1:
                currentSkin = skinOne;
                break;
            case 2:
                currentSkin = skinTwo;
                break;
            case 3:
                currentSkin = skinThree;
                break;
        }
    }

    public void NextSkin()
    {
        skinNumber++;

        if (skinNumber > 3)
            skinNumber = 1;
    }
}
