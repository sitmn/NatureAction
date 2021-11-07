using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NatureBGM : MonoBehaviour
{
    private AudioSource _audioSource;

    [SerializeField]
    private AudioClip _normalBGM;
    [SerializeField]
    private AudioClip _moveBGM;
    [SerializeField]
    private AudioClip _skillBGM;

    
    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlayBGM(CanvasGroup _canvasGroup)
    {
        if (_canvasGroup == null || _canvasGroup.gameObject.name == "HomeScreen" || _canvasGroup.gameObject.name == "TrainingScreen")
        {
            if(_audioSource.clip != _normalBGM)
            {
                _audioSource.clip = _normalBGM;
                _audioSource.Play();
            } 
        }
        else if (_canvasGroup.gameObject.name == "MoveScreen")
        {
            if (_audioSource.clip != _moveBGM)
            {
                _audioSource.clip = _moveBGM;
                _audioSource.Play();
            }
        }
        else if (_canvasGroup.gameObject.name == "SkillScreen")
        {
            if (_audioSource.clip != _skillBGM)
            {
                _audioSource.clip = _skillBGM;
                _audioSource.Play();
            }
        }
    }
}
