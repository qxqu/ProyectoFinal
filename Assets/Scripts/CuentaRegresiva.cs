using System.Collections;
using UnityEngine;
using TMPro;

public class CuentaRegresiva : MonoBehaviour
{
    public GameObject panelCuenta;        // Panel que contiene el texto
    public TextMeshProUGUI textoCuenta;   // Texto de la cuenta regresiva
    public float tiempoEntreNumeros = 1f; // Tiempo entre cada número

    private void Start()
    {
        // Pausa el juego mientras hace la cuenta
        Time.timeScale = 0f;
        StartCoroutine(IniciarCuenta());
    }

    IEnumerator IniciarCuenta()
    {
        int contador = 3;
        textoCuenta.color = Color.white; // color fijo

        while (contador > 0)
        {
            textoCuenta.text = contador.ToString();
            yield return new WaitForSecondsRealtime(tiempoEntreNumeros);
            contador--;
        }

        textoCuenta.text = "¡GO!";
        yield return new WaitForSecondsRealtime(0.7f);

        // Quita el panel y reanuda el juego
        panelCuenta.SetActive(false);
        Time.timeScale = 1f;
    }
}
