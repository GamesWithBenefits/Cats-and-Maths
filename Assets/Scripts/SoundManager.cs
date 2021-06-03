using UnityEngine;
using UnityEngine.SceneManagement;
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
        _sound = PlayerPrefs.GetInt("Sound", 0);
        Instance._aSource.mute = Instance._sound == 1;
        if (SceneManager.GetActiveScene().name == "Main Menu")
        {
            image.sprite = soundImage[_sound];
        }
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
        Debug.Log(image.color);
        image.sprite = soundImage[_sound];
        Debug.Log(image.color);
        _aSource.mute = !_aSource.mute;
    }

    private void OnDestroy()
    {
        PlayerPrefs.SetInt("Sound", _sound);
    }
}
