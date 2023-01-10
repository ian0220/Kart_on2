using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheel : MonoBehaviour
{
    public bool powered;
    public float maxAngle;
    public float offset;

    private float turnAngle;
    [SerializeField] private WheelCollider wCol;
    [SerializeField] private Transform wMesh;

    private void Start()
    {
        wCol = GetComponentInChildren<WheelCollider>();
        wMesh = transform.Find("WheelObject");
    }

    public void Steer(float steerInput)
    {
        turnAngle = steerInput * maxAngle + offset;
        wCol.steerAngle = turnAngle;
    }

    public void Accelerate(float powerInput)
    {
        if (powered) wCol.motorTorque = powerInput;
        else wCol.brakeTorque = 0;
    }

    public void UpdatePosition()
    {
        Vector3 pos = transform.position;
        Quaternion rot = transform.rotation;

        wCol.GetWorldPose(out pos, out rot);
        wMesh.transform.position = pos;
        wMesh.transform.rotation = rot;
    }
}
