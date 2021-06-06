using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    public int val, maxVal;
    public GameManager gameManager;
    private int _first, _second;
    private Transform _ques;
    
    private void Start()
    {
        _first = Random.Range(0, maxVal);
        _second = Random.Range(0, maxVal);
        val = _first + _second;
        _ques = transform.GetChild(1);
        _ques.GetComponent<TextMesh>().text = "  " + _first + "\n+ " + _second;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")){
            Destroy(other.gameObject);
            gameManager.GameOver();
        }
        else if (other.CompareTag("Ground"))
        {
            gameManager.IncreaseScore(1);
            Destroy(gameObject);
        }
    }
}
