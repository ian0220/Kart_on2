using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelTurn : MonoBehaviour
{
    [SerializeField] Transform m_RightWheel;
    [SerializeField] Transform m_LeftWheel;
    [SerializeField] float m_turnAmmount;


    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            m_RightWheel.localRotation = Quaternion.Lerp(m_RightWheel.localRotation, Quaternion.Euler(new Vector3(0, -m_turnAmmount, 0)), 0.5f);
            m_LeftWheel.localRotation = Quaternion.Lerp(m_LeftWheel.localRotation, Quaternion.Euler(new Vector3(0, -m_turnAmmount, 0)), 0.5f);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            m_RightWheel.localRotation = Quaternion.Lerp(m_RightWheel.localRotation, Quaternion.Euler(new Vector3(0, m_turnAmmount, 0)), 0.5f);
            m_LeftWheel.localRotation = Quaternion.Lerp(m_LeftWheel.localRotation, Quaternion.Euler(new Vector3(0, m_turnAmmount, 0)), 0.5f);
        }
        else
        {
            m_RightWheel.localRotation = Quaternion.Lerp(m_RightWheel.localRotation, Quaternion.Euler(new Vector3(0, 0, 0)), 0.5f);
            m_LeftWheel.localRotation = Quaternion.Lerp(m_LeftWheel.localRotation, Quaternion.Euler(new Vector3(0, 0, 0)), 0.5f);
        }
    }
}
