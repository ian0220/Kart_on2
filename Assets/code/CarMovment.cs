using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovment : MonoBehaviour
{
    [SerializeField]private Rigidbody m_Bal;
    [SerializeField]private float m_MaxSpeed = 8f,m_Speed = 5f,m_TurnStrength = 10f;

    private float m_SpeedInput, m_TurnInput;
    void Start()
    {
        m_Bal.transform.parent = null;
    }

    
    void Update()
    {
        m_TurnInput = Input.GetAxis("Horizontal");

        // transform.position = Quaternion.Euler(transform.rotation.eulerAngles); //Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0f, m_TurnInput * m_TurnStrength * Time.deltaTime, 0f));
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0f, m_TurnInput * m_TurnStrength * Time.deltaTime * 100f, 0f));

        transform.position = m_Bal.transform.position;
    }

    private void FixedUpdate()
    {
        m_Bal.AddForce(transform.forward * m_Speed * Time.fixedDeltaTime * 10000f);
    }
}
