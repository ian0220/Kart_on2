using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontLoad : MonoBehaviour
{

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

   
}
