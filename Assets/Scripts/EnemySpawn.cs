using System.Collections;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
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
        
        temp.GetComponent<Enemy>().maxVal = maxVal;
        return temp;
    }

    IEnumerator Spawner()
    {
        while (true)
        {
            GameManager.Instance.enemies.Add(SpawnEnemy());
            spawnRate -= 0.1f;
            yield return new WaitForSeconds(spawnRate);
        }
    }
}
