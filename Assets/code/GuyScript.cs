using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuyScript : MonoBehaviour
{
    [SerializeField] private GameObject emotionObject;
    [SerializeField] private GameObject playerObject;
    [SerializeField] private float guySpeed;
    [SerializeField] private bool walkin;
    private bool canBoost;

    void Start()
    {
        playerObject = GameObject.FindGameObjectWithTag("Player");
        emotionObject = GameObject.FindGameObjectWithTag("EmotionBalk");
        canBoost = true;
    }

    private void Update()
    {
        if (walkin == true)
        {
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, playerObject.transform.position, guySpeed);
        }

        if (emotionObject.GetComponent<EmotionManager>().emotion <= -50)
        {
            walkin = true;
            transform.rotation = new Quaternion(0, 180, 0, 0);
        }
        else
        {
            walkin = false;
            transform.rotation = new Quaternion(0, 0, 0, 0);
        }

        transform.LookAt(playerObject.transform.position);

    }

    private void OnTriggerExit(Collider other)
    {
        if (canBoost == true && other.CompareTag("Player"))
        {
            StartCoroutine(playerObject.GetComponent<CarMovment>().Boost(100));
            emotionObject.GetComponent<EmotionManager>().ChangeEmotion(25);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            canBoost = false;
            StartCoroutine(playerObject.GetComponent<CarMovment>().Boost(-100));
            emotionObject.GetComponent<EmotionManager>().ChangeEmotion(-25);
            gameObject.SetActive(false);
        }
    }
}
