using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // A reference to the enemy prefab you want to spawn.
    public GameObject enemyPrefab;

    // The rate at which enemies will spawn (in seconds).
    public float spawnRate = 2.0f;

    // The initial delay before the first enemy spawns.
    public float spawnDelay = 3.0f;

    // The area where enemies will spawn. You can either use a single transform
    // as the center of a circular area or an array of specific spawn points.
    public Transform[] spawnPoints;

    // The minimum and maximum number of enemies to spawn.
    public int minSpawnCount = 1;
    public int maxSpawnCount = 5;

    private void Start()
    {
        // Start the coroutine to handle enemy spawning.
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        // Wait for the initial delay before the first enemy spawns.
        yield return new WaitForSeconds(spawnDelay);

        // The main loop for the spawner.
        while (true)
        {
            // If there are spawn points assigned, spawn enemies.
            if (spawnPoints.Length > 0)
            {
                // Determine a random number of enemies to spawn within the set range.
                int numberOfEnemiesToSpawn = Random.Range(minSpawnCount, maxSpawnCount + 1);

                // Spawn the determined number of enemies.
                for (int i = 0; i < numberOfEnemiesToSpawn; i++)
                {
                    // Get a random spawn point from the array.
                    Transform randomSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

                    // Instantiate the enemy at the chosen spawn point's position and rotation.
                    Instantiate(enemyPrefab, randomSpawnPoint.position, randomSpawnPoint.rotation);
                }
            }
            // Wait for the next spawn cycle.
            yield return new WaitForSeconds(spawnRate);
        }
    }
}