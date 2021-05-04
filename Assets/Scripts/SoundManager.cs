using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    private int _sound;
    public Image image;
    public Sprite[] soundImage;
    public AudioClip[] audios;
    private static AudioSource _aSource;
    
    void Awake()
    {
        _aSource = GetComponent<AudioSource>();
        _sound = PlayerPrefs.GetInt("Sound");
    }

    public void PlaySound(int index)
    {
        if (_sound == 1)
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
}
