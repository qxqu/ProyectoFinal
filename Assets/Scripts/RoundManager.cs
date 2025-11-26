using UnityEngine;
using TMPro;
using System.Collections.Generic; // para Queue<T>

public class RoundManager : MonoBehaviour
{
    public static RoundManager Instance;

    [Header("Round Settings")]
    public int enemiesPerRound = 10;       // Valor base usado si no hay cola
    public int totalRounds = 3;
    public float difficultyIncreaseRate = 1.2f;

    // 🔹 NUEVA ESTRUCTURA DE DATOS
    private Queue<int> enemiesQueue = new Queue<int>(); 

    [Header("Current Round Info")]
    public int currentRound = 1;
    private int enemiesKilledThisRound = 0;
    private int requiredEnemiesThisRound; // valor extraído de la cola

    [Header("UI")]
    public TextMeshProUGUI roundText;
    public TextMeshProUGUI killsText;

    [Header("Events")]
    public bool pauseSpawningBetweenRounds = true;
    public float delayBetweenRounds = 3f;

    [Header("Panels")]
    public GameObject victoryPanel;

    private bool isRoundActive = true;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        if (victoryPanel != null)
            victoryPanel.SetActive(false);
    }

    private void Start()
    {
        BuildRoundQueue();

        requiredEnemiesThisRound = enemiesQueue.Dequeue(); // SE USA LA COLA
        UpdateUI();
    }

   
    private void BuildRoundQueue()
    {
        
        for (int i = 1; i <= totalRounds; i++)
        {
            int enemies = Mathf.RoundToInt(enemiesPerRound * Mathf.Pow(difficultyIncreaseRate, i - 1));
            enemiesQueue.Enqueue(enemies);
        }
    }

   
    public void OnEnemyKilled()
    {
        enemiesKilledThisRound++;

        UpdateUI();

        if (enemiesKilledThisRound >= requiredEnemiesThisRound)
        {
            CompleteRound();
        }
    }

    private void CompleteRound()
    {
        Debug.Log($"Round {currentRound} completada.");

        if (currentRound >= totalRounds)
        {
            Debug.Log("Juego completado. Mostrando pregunta final...");
            QuestionManager.Instance.ShowRandomQuestion();
            return;
        }

        if (pauseSpawningBetweenRounds)
        {
            isRoundActive = false;
            if (EnemySpawnManager.Instance != null)
                EnemySpawnManager.Instance.PauseSpawning();
        }

        Invoke(nameof(StartNextRound), delayBetweenRounds);
    }

    private void StartNextRound()
    {
        currentRound++;
        enemiesKilledThisRound = 0;
        isRoundActive = true;

        requiredEnemiesThisRound = enemiesQueue.Dequeue(); // SIGUIENTE VALOR DE LA COLA

        Debug.Log($"Iniciando ronda {currentRound}...");

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
            roundText.text = $"Round {currentRound}/{totalRounds}";

        if (killsText != null)
            killsText.text = $"Kills: {enemiesKilledThisRound}/{requiredEnemiesThisRound}";
    }

    public bool IsRoundActive()
    {
        return isRoundActive;
    }

    public void ShowFinalVictory()
    {
        Debug.Log("Jugador ha ganado con éxito. Mostrando panel final...");

        if (victoryPanel != null)
            victoryPanel.SetActive(true);

        Time.timeScale = 0f;
    }
}
