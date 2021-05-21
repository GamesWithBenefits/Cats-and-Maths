using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public Text scoreText;
    public GameObject attemptBox, gameOverPanel, pausePanel, spawner;
    public List<GameObject> enemies;
    public SoundManager soundManager;

    private bool _gameOver;
    public bool paused;
    private int _score, _coins;
    private Enemy _temp;
    private EnemySpawn _enemy;
    private Text _attemptText;
    private Animator _attemptAnim;
    private void Awake()
    {
        _enemy = spawner.GetComponent<EnemySpawn>();
        _attemptText = attemptBox.transform.GetComponentInChildren<Text>();
        _attemptAnim = attemptBox.GetComponent<Animator>();
        if (Instance == null)
        {
            Instance = this;
        }
    }
    
    public void GameOver()
    {
        IncreaseCoins();
        _gameOver = true;
        gameOverPanel.SetActive(true);
        gameOverPanel.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = _score.ToString();
        gameOverPanel.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<Text>().text = _coins.ToString();
        Destroy(spawner);
        AdsManager.Instance.ShowAds(1);
        AdsManager.Instance.ShowAds(2);
    }

    public void IncreaseCoins()
    {
        PlayerPrefs.SetInt("coins", PlayerPrefs.GetInt("coins") + _coins);
    }

    public void Input(int ip)
    {
        _attemptText.text += ip.ToString();
    }

    public void RemoveLast()
    {
        _attemptText.text = _attemptText.text.Remove(_attemptText.text.Length - 1);
    }

    public async void CheckAns()
    {
        bool correct = false;
        if (_attemptText.text != "")
        {
            int ans = int.Parse(_attemptText.text), i = 0, len = enemies.Count;
            while (i < len)
            {
                if (enemies[i] == null)
                {
                    enemies.RemoveAt(i);
                    len -= 1;
                    continue;
                }

                _temp = enemies[i].GetComponent<Enemy>();
                if (_temp.val == ans)
                {
                    IncreaseScore(_temp.val);
                    len -= 1;
                    Destroy(enemies[i]);
                    enemies.RemoveAt(i);
                    _coins += 1;
                    correct = true;
                }
                else
                {
                    i += 1;
                }
            }
        }

        if (!correct)
        {
            _attemptAnim.SetBool(-1037297297, true);
            soundManager.PlaySound(0);
        }
        else
        {
            soundManager.PlaySound(1);
            if (_coins % 10 == 0)
            {
                _enemy.maxVal *= 10;
                _enemy.spawnRate = 3;
            }
        }
        _attemptText.text = "";
        await Task.Delay(200);
        _attemptAnim.SetBool(-1037297297, false);
    }

    public void IncreaseScore(int delta)
    {
        if (_gameOver) return;
        _score += delta;
        scoreText.text = _score.ToString();
    }

    public void MainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void Replay()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }

    public async void Pause()
    {
        Instance.paused = true;
        pausePanel.transform.GetChild(0).GetComponent<Text>().text = _score.ToString();
        pausePanel.transform.GetChild(1).GetComponent<Button>().interactable = false;
        pausePanel.transform.GetChild(2).GetComponent<Button>().interactable = false;
        pausePanel.transform.GetChild(3).GetComponent<Button>().interactable = false;
        pausePanel.SetActive(true);
        Time.timeScale = 0;
        AdsManager.Instance.InterstitialAd();
        while (Instance.paused)
        {
            await Task.Delay(500);
            Time.timeScale = 0;
        }
        pausePanel.transform.GetChild(1).GetComponent<Button>().interactable = true;
        pausePanel.transform.GetChild(2).GetComponent<Button>().interactable = true;
        pausePanel.transform.GetChild(3).GetComponent<Button>().interactable = true;
    }

    public async void Resume()
    {
        Time.timeScale = 1;
        AdsManager.Instance.HideAds(0);
        pausePanel.GetComponent<Animator>().SetBool(-2085996487, true);
        await Task.Delay(500);
        pausePanel.SetActive(false);
    }
}
