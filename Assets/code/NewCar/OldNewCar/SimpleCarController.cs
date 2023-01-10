using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleCarController : MonoBehaviour
{
    private float horizontalInput;
    private float verticalInput;
    private float steerAngle;

    public WheelCollider frontDriverW, frontPassengerW;
    public WheelCollider rearDriverW, rearPassengerW;
    public Transform frontDriverT, frontPassengerT;
    public Transform rearDriverT, rearPassengerT;
    public float maxSteerAngle = 30f;
    public float motorForce = 50f;

    public void GetInput()
    {

    }

    private void Steer()
    {

    }

    private void Accelerate()
    {

    }

    private void UpdateWheelPoses()
    {

    }

    private void UpdateWheelPose(WheelCollider wheelCollider, Transform transform)
    {

    }


}
