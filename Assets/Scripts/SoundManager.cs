﻿using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    private int _sound;
    public Image image;
    public Sprite[] soundImage;
    public AudioClip[] audios;
    private AudioSource _aSource;
    
    void Awake()
    {
        _aSource = GetComponent<AudioSource>();
        _sound = PlayerPrefs.GetInt("Sound", 0);
        _aSource.mute = _sound == 1;
        if (SceneManager.GetActiveScene().name == "Main Menu")
        {
            image.sprite = soundImage[_sound];
        }
    }

    public void PlaySound(int index)
    {
        if (_sound == 0)
        {
            _aSource.PlayOneShot(audios[index]);
        }
    }
    
    public void Switch()
    {
        _sound = 1 - _sound;
        image.sprite = soundImage[_sound];
        _aSource.mute = !_aSource.mute;
    }

    private void OnDestroy()
    {
        PlayerPrefs.SetInt("Sound", _sound);
    }
}
