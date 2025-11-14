using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    public GameObject coinPrefab;
    public float spawnRate = 2f; // seconds between spawns
    public float xMin = -4f, xMax = 4f;
    public float yMin = -8f, yMax = 8f;

    void Start()
    {
        InvokeRepeating("SpawnCoin", 1f, spawnRate);
    }

    void SpawnCoin()
    {
        Vector3 spawnPos = new Vector3(Random.Range(xMin, xMax), Random.Range(yMin, yMax), 0f);
        Instantiate(coinPrefab, spawnPos, Quaternion.identity);
    }
}
