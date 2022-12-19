using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinSwitch : MonoBehaviour
{
    [SerializeField] private int skinNumber;
    [SerializeField] private GameObject[] carSkins;

    void Start()
    {
        skinNumber = 1;
        SwitchSkin(skinNumber - 1);
    }

    void Update()
    {
        switch (skinNumber)
        {
            case 1:
                SwitchSkin(skinNumber - 1);
                break;
            case 2:
                SwitchSkin(skinNumber - 1);
                break;
            case 3:
                SwitchSkin(skinNumber - 1);
                break;
            case 4:
                SwitchSkin(skinNumber - 1);
                break;
        }
    }

    private void SwitchSkin(int skinNumber)
    {
        for (int i = 0; i < carSkins.Length; i++)
        {
            carSkins[i].SetActive(false);
        }

        carSkins[skinNumber].SetActive(true);
    }

    public void NextSkin()
    {
        skinNumber++;

        if (skinNumber > carSkins.Length)
            skinNumber = 1;
    }

    public void PreviousSkin()
    {
        skinNumber--;

        if (skinNumber < 1)
            skinNumber = carSkins.Length;
    }
}
