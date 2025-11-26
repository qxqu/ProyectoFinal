using System.Collections;
using UnityEngine;
using TMPro;

public class CuentaRegresiva : MonoBehaviour
{
    public GameObject panelCuenta;    
    public TextMeshProUGUI textoCuenta;   
    public float tiempoEntreNumeros = 1f; 

    private void Start()
    {
        Time.timeScale = 0f;
        StartCoroutine(IniciarCuenta());
    }

    IEnumerator IniciarCuenta()
    {
        int contador = 3;
        textoCuenta.color = Color.white; 

        while (contador > 0)
        {
            textoCuenta.text = contador.ToString();
            yield return new WaitForSecondsRealtime(tiempoEntreNumeros);
            contador--;
        }

        textoCuenta.text = "¡GO!";
        yield return new WaitForSecondsRealtime(0.7f);

        panelCuenta.SetActive(false);
        Time.timeScale = 1f;
    }
}
