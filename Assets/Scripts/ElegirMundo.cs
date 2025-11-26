using UnityEngine;
using UnityEngine.SceneManagement;

public class ElegirMundo : MonoBehaviour
{
    public void JugarMundo()
    {
        SceneManager.LoadScene("Mundo1"); 
    }

    public void VolverAlMenu()
    {
        SceneManager.LoadScene("menuInicial");
    }

    public void Mundo2()
    {
        SceneManager.LoadScene("Mundo2");
    }
}
