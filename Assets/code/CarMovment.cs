using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovment : MonoBehaviour
{
    [SerializeField] private float m_YVerhogen = 3.76f;
    [SerializeField] private Rigidbody m_RB;
    [SerializeField] private float m_DriftStrengt = 2;
    [SerializeField] float GrafetyForce = 5;

    [Header("Player Bools")]
    [SerializeField] private bool m_PlayerOne;
    [SerializeField] private bool m_PlayerTwo;

    [Header("Drift")]
    [SerializeField] private float m_endtimer = 5;
    private float m_Driftto = 0;
    private bool IsDrifting = false;
    private float m_timer;
    [SerializeField] int m_timeInfrimeLerp = 5;
    [SerializeField] float m_HowLongToLerp = 50f;

    [Header("Boost")]
    private bool GiveBoost = false;
    [SerializeField] float SetBoostSpeed;
    [SerializeField] float m_BoostTime = 0;
    private float m_timerboost;

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
    private float m_YasCarArtGoTo;    

    [Header("private")]
    private bool OnGround;
    private float m_SpeedInput, m_TurnInput;
    private float m_TurnStrength;
    private float m_Speed;
    private float m_MaxSpeed;
    private float Lerpnummer = 0;
    void Start()
    {
        m_RB.transform.parent = null;
    }


    void Update()
    {
        // welke kant die op gaat drijen en hoe die rijd
        m_TurnInput = Input.GetAxis("Horizontal");
        SetOverData();

        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0f, m_TurnInput * m_TurnStrength * Time.deltaTime * 10f, 0f));
        transform.position = m_RB.transform.position + new Vector3(0, m_YVerhogen, 0);
        ToDrifting();
        OfTheWorld();
        if(GiveBoost)
        {
            Boost2_0();
        }
        Priten();
    }

    private void SetOverData()
    {
        // laat de movment bepalen in wele status het zit
        if (OnGround && (!IsDrifting))
        {
            m_TurnStrength = m_NormalPlayermovment.TuringSpeed;
            m_Speed = m_NormalPlayermovment.Speed;
            m_MaxSpeed = m_NormalPlayermovment.MaxSpeed;
            if(GiveBoost)
            {
                m_MaxSpeed += SetBoostSpeed;
            }
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
        if (m_PlayerOne == true)
        {
            // controleerd welke kand op de kijken met de drift
            if (Input.GetKey(KeyCode.A) && (Input.GetKey(KeyCode.LeftShift)) && (!IsDrifting))
            {
                m_Driftto = -10f;
                m_YasCarArtGoTo = -m_yasARTCar;
                IsDrifting = true;
            }
            else if (Input.GetKey(KeyCode.D) && (Input.GetKey(KeyCode.LeftShift)) && (!IsDrifting))
            {

                m_YasCarArtGoTo = m_yasARTCar;
                m_Driftto = 10f;
                IsDrifting = true;
            }
            // laat de lerp nummer opnieuw beginnen zo dat die weer kan lerpen naar de juisten kant
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                Lerpnummer = 0;
            }
            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                m_YasCarArtGoTo = 0f;
                Lerpnummer = 0;
            }
            // lerp naar eem kant to 
            m_CarArtTransform.localRotation = Quaternion.Lerp(m_CarArtTransform.localRotation, Quaternion.Euler(new Vector3(0, m_YasCarArtGoTo, 0)), Lerpnummer);
            Lerpnummer += 0.5f * Time.deltaTime;
        }

        if (m_PlayerTwo == true)
        {
            // controleerd welke kand op de kijken met de drift
            if (Input.GetKey(KeyCode.LeftArrow) && (Input.GetKey(KeyCode.RightShift)) && (!IsDrifting))
            {
                m_Driftto = -10f;
                m_YasCarArtGoTo = -m_yasARTCar;
                IsDrifting = true;
            }
            else if (Input.GetKey(KeyCode.RightArrow) && (Input.GetKey(KeyCode.RightShift)) && (!IsDrifting))
            {

                m_YasCarArtGoTo = m_yasARTCar;
                m_Driftto = 10f;
                IsDrifting = true;
            }
            // laat de lerp nummer opnieuw beginnen zo dat die weer kan lerpen naar de juisten kant
            if (Input.GetKeyDown(KeyCode.RightShift))
            {
                Lerpnummer = 0;
            }
            if (Input.GetKeyUp(KeyCode.RightShift))
            {
                m_YasCarArtGoTo = 0f;
                Lerpnummer = 0;
            }
            // lerp naar eem kant to 
            m_CarArtTransform.localRotation = Quaternion.Lerp(m_CarArtTransform.localRotation, Quaternion.Euler(new Vector3(0, m_YasCarArtGoTo, 0)), Lerpnummer);
            Lerpnummer += 0.5f * Time.deltaTime;
        }

        
        if (IsDrifting)
        {
            Drifting(m_Driftto);
        }
    }


    private void FixedUpdate()
    {
        CheckOnGround();
                           
        ForwardMovement();
        // als die niet op de grond zit dan geeft die meer grafetie force 
        if (!OnGround)
        {
            m_RB.AddForce(transform.up * -GrafetyForce * 100f);
        }
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
        if (m_RB.velocity.magnitude < m_MaxSpeed || (GiveBoost))
        {

            m_RB.AddForce(transform.forward * m_Speed * Time.fixedDeltaTime * 1000f);
        }
    }

    private void Drifting(float _TuringTo)
    {
        // drift is ingedrukt dan geeft die tijd mee en lerpt die naar zij kant zo dra los kijk og boost mag geven
        if (Input.GetKey(KeyCode.LeftShift))
        {
            m_timer += Time.deltaTime;
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0f, _TuringTo * m_DriftStrengt * Time.deltaTime, 0f));
        }
        else
        {           
            if (m_timer >= m_endtimer)
            {
                GiveBoost = true;
            }
            m_timer = 0;
            IsDrifting = false;
        }      
    }

    //public IEnumerator Boost(float _SetBoostSpeed)
    //{
    //    GiveBoost = true;
    //    boostspeed = _SetBoostSpeed;
    //    m_Speed += boostspeed;
    //    yield return new WaitForSeconds(m_BoostTime);
    //    GiveBoost = false;
    //    boostspeed = 0;
    //    m_Speed += boostspeed;
    //    yield return null;
    //}

    /// <summary>
    /// boost de car 
    /// </summary>

    public void Boost2_0() 
    {
        if(m_timerboost <= m_BoostTime && !IsDrifting)
        {
            // zet boost tijd en boost als de tijd over doe is reset die het
            m_timerboost += Time.deltaTime;
            boostspeed = SetBoostSpeed;
        }
        else
        {
            boostspeed = 0;
            m_timerboost = 0;
            GiveBoost = false;
        }
        m_Speed += boostspeed;
    }
    private void OfTheWorld()
    { // als van de wereld repand
        if (m_RB.transform.position.y < -5)
        {
            m_RB.transform.position = new Vector3(0, 5, 0);
        }
    }

    private void Priten()
    {
        print(GiveBoost);
        print(m_timerboost);
        //print(m_Speed);
        //print(m_RB.velocity.magnitude);
        //print(Lerpnummer);
        // print(m_timer);
        // print(m_RB.velocity.magnitude);
    }
}
