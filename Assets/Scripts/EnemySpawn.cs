using System.Collections;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject enemy;
    public int maxVal = 10;
    public float xLim, spawnRate;
    void Start()
    {
        StartCoroutine(Spawner());
    }
    
    GameObject SpawnEnemy()
    {
        GameObject temp = Instantiate(enemy,
            new Vector2(Random.Range(-xLim, xLim), transform.position.y), Quaternion.identity);
        temp.GetComponent<Rigidbody2D>().gravityScale = 0.025f;
        temp.GetComponent<Enemy>().maxVal = maxVal;
        return temp;
    }

    IEnumerator Spawner()
    {
        while (true)
        {
            gameManager.enemies.Add(SpawnEnemy());
            spawnRate -= 0.1f;
            yield return new WaitForSeconds(spawnRate);
        }
    }
}
