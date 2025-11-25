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

    [Header("UI (TextMeshPro)")]
    public TMP_Text roundText;      // Ej: "Round 1/3"
    public TMP_Text killsText;      // Ej: "Kills: 0/10"

    [Header("Round Events")]
    public bool pauseSpawningBetweenRounds = true;
    public float delayBetweenRounds = 3f;

    private bool isRoundActive = true;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void Start()
    {
        UpdateUI();
    }

    // Llamado cuando un enemigo muere
    public void OnEnemyKilled()
    {
        enemiesKilledThisRound++;
        totalEnemiesKilled++;

        Debug.Log($"Enemy killed! {enemiesKilledThisRound}/{enemiesPerRound}");

        UpdateUI();

        // Si la ronda está completa
        if (enemiesKilledThisRound >= enemiesPerRound)
        {
            CompleteRound();
        }
    }

    void CompleteRound()
    {
        Debug.Log($"Round {currentRound} completed!");

        if (pauseSpawningBetweenRounds)
        {
            isRoundActive = false;

            if (EnemySpawnManager.Instance != null)
                EnemySpawnManager.Instance.PauseSpawning();
        }

        Invoke(nameof(StartNextRound), delayBetweenRounds);
    }

    void StartNextRound()
    {
        currentRound++;
        enemiesKilledThisRound = 0;
        isRoundActive = true;

        Debug.Log($"Starting Round {currentRound}!");

        if (EnemySpawnManager.Instance != null)
        {
            EnemySpawnManager.Instance.IncreaseDifficulty(difficultyIncreaseRate);
            EnemySpawnManager.Instance.ResumeSpawning();
        }

        UpdateUI();
    }

    void UpdateUI()
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
