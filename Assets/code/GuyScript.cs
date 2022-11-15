using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuyScript : MonoBehaviour
{
    [SerializeField] private GameObject emotionObject;
    private bool canBoost;

    void Start()
    {
        canBoost = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (canBoost == true && other.CompareTag("Player"))
        {
            Debug.Log("Boost!"); // Add player speed boost here
            emotionObject.GetComponent<EmotionManager>().ChangeEmotion(25);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            canBoost = false;
            Debug.Log("Slow!"); // Add player slow here
            emotionObject.GetComponent<EmotionManager>().ChangeEmotion(-25);
            gameObject.SetActive(false);
        }
    }
}
