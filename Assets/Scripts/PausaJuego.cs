using UnityEngine;
using UnityEngine.SceneManagement;

public class PausaJuego : MonoBehaviour
{
    public GameObject panelPausa;
    private bool juegoPausado = false;

    public void Pausar()
    {
        juegoPausado = !juegoPausado;

        if (juegoPausado)
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
        SceneManager.LoadScene("menuInicial"); // Cambia el nombre de la escena si es distinto
    }
}
