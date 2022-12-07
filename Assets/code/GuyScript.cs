using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuyScript : MonoBehaviour
{
    [SerializeField] private GameObject emotionObject;
    [SerializeField] private GameObject playerObject;
    [SerializeField] private GameObject turnObject;
    [SerializeField] private float guySpeed;
    [SerializeField] private bool walkin;
    private bool canBoost;

    void Start()
    {
        playerObject = GameObject.FindGameObjectWithTag("Player");
        emotionObject = GameObject.FindGameObjectWithTag("EmotionBalk");
        canBoost = true;
        EmotionCheck();
    }

    private void Update()
    {
        if (walkin == true)
        {
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, playerObject.transform.position, guySpeed);
        }

        // https://answers.unity.com/questions/161053/making-an-object-rotate-to-face-another-object.html
        int damping = 2;

        var lookPos = playerObject.transform.position - transform.position;
        lookPos.y = 0;
        var rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * damping);

    }

    private void OnTriggerExit(Collider other)
    {
        if (canBoost == true && other.CompareTag("Player"))
        {
            //StartCoroutine(playerObject.GetComponent<CarMovment>().Boost(100));
            emotionObject.GetComponent<EmotionManager>().ChangeEmotion(25);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            canBoost = false;
            //StartCoroutine(playerObject.GetComponent<CarMovment>().Boost(-100));
            emotionObject.GetComponent<EmotionManager>().ChangeEmotion(-25);
            gameObject.SetActive(false);
        }
    }

    public void EmotionCheck()
    {
        if (emotionObject.GetComponent<EmotionManager>().emotion <= -50)
        {
            walkin = true;
            turnObject.transform.rotation = new Quaternion(0, transform.rotation.y + 180, 0, 0);
        }
        else
        {
            walkin = false;
            turnObject.transform.rotation = new Quaternion(0, 180, 0, 0);
        }
    }
}
