using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioClip[] audios;
    private static AudioSource _aSource;
    void Awake()
    {
        _aSource = GetComponent<AudioSource>();
    }

    public void PlaySound(int index)
    {
        _aSource.PlayOneShot(audios[index]);
    }
}
