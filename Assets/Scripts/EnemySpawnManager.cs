using UnityEngine;
using System.Collections.Generic;

public class EnemySpawnManager : MonoBehaviour
{
    public static EnemySpawnManager Instance { get; private set; }

    public GameObject enemyPrefab;           
    public List<Transform> spawnPoints;     
    public float spawnInterval = 3f;

    private float currentSpawnInterval;
    private bool isSpawning = true;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        currentSpawnInterval = spawnInterval;
        InvokeRepeating(nameof(SpawnEnemy), 1f, currentSpawnInterval);
    }

    void SpawnEnemy()
    {
        if (!isSpawning)
            return;

        if (spawnPoints.Count == 0 || enemyPrefab == null)
        {
            Debug.LogWarning("Faltan spawners o prefab del enemigo.");
            return;
        }

        int randomIndex = Random.Range(0, spawnPoints.Count);
        Transform chosenSpawner = spawnPoints[randomIndex];

        GameObject enemy = Instantiate(enemyPrefab, chosenSpawner.position, Quaternion.identity);

        
        FlyingEnemy flyingEnemy = enemy.GetComponent<FlyingEnemy>();
        if (flyingEnemy != null && flyingEnemy.player == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                flyingEnemy.player = player.transform;
            }
        }
    }

    public void PauseSpawning()
    {
        isSpawning = false;
        Debug.Log("Enemy spawning paused");
    }

    public void ResumeSpawning()
    {
        isSpawning = true;
        Debug.Log("Enemy spawning resumed");
    }

    public void IncreaseDifficulty(float multiplier)
    {
        currentSpawnInterval = Mathf.Max(0.5f, currentSpawnInterval / multiplier);

        CancelInvoke(nameof(SpawnEnemy));
        InvokeRepeating(nameof(SpawnEnemy), 0.5f, currentSpawnInterval);

        Debug.Log($"Difficulty increased! New spawn interval: {currentSpawnInterval}s");
    }
}
