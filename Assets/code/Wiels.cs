using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wiels : MonoBehaviour
{
    [SerializeField] private Transform m_LinksForWiel, m_RechtsForWiel,m_LinksAchterWiel, m_RechtsAchterWiel;

    [SerializeField] private float m_RotatingSpeed;

    void Update()
    {
        RotateWiel(m_LinksForWiel);
        RotateWiel(m_RechtsForWiel);
        RotateWiel(m_LinksAchterWiel);
        RotateWiel(m_RechtsAchterWiel);
    }

    private void RotateWiel(Transform _TransformWiel)
    {
        _TransformWiel.Rotate(m_RotatingSpeed * Time.deltaTime, 0, 0);
    }
}
