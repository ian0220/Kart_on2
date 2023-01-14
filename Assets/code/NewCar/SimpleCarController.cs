using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleCarController : MonoBehaviour
{
    // tutorial used: https://www.youtube.com/watch?v=j6_SMdWeGFI

    private float horizontalInput;
    private float verticalInput;
    private float steeringAngle;

    public WheelCollider frontDriverW, frontPassengerW;
    public WheelCollider rearDriverW, rearPassengerW;
    public Transform frontDriverT, frontPassengerT;
    public Transform rearDriverT, rearPassengerT;
    public float rayRange;
    public float maxSteerAngle = 30f;
    public float speed = 50f;
    public float grassSpeed = 30f;

    [SerializeField] private LayerMask floorLayer;
    [SerializeField] private LayerMask grassLayer;

    private void FixedUpdate()
    {
        GetInput();
        Steer();
        Accelerate();
        UpdateWheelPoses();
    }

    public void GetInput()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
    }

    private void Steer()
    {
        steeringAngle = maxSteerAngle * horizontalInput;
        frontDriverW.steerAngle = steeringAngle;
        frontPassengerW.steerAngle = steeringAngle;
    }

    private void Accelerate()
    {
        frontDriverW.motorTorque = speed;
        frontPassengerW.motorTorque = speed;

        //RaycastHit hit;
        //if (Physics.Raycast(transform.position, transform.forward, out hit, rayRange, floorLayer))
        //{
        //    frontDriverW.motorTorque = speed;
        //    frontPassengerW.motorTorque = speed;
        //}
        //if (Physics.Raycast(transform.position, transform.forward, out hit, rayRange, grassLayer))
        //{
        //    frontDriverW.motorTorque = grassSpeed;
        //    frontPassengerW.motorTorque = grassSpeed;
        //}

    }

    private void UpdateWheelPoses()
    {
        UpdateWheelPose(frontDriverW, frontDriverT);
        UpdateWheelPose(frontPassengerW, frontPassengerT);
        UpdateWheelPose(rearDriverW, rearDriverT);
        UpdateWheelPose(rearPassengerW, rearPassengerT);
    }

    private void UpdateWheelPose(WheelCollider wheelCollider, Transform transform)
    {
        Vector3 position = transform.position;
        Quaternion rotation = transform.rotation;

        wheelCollider.GetWorldPose(out position, out rotation);

        transform.position = position;
        transform.rotation = rotation;
    }


}
