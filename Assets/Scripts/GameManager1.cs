using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager1 : MonoBehaviour
{
    public Text coinNum;

    private void Start()
    {
        coinNum.text = PlayerPrefs.GetInt("coins").ToString();
    }

    public void Play()
    {
        SceneManager.LoadScene(1);
    }
}
