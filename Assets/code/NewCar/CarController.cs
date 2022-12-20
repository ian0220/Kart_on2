using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    // Tutorial used for car controller: https://www.youtube.com/watch?v=BSybcKPQCnc

    public float moveSpeed;
    public float steerAngle;

    public float maxSpeed;
    public float drag;

    public float traction;

    private Vector3 moveForce;

    void Update()
    {
        // to move forward and backward add - Input.GetAxis("Vertical")
        moveForce += transform.forward * moveSpeed * Time.deltaTime;
        transform.position += moveForce * Time.deltaTime;

        float steerInput = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.up * steerInput * moveForce.magnitude * steerAngle * Time.deltaTime);

        moveForce *= drag;
        moveForce = Vector3.ClampMagnitude(moveForce, maxSpeed);

        Debug.DrawRay(transform.position, moveForce.normalized * 3);
        Debug.DrawRay(transform.position, transform.forward * 3, Color.blue);
        moveForce = Vector3.Lerp(moveForce.normalized, transform.forward, traction * Time.deltaTime) * moveForce.magnitude;
    }
}
