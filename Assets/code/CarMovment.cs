using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovment : MonoBehaviour
{


    [SerializeField] private float m_YVerhogen = 3.76f;
    [SerializeField]private Rigidbody m_RB;
    [SerializeField]private float m_TurnStrength = 10f, m_DriftStrengt = 2;
    [SerializeField] float GrafetyForce = 5;

    [Header("Drift")]
    [SerializeField]private float m_endtimer = 5;
    private float m_Driftto = 0;
    private bool IsDrifting = false;
    private float m_timer;

    [Header("MovmentData")]
    [SerializeField] private ScriptelbelPlayerMovment m_NormalPlayermovment;
    [SerializeField] private ScriptelbelPlayerMovment m_DriftMovment;
    [SerializeField] private ScriptelbelPlayerMovment m_FlyingMovement;

    [Header("Raycast")]
    [SerializeField] LayerMask FloorLayer;
    [SerializeField] LayerMask NothingLayer;
    [SerializeField] float RayRange;
    [SerializeField] Transform BeginPointRay;

    [Header("CarArt")]
    [SerializeField] Transform CarArtTransform;


    [Header("private")]
    private bool OnGround;
    private float m_SpeedInput, m_TurnInput;
    void Start()
    {
        m_RB.transform.parent = null;
    }

    
    void Update()
    {
        m_TurnInput = Input.GetAxis("Horizontal");
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0f, m_TurnInput * m_TurnStrength * Time.deltaTime * 100f, 0f));

        transform.position = m_RB.transform.position + new Vector3(0, m_YVerhogen, 0);

        if (Input.GetKey(KeyCode.LeftShift) && (Input.GetKeyDown(KeyCode.A)))
        {
            m_Driftto = -1f;
            IsDrifting = true;
        }
        else if (Input.GetKey(KeyCode.LeftShift) && (Input.GetKeyDown(KeyCode.D)))
        {
            m_Driftto = 1f;
            IsDrifting = true;
        }

        if (IsDrifting)
        {
            Drifting(m_Driftto);
        }

        if (transform.position.y < -5)
        {
            transform.position = new Vector3(0, 5, 0);
        }
    }

    private void FixedUpdate()
    {
        OnGround = false;
        RaycastHit hit;

        if (Physics.Raycast(BeginPointRay.position, -transform.up, out hit, RayRange, FloorLayer))
        {
            OnGround = true;
            print(OnGround);
            transform.rotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
        }




        if (OnGround)
        {
            ForwardMovement();
        }
        else
        {
            print(OnGround);
            m_RB.AddForce(transform.up * -GrafetyForce * 100f);
            m_RB.AddForce(transform.forward * m_FlyingMovement.Speed * 1000f);
        }



    }

    private void ForwardMovement()
    {
        Debug.Log(m_RB.velocity.magnitude);
        if (m_RB.velocity.magnitude < m_NormalPlayermovment.MaxSpeed)
        {
            m_RB.AddForce(transform.forward * m_NormalPlayermovment.Speed * 1000f);
        }
    }

    private void Drifting(float _TuringTo)
    {
        //print("in clas drift");
        m_timer += Time.deltaTime;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            //print("holding shift");
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0f, _TuringTo * m_DriftStrengt * Time.deltaTime, 0f));
        }
        else
        {
            m_timer = 0;
            IsDrifting = false;
        }
    }
}
