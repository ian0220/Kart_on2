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
        ForwardMovement();

        

    }

    private void ForwardMovement()
    {
        Debug.Log(m_Bal.velocity.magnitude);
        if (m_Bal.velocity.magnitude < m_NormalPlayermovment.MaxSpeed)
        {
            m_Bal.AddForce(transform.forward * m_NormalPlayermovment.Speed * Time.fixedDeltaTime * 10000f);
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
