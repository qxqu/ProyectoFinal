using UnityEngine;
using UnityEngine.SceneManagement; // Importante para cambiar escenas

public class menuproyecto1 : MonoBehaviour
{
    // Este método se ejecutará cuando hagas clic en el botón "START GAME"
    public void StartGame()
    {
        // Carga la escena llamada "SampleScene"
        SceneManager.LoadScene("ElegirMundo");
    }

    // Si quieres, puedes agregar más funciones para los otros botones:
    public void ExitGame()
    {
        Debug.Log("Saliendo del juego...");
        Application.Quit();
    }

    public void OpenOptions()
    {
        Debug.Log("Abriendo menú de opciones...");
        // Aquí puedes abrir un panel de opciones o cargar otra escena
    }
}
