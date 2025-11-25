using UnityEngine;
using UnityEngine.SceneManagement;

public class PausaJuego : MonoBehaviour
{
    public static bool GamePaused = false;

    public GameObject panelPausa;

    public void Pausar()
    {
        GamePaused = !GamePaused;

        if (GamePaused)
        {
            panelPausa.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            panelPausa.SetActive(false);
            Time.timeScale = 1f;
        }
    }

    public void VolverAlMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("menuInicial");
    }
}
