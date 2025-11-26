using UnityEngine;
using UnityEngine.SceneManagement; 

public class menuproyecto1 : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("ElegirMundo");
    }

    public void ExitGame()
    {
        Debug.Log("Saliendo del juego...");
        Application.Quit();
    }

    public void OpenOptions()
    {
        Debug.Log("Abriendo menú de opciones...");
    }
}
