using UnityEngine;

public class SettingsController : MonoBehaviour
{
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject skinsPanel;
    [SerializeField] private GameObject ayudaPanel;

    private bool isOpen = false;

    private void Start()
    {
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

        if (ayudaPanel == null)
        {
            ayudaPanel = GameObject.Find("AyudaPanel");
            if (ayudaPanel == null)
                Debug.LogWarning("⚠️ AyudaPanel no está asignado en el Inspector ni encontrado en la escena.");
        }

        if (settingsPanel != null) settingsPanel.SetActive(false);
        if (skinsPanel != null) skinsPanel.SetActive(false);
        if (ayudaPanel != null) ayudaPanel.SetActive(false);
    }

    public void OpenSettings()
    {
        if (settingsPanel == null) return;

        settingsPanel.SetActive(true);
        if (skinsPanel != null) skinsPanel.SetActive(false);
        if (ayudaPanel != null) ayudaPanel.SetActive(false);

        Time.timeScale = 0f;
        isOpen = true;
    }

    public void CloseSettings()
    {
        if (settingsPanel == null) return;

        settingsPanel.SetActive(false);
        Time.timeScale = 1f; 
        isOpen = false;
    }

    public void ToggleSettings()
    {
        if (isOpen)
            CloseSettings();
        else
            OpenSettings();
    }

    public void OpenSkins()
    {
        if (skinsPanel == null) return;

        settingsPanel.SetActive(false);
        skinsPanel.SetActive(true);
        if (ayudaPanel != null) ayudaPanel.SetActive(false);
    }

    public void CloseSkins()
    {
        if (skinsPanel == null) return;

        skinsPanel.SetActive(false);
        settingsPanel.SetActive(true);
    }

    public void OpenAyuda()
    {
        if (ayudaPanel == null) return;

        settingsPanel.SetActive(false);
        ayudaPanel.SetActive(true);
        if (skinsPanel != null) skinsPanel.SetActive(false);
    }

    public void CloseAyuda()
    {
        if (ayudaPanel == null) return;

        ayudaPanel.SetActive(false);
        settingsPanel.SetActive(true);
    }
}
