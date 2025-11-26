using UnityEngine;
using TMPro;

public class RoundManager : MonoBehaviour
{
    public static RoundManager Instance;

    [Header("Round Settings")]
    public int enemiesPerRound = 10;       // Enemigos necesarios por ronda
    public int totalRounds = 3;            // Cantidad de rondas del juego
    public float difficultyIncreaseRate = 1.2f;

    [Header("Current Round Info")]
    public int currentRound = 1;
    private int enemiesKilledThisRound = 0;

    [Header("UI")]
    public TextMeshProUGUI roundText; 
    public TextMeshProUGUI killsText;

    [Header("Events")]
    public bool pauseSpawningBetweenRounds = true;
    public float delayBetweenRounds = 3f;

    [Header("Panels")]
    public GameObject victoryPanel;  // Panel final de victoria

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
            victoryPanel.SetActive(false); // asegurarse que está oculto
    }

    private void Start()
    {
        UpdateUI();
    }

    // Llamado por Enemy cuando muere
    public void OnEnemyKilled()
    {
        enemiesKilledThisRound++;

        UpdateUI();

        if (enemiesKilledThisRound >= enemiesPerRound)
        {
            CompleteRound();
        }
    }

    private void CompleteRound()
    {
        Debug.Log($"Round {currentRound} completada.");

        // Si ya es la última ronda → PREGUNTA
        if (currentRound >= totalRounds)
        {
            Debug.Log("Juego completado. Mostrando pregunta final...");
            QuestionManager.Instance.ShowRandomQuestion();
            return;
        }

        // Si NO es la última ronda → continuar normal
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

        Debug.Log($"Iniciando ronda {currentRound}...");

        // Aumentar dificultad
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
            killsText.text = $"Kills: {enemiesKilledThisRound}/{enemiesPerRound}";
    }

    public bool IsRoundActive()
    {
        return isRoundActive;
    }

    // Llamado desde QuestionManager SI RESPONDE BIEN
    public void ShowFinalVictory()
    {
        Debug.Log("Jugador ha ganado con éxito. Mostrando panel final...");

        if (victoryPanel != null)
            victoryPanel.SetActive(true);

        Time.timeScale = 0f; // Pausar el juego para mostrar victoria
    }
}
