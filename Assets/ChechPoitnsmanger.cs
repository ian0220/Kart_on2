using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChechPoitnsmanger : MonoBehaviour
{
    [SerializeField]private List<GameObject> chechpoint = new List<GameObject>();
    [SerializeField] private GameObject timerObject;

    [SerializeField]private GameObject finish;

    public List<GameObject> CheckPoints => chechpoint;

    public GameObject Finish => finish;

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
