using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{
    public GameObject[] powerUps; // Assign prefabs (SpeedBoost, Shield)
    public float spawnRate = 10f;
    public float xMin = -4f, xMax = 4f;
    public float yMin = -8f, yMax = 8f;

    void Start()
    {
        InvokeRepeating("SpawnPowerUp", 5f, spawnRate);
    }

    void SpawnPowerUp()
    {
        int index = Random.Range(0, powerUps.Length);
        Vector3 spawnPos = new Vector3(Random.Range(xMin, xMax), Random.Range(yMin, yMax), 0f);
        Instantiate(powerUps[index], spawnPos, Quaternion.identity);
    }
}
