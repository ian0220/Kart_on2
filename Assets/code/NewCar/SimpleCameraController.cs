using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleCameraController : MonoBehaviour
{
    // tutorial used: https://www.youtube.com/watch?v=j6_SMdWeGFI

    public Transform objectToFollow;
    public Vector3 offset;
    public float followSpeed = 10f;
    public float lookSpeed = 10f;

    private void FixedUpdate()
    {
        LookAtTarget();
        MoveToTarget();        
    }
    private void LookAtTarget()
    {
        Vector3 lookDirection = objectToFollow.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(lookDirection, Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, lookSpeed * Time.deltaTime);
    }

    private void MoveToTarget()
    {
        Vector3 targetPos = objectToFollow.position +
                            objectToFollow.forward * offset.z +
                            objectToFollow.right * offset.x +
                            objectToFollow.up * offset.y;
        transform.position = Vector3.Lerp(transform.position,targetPos, followSpeed * Time.deltaTime);
    }
}
