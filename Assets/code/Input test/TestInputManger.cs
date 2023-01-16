using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class TestInputManger : MonoBehaviour
{
    private List<PlayerInput> m_Players = new List<PlayerInput>();
    [SerializeField]
    private List<Transform> m_StartingPoint;
    [SerializeField]
    private List<LayerMask> m_PlayerLayer;

    private PlayerInputManager m_playerInputManger;
    // Start is called before the first frame update
    void Awake()
    {
        m_playerInputManger = FindObjectOfType<PlayerInputManager>();
    //    m_playerInputManger.joinBehavior = PlayerJoinBehavior.JoinPlayersManually;
    }

    // Update is called once per frame
    void Update()
    {
        //if(SceneManager.GetActiveScene().buildIndex == 2)
        //{
        //    m_playerInputManger.joinBehavior = PlayerJoinBehavior.JoinPlayersWhenButtonIsPressed;
        //}
        //else
        //{
        //    m_playerInputManger.joinBehavior = PlayerJoinBehavior.JoinPlayersManually;
        //}
    }
    
    public void SpawnCar()
    {
        
    }

    private void OnEnable()
    {
        m_playerInputManger.onPlayerJoined += AddPlayer;
    }

    private void OnDisable()
    {
        m_playerInputManger.onPlayerJoined -= AddPlayer;
    }

    public void AddPlayer(PlayerInput _player)
    {
        m_Players.Add(_player);

        _player.transform.position = m_StartingPoint[m_Players.Count - 1].position;
    }
}
