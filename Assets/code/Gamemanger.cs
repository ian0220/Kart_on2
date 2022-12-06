using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamemanger : MonoBehaviour
{
    public static Gamemanger SingGame;

    public GameObject[] CheckPointsArray;
    public GameObject Finsh;

   //[SerializeField]
   // private speherecollider m_ColliderScript;
    private void Awake()
    {
        if(SingGame == null)
        {
            SingGame = this;
        }
        else
        {
            Destroy(this);
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
