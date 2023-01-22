using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class CameraInilzar : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] GameObject VirCamera;
    public int Layer;
    // Start is called before the first frame update
    void Start()
    {
        CarHelper[] carmovement = FindObjectsOfType<CarHelper>();


        VirCamera.layer = Layer;

        var bitmask = (1 << Layer)
                        | (1 << 0)
                        | (1 << 1)
                        | (1 << 2)
                        | (1 << 3)
                        | (1 << 4)
                        | (1 << 5)
                        | (1 << 6)
                        | (1 << 7)
                        | (1 << 8);

        cam.cullingMask = bitmask;
        cam.gameObject.layer = Layer;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
