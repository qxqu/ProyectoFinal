using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    public static EnemySpawnManager Instance { get; private set; }

    public GameObject enemyPrefab;    // Prefab del enemigo
    public Transform[] spawnPoints;   // Array de tus 4 spawners
    public float spawnInterval = 3f;  // Cada cuántos segundos aparece un enemigo

    private float currentSpawnInterval;
    private bool isSpawning = true;

    void Awake()
    {
        // Singleton pattern
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
        // No spawnear si está pausado
        if (!isSpawning)
            return;

        if (spawnPoints.Length == 0 || enemyPrefab == null)
        {
            Debug.LogWarning("Faltan spawners o prefab del enemigo.");
            return;
        }

        // Escoge un spawner aleatorio
        int randomIndex = Random.Range(0, spawnPoints.Length);
        Transform chosenSpawner = spawnPoints[randomIndex];

        // Instancia el enemigo en ese spawner
        GameObject enemy = Instantiate(enemyPrefab, chosenSpawner.position, Quaternion.identity);

        // Asignar player reference si el enemigo lo necesita
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

    // Pausar spawning entre rondas
    public void PauseSpawning()
    {
        isSpawning = false;
        Debug.Log("Enemy spawning paused");
    }

    // Reanudar spawning
    public void ResumeSpawning()
    {
        isSpawning = true;
        Debug.Log("Enemy spawning resumed");
    }

    // Aumentar dificultad (spawn más rápido)
    public void IncreaseDifficulty(float multiplier)
    {
        // Reducir el intervalo de spawn (spawn más rápido)
        currentSpawnInterval = Mathf.Max(0.5f, currentSpawnInterval / multiplier);

        // Reiniciar el InvokeRepeating con el nuevo intervalo
        CancelInvoke(nameof(SpawnEnemy));
        InvokeRepeating(nameof(SpawnEnemy), 0.5f, currentSpawnInterval);

        Debug.Log($"Difficulty increased! New spawn interval: {currentSpawnInterval}s");
    }
}
