using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetFinder : MonoBehaviour
{
    [SerializeField] private GuyScript crowd;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            crowd.TargetSelect(other);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            crowd.TargetSelect(null);
    }
}

