using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovment : MonoBehaviour
{
    [SerializeField] private ScriptelbelPlayerMovment m_NormalPlayermovment;
    [SerializeField] private ScriptelbelPlayerMovment m_DriftMovment;
    [SerializeField]private Rigidbody m_Bal;
    [SerializeField]private float m_MaxSpeed = 8f,m_Speed = 5f,m_TurnStrength = 10f, m_DriftStrengt = 2;
    private float m_Driftto = 0;
    private bool IsDrifting = false;
    private float m_timer;
    [SerializeField]private float m_endtimer = 5;

    private bool OnGround;
    [SerializeField] float GrafetyForce = 5;

    [Header("Raycast")]
    [SerializeField] LayerMask FloorLayer;
    [SerializeField] LayerMask NothingLayer;
    [SerializeField] float RayRange;
    [SerializeField] Transform BeginPointRay;

    [Header("CarArt")]
    [SerializeField] Transform CarArtTransform;

    private float m_SpeedInput, m_TurnInput;
    void Start()
    {
        m_Bal.transform.parent = null;
    }

    
    void Update()
    {
        m_TurnInput = Input.GetAxis("Horizontal");
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0f, m_TurnInput * m_TurnStrength * Time.deltaTime * 100f, 0f));

        transform.position = m_Bal.transform.position;

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
        if(IsDrifting)
        {
            Drifting(m_Driftto);
        }
    }

    private void FixedUpdate()
    {
        OnGround = false;
        RaycastHit hit;

        if(Physics.Raycast(BeginPointRay.position, -transform.up, out hit,RayRange,FloorLayer))
        {
            OnGround = true;
            print(OnGround);
            transform.rotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
        }

        //if(!OnGround)
        //{
        //    print(OnGround + " 2");
        //    transform.rotation = Quaternion.Euler(0, transform.rotation.y, 0);
        //}

        
        if(OnGround)
        {
            ForwardMovement();
        }
        else
        {
            m_Bal.AddForce(transform.up * GrafetyForce * 100f);
            m_Bal.AddForce(transform.forward * (m_NormalPlayermovment.Speed * 0.5f));
        }

        

    }

    private void ForwardMovement()
    {
       // Debug.Log(m_Bal.velocity.magnitude);
        if (m_Bal.velocity.magnitude < m_NormalPlayermovment.MaxSpeed)
        {
            m_Bal.AddForce(transform.forward * m_NormalPlayermovment.Speed);
        }
    }

    private void Drifting(float _TuringTo )
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
