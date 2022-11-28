using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovment : MonoBehaviour
{


    [SerializeField] private float m_YVerhogen = 3.76f;
    [SerializeField]private Rigidbody m_RB;
    [SerializeField]private float m_DriftStrengt = 2;
    [SerializeField] float GrafetyForce = 5;

    [Header("Drift")]
    [SerializeField]private float m_endtimer = 5;
    private float m_Driftto = 0;
    private bool IsDrifting = false;
    private float m_timer;
    private bool GiveBoost = false;
    [SerializeField] float SetBoostSpeed;

    [Header("MovmentData")]
    [SerializeField] private ScriptelbelPlayerMovment m_NormalPlayermovment;
    [SerializeField] private ScriptelbelPlayerMovment m_DriftMovment;
    [SerializeField] private ScriptelbelPlayerMovment m_FlyingMovement;
    private float boostspeed;

    [Header("Raycast")]
    [SerializeField] LayerMask FloorLayer;
    [SerializeField] LayerMask NothingLayer;
    [SerializeField] float RayRange;
    [SerializeField] Transform BeginPointRay;

    [Header("CarArt")]
    [SerializeField] Transform m_CarArtTransform;
    [SerializeField] float m_yasARTCar;
    private float test;


    [Header("private")]
    private bool OnGround;
    private float m_SpeedInput, m_TurnInput;
    private float m_TurnStrength;
    private float m_Speed;
    private float m_MaxSpeed;
    void Start()
    {
        m_RB.transform.parent = null;
    }

    
    void Update()
    {

        m_TurnInput = Input.GetAxis("Horizontal");
        SetOverData();

        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0f, m_TurnInput * m_TurnStrength * Time.deltaTime * 10f, 0f));
        transform.position = m_RB.transform.position + new Vector3(0, m_YVerhogen, 0);
        ToDrifting();
        OfTheWorld();
    }

    private void SetOverData()
    {
        if (OnGround && (!IsDrifting))
        {
            m_TurnStrength = m_NormalPlayermovment.TuringSpeed;
            m_Speed = m_NormalPlayermovment.Speed;
            m_MaxSpeed = m_NormalPlayermovment.MaxSpeed;
        }
        else if (OnGround && (IsDrifting))
        {
            m_TurnStrength = m_DriftMovment.TuringSpeed;
            m_Speed = m_DriftMovment.Speed;
            m_MaxSpeed = m_DriftMovment.MaxSpeed;
        }
        else if (!OnGround && (!IsDrifting))
        {
            m_TurnStrength = m_FlyingMovement.TuringSpeed;
            m_Speed = m_FlyingMovement.Speed;
            m_MaxSpeed = m_FlyingMovement.MaxSpeed;
        }
    }

    private void ToDrifting()
    {
        if (Input.GetKey(KeyCode.A) && (Input.GetKey(KeyCode.LeftShift)) && (!IsDrifting))
        {
            m_Driftto = -10f;
            test = -m_yasARTCar;
            IsDrifting = true;
        }
        else if (Input.GetKey(KeyCode.D) && (Input.GetKey(KeyCode.LeftShift)) && (!IsDrifting))
        {

            test = m_yasARTCar;
            m_Driftto = 10f;
            IsDrifting = true;
        }

        if (IsDrifting)
        {
            Drifting(m_Driftto, test);
        }
    }

    private void OfTheWorld()
    {
        if (m_RB.transform.position.y < -5)
        {
            m_RB.transform.position = new Vector3(0, 5, 0);
        }
    }

    private void FixedUpdate()
    {
        CheckOnGround();

        ForwardMovement();
        if (!OnGround)
        {       
            m_RB.AddForce(transform.up * -GrafetyForce * 100f);
        }

        print(m_timer);
        print(GiveBoost);

    }

    private void CheckOnGround()
    {
        // schiet een race cats naar beneden om te kijken of die de layer raakt als dat zo is dan voert die de if uit
        OnGround = false;
        RaycastHit hit;

        if (Physics.Raycast(BeginPointRay.position, -transform.up, out hit, RayRange, FloorLayer))
        {
            OnGround = true;
            transform.rotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
        }
    }

    private void ForwardMovement()
    {
      //  Debug.Log(m_RB.velocity.magnitude);
        if (m_RB.velocity.magnitude < m_MaxSpeed ||(GiveBoost))
        {
            m_Speed += boostspeed;
            m_RB.AddForce(transform.forward * m_Speed* Time.fixedDeltaTime * 1000f);
        }
    }

    private void Drifting(float _TuringTo ,float _yasartcar)
    {
        m_timer += Time.deltaTime;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0f, _TuringTo * m_DriftStrengt * Time.deltaTime, 0f));
            
            m_CarArtTransform.localRotation = Quaternion.Euler(new Vector3(0, _yasartcar, 0));
        }
        else
        {
            if(m_timer >= m_endtimer)
            {
                StartCoroutine(Boost());
            }
            m_CarArtTransform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
            m_timer = 0;
            IsDrifting = false;
        }
    }

    private IEnumerator Boost()
    {
        GiveBoost = true;
        boostspeed = SetBoostSpeed;
        yield return new WaitForSeconds(3f);
        GiveBoost = false;
        boostspeed = 0;
        yield return null;
    }
}
