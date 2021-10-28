using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SE : MonoBehaviour
{
    private AudioSource _audioSource;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlaySE(AudioClip _audioClip)
    {
        _audioSource.PlayOneShot(_audioClip);
    }
}
