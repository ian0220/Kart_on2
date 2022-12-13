using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddforceSign : MonoBehaviour
{
    public float thrust = 1.0f;
    public Rigidbody rb;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            { 
                if (hit.transform.name == "TestSign")
                {
                    rb = GetComponent<Rigidbody>();
                    rb.AddForce(0, 0, thrust, ForceMode.Impulse);
                    Debug.Log("ZURRZuzuuzr");
                }
            }
        }
    }
}