using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Gamemanger : MonoBehaviour
{
    public static Gamemanger SingGame;

   // private List<PlayerInput> m_Players = new List<PlayerInput>();
    public GameObject[] CheckPointsArray;
    public GameObject Finsh;
    private bool m_TwoPlayer = false;
    public List<GameObject> m_playerscars = new List<GameObject>();
    public GameObject m_PlayerOneCar;
    public GameObject m_PlayerTwoCar;

    //[Header("inputmanger")]
    //private List<PlayerInput> m_Players = new List<PlayerInput>();
    //[SerializeField]
    //private List<Transform> m_StartingPoint;
    //[SerializeField]                      
    //private List<LayerMask> m_PlayerLayer;

    //private PlayerInputManager m_playerInputManger;

    //[SerializeField]
    // private speherecollider m_ColliderScript;
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        if(SingGame == null)
        {
            SingGame = this;
        }
        else
        {
            Destroy(this);
        }

        //m_playerInputManger = FindObjectOfType<PlayerInputManager>();
    }
    void Start()
    {

    }

    public void Twoplayers()
    {
        m_TwoPlayer = true;
        NextScene();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayerSelection(GameObject _car)
    { 
            m_playerscars.Add(_car);
        


        if((m_TwoPlayer == true && (m_playerscars.Count == 2)) || (m_TwoPlayer == false && (m_playerscars.Count == 1)))
        {
            NextScene();
        }
        
    }

    public void NextScene()
    {
         SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    //private void OnEnable()
    //{
    //    m_playerInputManger.onPlayerJoined += AddPlayer;
    //}

    //private void OnDisable()
    //{
    //    m_playerInputManger.onPlayerJoined -= AddPlayer;
    //}

    //public void AddPlayer(PlayerInput _player)
    //{
    //    m_Players.Add(_player);

    //    _player.transform.position = m_StartingPoint[m_Players.Count - 1].position;
    //}
}
