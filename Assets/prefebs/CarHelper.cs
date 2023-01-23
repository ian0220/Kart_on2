using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarHelper : MonoBehaviour
{
    [SerializeField] private Rigidbody rbody;
    [SerializeField] private Transform racatpoint;
    [SerializeField] private Transform cararttf;

    [SerializeField] private ParticleSystem particleSystem;

    public Rigidbody Rbody => rbody;
    public Transform Racetpoint => racatpoint;
    public Transform CarArttf => cararttf;
    public ParticleSystem ParticleSystem => particleSystem;
}
