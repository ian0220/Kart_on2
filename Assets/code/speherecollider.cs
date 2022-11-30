using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class speherecollider : MonoBehaviour
{
    private int m_chechpointshad = 0; // how much check point you went troe

    private void Awake()
    {
        m_chechpointshad = 0;
        Gamemanger.SingGame.Finsh.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Finsh"))
        {
            // if you went troe the finsh say you went troe it and go to chechpointactifate
            CheckPointActifate();

        }

        if(other.CompareTag("CheckPoint"))
        {
            // if you went tro a checkpoint turn it off and go to chechpointchecker()
            other.gameObject.SetActive(false);            
            CheckPointChecker();
        }
    }

    private void CheckPointActifate()
    {
        // if you wnet troe start tel you have al the check point back turn the check point you had back to 0 and turn al the checkpoints back on
        m_chechpointshad = 0;
        for (int i = 0; i < Gamemanger.SingGame.CheckPointsArray.Length; i++)
        {
            Gamemanger.SingGame.CheckPointsArray[i].SetActive(true);
        }
        Gamemanger.SingGame.Finsh.SetActive(false);
    }

    private void CheckPointChecker()
    {
        // if you went trow a check point the say had the check point you had and check if you hade al the check point
        m_chechpointshad += 1;
        if (m_chechpointshad == Gamemanger.SingGame.CheckPointsArray.Length)
        {
            Gamemanger.SingGame.Finsh.SetActive(true);
        }
    }

    private void Update()
    {
       
    }
}
