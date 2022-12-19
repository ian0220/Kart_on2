using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip audioClip;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
            PlayAudioSound();
    }

    public void PlayAudioSound()
    {
        audioSource.PlayOneShot(audioClip);
    }
}
