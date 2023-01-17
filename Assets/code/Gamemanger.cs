using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Gamemanger : MonoBehaviour
{
    public static Gamemanger SingGame;

   // private List<PlayerInput> m_Players = new List<PlayerInput>();
    public List<GameObject> CheckPointsArray;
    public GameObject Finsh;
    private bool m_TwoPlayer = false;
    public List<GameObject> m_playerscars = new List<GameObject>();
    public GameObject m_PlayerOneCar;
    public GameObject m_PlayerTwoCar;
    public int checkpointschecker = 0;
    private bool ingame = false;

   
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

    public void setCheckpoints()
    {
        ChechPoitnsmanger chech = FindObjectOfType<ChechPoitnsmanger>();
        CheckPointsArray = chech.CheckPoints;
        Finsh = chech.Finish;
        Finsh.SetActive(false);
        ingame = true;
    }

    public void Twoplayers()
    {
        m_TwoPlayer = true;
        NextScene();
    }

    // Update is called once per frame
    void Update()
    {
        if(checkpointschecker == CheckPointsArray.Count && (ingame))
        {
            Finsh.SetActive(true);
        }
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

    public void win()
    {
        Timer.singleton.EndTime();
        Time.timeScale = 0;
        StartCoroutine(WinWait());
    }

    private IEnumerator WinWait()
    {
        yield return new WaitForSeconds(7);
        SceneManager.LoadScene(0);
    }
}
