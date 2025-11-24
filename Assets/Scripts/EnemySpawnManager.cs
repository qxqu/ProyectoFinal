using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;    // Prefab del enemigo
    public Transform[] spawnPoints;   // Array de tus 4 spawners
    public float spawnInterval = 3f;  // Cada cuántos segundos aparece un enemigo

    void Start()
    {
        InvokeRepeating(nameof(SpawnEnemy), 1f, spawnInterval);
    }

    void SpawnEnemy()
    {
        if (spawnPoints.Length == 0 || enemyPrefab == null)
        {
            Debug.LogWarning("Faltan spawners o prefab del enemigo.");
            return;
        }

        // Escoge un spawner aleatorio
        int randomIndex = Random.Range(0, spawnPoints.Length);
        Transform chosenSpawner = spawnPoints[randomIndex];

        // Instancia el enemigo en ese spawner
        Instantiate(enemyPrefab, chosenSpawner.position, Quaternion.identity);
    }
}
