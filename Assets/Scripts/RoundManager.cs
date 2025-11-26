using UnityEngine;
using TMPro;

public class RoundManager : MonoBehaviour
{
    public static RoundManager Instance { get; private set; }

    [Header("Round Settings")]
    public int enemiesPerRound = 10;
    public float difficultyIncreaseRate = 1.2f;
    public int maxRounds = 3;

    [Header("Current Round Info")]
    public int currentRound = 1;
    private int enemiesKilledThisRound = 0;
    private int totalEnemiesKilled = 0;

    [Header("UI References (TMP)")]
    public TMP_Text roundText;
    public TMP_Text killsText;

    [Header("Round Events")]
    public bool pauseSpawningBetweenRounds = true;
    public float delayBetweenRounds = 2f;

    private bool isRoundActive = true;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        UpdateUI();
    }

    // Llamado desde Bullet.cs cuando un enemigo muere
    public void OnEnemyKilled()
    {
        enemiesKilledThisRound++;
        totalEnemiesKilled++;

        UpdateUI();

        // ¿Terminó la ronda?
        if (enemiesKilledThisRound >= enemiesPerRound)
        {
            CompleteRound();
        }
    }

    private void CompleteRound()
    {
        Debug.Log($"Round {currentRound} completado!");

        // Pausar el spawn
        if (pauseSpawningBetweenRounds && EnemySpawnManager.Instance != null)
            EnemySpawnManager.Instance.PauseSpawning();

        isRoundActive = false;

        // Pasar a la siguiente ronda luego de un delay
        Invoke(nameof(StartNextRound), delayBetweenRounds);
    }

    private void StartNextRound()
    {
        currentRound++;

        // ❗ Si ya pasamos la última ronda → victoria
        if (currentRound > maxRounds)
        {
            Debug.Log("El jugador ganó el juego.");

            if (VictoryManager.Instance != null)
                VictoryManager.Instance.ShowVictory();
            else
                Debug.LogError("VictoryManager no está en la escena.");

            return;
        }

        Debug.Log($"Iniciando Round {currentRound}");

        enemiesKilledThisRound = 0;
        isRoundActive = true;

        // Aumentar dificultad del spawner
        if (EnemySpawnManager.Instance != null)
        {
            EnemySpawnManager.Instance.IncreaseDifficulty(difficultyIncreaseRate);
            EnemySpawnManager.Instance.ResumeSpawning();
        }

        UpdateUI();
    }

    private void UpdateUI()
    {
        if (roundText != null)
            roundText.text = $"Round {currentRound}/{maxRounds}";

        if (killsText != null)
            killsText.text = $"Kills: {enemiesKilledThisRound}/{enemiesPerRound}";
    }

    public bool IsRoundActive()
    {
        return isRoundActive;
    }

    public int GetEnemiesRemainingInRound()
    {
        return enemiesPerRound - enemiesKilledThisRound;
    }

    public int GetTotalKills()
    {
        return totalEnemiesKilled;
    }
}
