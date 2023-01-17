using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class TestInputManger : MonoBehaviour
{
    [SerializeField]
    private List<PlayerInput> m_Players = new List<PlayerInput>();
    [SerializeField]
    private List<Transform> m_StartingPoint;
    [SerializeField]
    private List<LayerMask> m_PlayerLayer;
    [SerializeField] GameObject car1;
    private int getal = 0;

    private PlayerInputManager m_playerInputManger;
    // Start is called before the first frame update
    void Awake()
    {
        m_playerInputManger = FindObjectOfType<PlayerInputManager>();
        //m_playerInputManger.joinBehavior = PlayerJoinBehavior.JoinPlayersManually;
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 3)
        {
            m_playerInputManger.joinBehavior = PlayerJoinBehavior.JoinPlayersManually;
            Timer.singleton.StartTime();
        }
        else
        {
            m_playerInputManger.joinBehavior = PlayerJoinBehavior.JoinPlayersWhenButtonIsPressed;
        }


    }

    


    private void OnEnable()
    {
        m_playerInputManger.onPlayerJoined += AddPlayer;
        SceneManager.sceneLoaded += SceneControl;
    }

    private void OnDisable()
    {
        m_playerInputManger.onPlayerJoined -= AddPlayer;
    }

    public void SceneControl(Scene _scene , LoadSceneMode _loadSceneMode)
    {
        if(_scene.buildIndex == 3)
        {
          //  print(m_Players.Count);
            for (int i = 0; i < m_Players.Count; i++)
            {
               // print(m_Players.Count);
                SetUpCar(i);               
            }
            m_playerInputManger.splitScreen = true;
        }
    }

    public void AddPlayer(PlayerInput _player)
    {
       // Debug.Log(_player);
        m_Players.Add(_player);

        _player.transform.position = m_StartingPoint[m_Players.Count - 1].position;
    }

    private void SetUpCar(int _player)
    {
       // print(" ya");
        GameObject test = Instantiate(car1, m_Players[_player].transform.position, m_Players[_player].transform.rotation * Quaternion.Euler(0,180,0));
        CarMovment carMovement = m_Players[_player].GetComponent<CarMovment>();
        carMovement.car = test.transform;
        m_Players[_player].camera = test.GetComponentInChildren<Camera>();
        carMovement.Initialize();
        CarHelper _carhelper = test.GetComponent<CarHelper>();
        Instantiate(Gamemanger.SingGame.m_playerscars[_player], _carhelper.CarArttf.transform.position, _carhelper.CarArttf.transform.rotation, _carhelper.CarArttf);
        


    }
}
