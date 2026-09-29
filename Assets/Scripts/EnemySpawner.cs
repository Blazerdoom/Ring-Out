using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InvokeRepeating("spawnEnemy", 1f, 2f);
    }

    private void spawnEnemy()
    {
        Vector3 spot = new Vector3(Random.Range(-6f, 6f), 6, Random.Range(-6f, 6f));
        Instantiate(enemyPrefab, spot, enemyPrefab.transform.rotation);
    }
}
