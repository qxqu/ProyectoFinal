using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public static GameOverManager Instance { get; private set; }

    public GameObject gameOverPanel;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        gameOverPanel.SetActive(false);
    }

    public void ShowGameOver()
    {
        Time.timeScale = 0f; // Congelar el juego
        gameOverPanel.SetActive(true);
    }

    public void Retry()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MenuInicial");
    }
}
