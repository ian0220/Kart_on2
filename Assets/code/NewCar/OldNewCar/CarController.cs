using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    // Tutorial used for car controller: https://www.youtube.com/watch?v=BSybcKPQCnc

    [Header("Car Controller")]
    public float moveSpeed;
    public float turnStrength;
    public float maxSpeed;
    public float drag;
    public float traction;
    private Vector3 moveForce;

    [Header("Ground Ray")]
    [SerializeField] private LayerMask floorLayer;
    [SerializeField] private LayerMask grassLayer;
    [SerializeField] private LayerMask wallLayer;
    public float rayRange;
    private bool onGround;
    private bool onGrass;

    void Update()
    {
        RaycastHit hit;
        if (!Physics.Raycast(transform.position, transform.forward, out hit, rayRange, wallLayer))
        {
            moveForce += transform.forward * moveSpeed * Time.deltaTime;
            transform.position += moveForce * Time.deltaTime;
        }

        float steerInput = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.up * steerInput * moveForce.magnitude * turnStrength * Time.deltaTime);

        moveForce *= drag;
        moveForce = Vector3.ClampMagnitude(moveForce, maxSpeed);

        Debug.DrawRay(transform.position, moveForce.normalized * 3);
        Debug.DrawRay(transform.position, transform.forward * 3, Color.blue);
        moveForce = Vector3.Lerp(moveForce.normalized, transform.forward, traction * Time.deltaTime) * moveForce.magnitude;
    }

    private void FixedUpdate()
    {
        //RaycastHit hit;
        //if (!Physics.Raycast(transform.position, transform.forward, out hit, rayRange, wallLayer))
        //{

        //}

        //if (Physics.Raycast(transform.position, -transform.up, out hit, rayRange, floorLayer))
        //{
        //    onGround = true;
        //    //transform.rotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
        //}
        //else if (Physics.Raycast(transform.position, -transform.up, out hit, rayRange, grassLayer))
        //{
        //    Debug.Log("Grass");
        //    onGrass = true;
        //    onGround = true;
        //    //transform.rotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
        //}
    }
}
