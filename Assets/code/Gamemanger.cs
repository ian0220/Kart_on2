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
    }
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NextScene()
    {
         SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
