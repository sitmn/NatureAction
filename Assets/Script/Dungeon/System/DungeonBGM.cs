using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonBGM : MonoBehaviour
{
    private AudioSource _audioSource;

    [SerializeField]
    private AudioClip _BGMClip;


    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        PlayBGM();
    }

    public void PlayBGM()
    {
        _audioSource.clip = _BGMClip;
        _audioSource.Play();
    }
}
