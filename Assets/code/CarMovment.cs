using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CarMovment : MonoBehaviour
{
    [SerializeField] private float m_YVerhogen = 3.76f;
    [SerializeField] private Rigidbody m_RB;
    [SerializeField] private float m_DriftStrengt = 2;
    [SerializeField] float GrafetyForce = 5;

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
    [SerializeField] private ScriptelbelPlayerMovment m_NormalPlayermovement;
    [SerializeField] private ScriptelbelPlayerMovment m_DriftMovement;
    [SerializeField] private ScriptelbelPlayerMovment m_FlyingMovement;
    [SerializeField] private ScriptelbelPlayerMovment m_GrassMovement;
    [SerializeField] private ScriptelbelPlayerMovment m_GrassDriftMovement;
    private float boostspeed;

    [Header("Raycast")]
    [SerializeField] LayerMask FloorLayer;
    [SerializeField] LayerMask GrassLayer;
    [SerializeField] float RayRange;
    [SerializeField] Transform BeginPointRay;

    [Header("CarArt")]
    [SerializeField] Transform m_CarArtTransform;
    [SerializeField] float m_yasARTCar;
    private float m_YasCarArtGoTo;

    //[Header("WheelTurn")]
    //[SerializeField] Transform m_RightWheel;
    //[SerializeField] Transform m_LeftWheel;
    //[SerializeField] float m_turnAmmount;

    [Header("private")]
    private bool OnGround;
    private float m_SpeedInput, m_TurnInput;
    private float m_TurnStrength;
    private float m_Speed;
    private float m_MaxSpeed;
    private float Lerpnummer = 0;
    private bool OnGrass = false;

    void Start()
    {
        m_RB.transform.parent = null;
        m_TurnInput = 0;
    }
    public void HandleMoveInput(InputAction.CallbackContext context)
    {
        Vector2 direction = context.ReadValue<Vector2>();

        if(direction.x > 0.2f)
        {
            m_TurnInput = 1f;
        }
        else if(direction.x < -0.2f)
        {
            m_TurnInput = -1f;
        }
        else if(direction.x == 0f)
        {
            m_TurnInput = 0f;
        }

        Debug.Log(direction);
        print(m_TurnInput);
    }

    void Update()
    {
        // welke kant die op gaat drijen en hoe die rijd
       
        SetOverData();

        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0f, m_TurnInput * m_TurnStrength * Time.deltaTime * 10f, 0f));
        transform.position = m_RB.transform.position + new Vector3(0, m_YVerhogen, 0);
        ToDrifting();
        OfTheWorld();
        if (GiveBoost)
        {
            Boost2_0();
        }

        //if (Input.GetKey(KeyCode.A))
        //{
        //    m_RightWheel.localRotation = Quaternion.Lerp(m_RightWheel.localRotation, Quaternion.Euler(new Vector3(0, -m_turnAmmount, 0)), 0.5f);
        //    m_LeftWheel.localRotation = Quaternion.Lerp(m_LeftWheel.localRotation, Quaternion.Euler(new Vector3(0, -m_turnAmmount, 0)), 0.5f);
        //}
        //else if (Input.GetKey(KeyCode.D))
        //{
        //    m_RightWheel.localRotation = Quaternion.Lerp(m_RightWheel.localRotation, Quaternion.Euler(new Vector3(0, m_turnAmmount, 0)), 0.5f);
        //    m_LeftWheel.localRotation = Quaternion.Lerp(m_LeftWheel.localRotation, Quaternion.Euler(new Vector3(0, m_turnAmmount, 0)), 0.5f);
        //}
        //else
        //{
        //    m_RightWheel.localRotation = Quaternion.Lerp(m_RightWheel.localRotation, Quaternion.Euler(new Vector3(0, 0, 0)), 0.5f);
        //    m_LeftWheel.localRotation = Quaternion.Lerp(m_LeftWheel.localRotation, Quaternion.Euler(new Vector3(0, 0, 0)), 0.5f);
        //}
    }

    private void SetOverData()
    {
        // laat de movment bepalen in wele status het zit
        if (OnGround && (!IsDrifting) && (!OnGrass))
        {
            m_TurnStrength = m_NormalPlayermovement.TuringSpeed;
            m_Speed = m_NormalPlayermovement.Speed;
            m_MaxSpeed = m_NormalPlayermovement.MaxSpeed;
            if (GiveBoost)
            {
                m_MaxSpeed += SetBoostSpeed;
            }
        }
        else if(OnGround && (!IsDrifting) && (OnGrass))
        {
            m_TurnStrength = m_GrassMovement.TuringSpeed;
            m_Speed = m_GrassMovement.Speed;
            m_MaxSpeed = m_GrassMovement.MaxSpeed;
        }
        else if (OnGround && (IsDrifting) && (!OnGrass))
        {
            m_TurnStrength = m_DriftMovement.TuringSpeed;
            m_Speed = m_DriftMovement.Speed;
            m_MaxSpeed = m_DriftMovement.MaxSpeed;
        }
        else if (OnGround && (IsDrifting) && (OnGrass))
        {
            m_TurnStrength = m_GrassDriftMovement.TuringSpeed;
            m_Speed = m_GrassDriftMovement.Speed;
            m_MaxSpeed = m_GrassDriftMovement.MaxSpeed;
        }
        else if (!OnGround && (!IsDrifting) && (!OnGrass))
        {
            m_TurnStrength = m_FlyingMovement.TuringSpeed;
            m_Speed = m_FlyingMovement.Speed;
            m_MaxSpeed = m_FlyingMovement.MaxSpeed;
        }
    }

    private void ToDrifting()
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
        OnGrass = false;
        RaycastHit hit;

        if (Physics.Raycast(BeginPointRay.position, -transform.up, out hit, RayRange, FloorLayer))
        {
            OnGround = true;
            transform.rotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
        }        
        else if (Physics.Raycast(BeginPointRay.position, -transform.up, out hit, RayRange, GrassLayer))
        {
            Debug.Log("Grass");
            OnGrass = true;
            OnGround = true;
            transform.rotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
        }
        //print(hit);
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
        if (m_timerboost <= m_BoostTime && !IsDrifting)
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
        //print(GiveBoost);
        //print(m_timerboost);
        //print(m_Speed);
        //print(m_RB.velocity.magnitude);
        //print(Lerpnummer);
        // print(m_timer);
        // print(m_RB.velocity.magnitude);
        print(OnGround);
    }
}
