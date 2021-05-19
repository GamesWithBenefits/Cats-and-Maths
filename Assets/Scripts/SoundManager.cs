using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    private int _sound;
    public Image image;
    public Sprite[] soundImage;
    public AudioClip[] audios;
    private AudioSource _aSource;
    public static SoundManager Instance;
    
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); return;
        }
        _aSource = GetComponent<AudioSource>();
        _sound = PlayerPrefs.GetInt("Sound");
        Instance._aSource.mute = Instance._sound == 1;
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

    private void OnDestroy()
    {
        PlayerPrefs.SetInt("Sound", _sound);
    }
}
