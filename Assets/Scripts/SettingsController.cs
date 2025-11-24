using UnityEngine;

public class SettingsController : MonoBehaviour
{
    [SerializeField] private GameObject settingsPanel; // Panel de Configuración
    [SerializeField] private GameObject skinsPanel;    // Panel de Skins

    private bool isOpen = false;

    private void Start()
    {
        // Busca automáticamente si no están asignados
        if (settingsPanel == null)
        {
            settingsPanel = GameObject.Find("SettingsPanel");
            if (settingsPanel == null)
                Debug.LogError("❌ SettingsPanel no está asignado en el Inspector y no se encontró en la escena.");
        }

        if (skinsPanel == null)
        {
            skinsPanel = GameObject.Find("PanelSkins");
            if (skinsPanel == null)
                Debug.LogWarning("⚠️ PanelSkins no está asignado en el Inspector ni encontrado en la escena.");
        }

        // Ambos empiezan ocultos
        if (settingsPanel != null) settingsPanel.SetActive(false);
        if (skinsPanel != null) skinsPanel.SetActive(false);
    }

    // 🔹 Abre el panel principal de Settings
    public void OpenSettings()
    {
        if (settingsPanel == null) return;

        settingsPanel.SetActive(true);
        if (skinsPanel != null) skinsPanel.SetActive(false);
        Time.timeScale = 0f; // pausa el juego
        isOpen = true;
    }

    // 🔹 Cierra el panel de Settings
    public void CloseSettings()
    {
        if (settingsPanel == null) return;

        settingsPanel.SetActive(false);
        Time.timeScale = 1f; // reanuda el juego
        isOpen = false;
    }

    // 🔹 Alternar (si usas un mismo botón)
    public void ToggleSettings()
    {
        if (isOpen)
            CloseSettings();
        else
            OpenSettings();
    }

    // 🔹 Abre el panel de Skins desde Settings
    public void OpenSkins()
    {
        if (skinsPanel == null) return;

        settingsPanel.SetActive(false);
        skinsPanel.SetActive(true);
    }

    // 🔹 Vuelve del panel Skins al panel Settings
    public void CloseSkins()
    {
        if (skinsPanel == null) return;

        skinsPanel.SetActive(false);
        settingsPanel.SetActive(true);
    }
}
