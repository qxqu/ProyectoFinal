using UnityEngine;
using System.Collections.Generic; // estructura de datos

public class SettingsController : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject skinsPanel;
    [SerializeField] private GameObject ayudaPanel;

    // estructura de datos
    private Stack<GameObject> panelHistory = new Stack<GameObject>(); 
    private bool isOpen = false;

    private void Start()
    {
        
        if (settingsPanel == null)
            settingsPanel = GameObject.Find("SettingsPanel");

        if (skinsPanel == null)
            skinsPanel = GameObject.Find("PanelSkins");

        if (ayudaPanel == null)
            ayudaPanel = GameObject.Find("AyudaPanel");

        
        if (settingsPanel != null) settingsPanel.SetActive(false);
        if (skinsPanel != null) skinsPanel.SetActive(false);
        if (ayudaPanel != null) ayudaPanel.SetActive(false);
    }

    
    public void OpenSettings()
    {
        if (settingsPanel == null) return;

        settingsPanel.SetActive(true);
        panelHistory.Clear(); 

        Time.timeScale = 0f;
        isOpen = true;
    }

    
    public void CloseSettings()
    {
        if (settingsPanel == null) return;

        settingsPanel.SetActive(false);
        Time.timeScale = 1f;
        panelHistory.Clear();
        isOpen = false;
    }

   
    private void OpenPanel(GameObject panelToOpen)
    {
        if (panelToOpen == null) return;

        
        if (panelHistory.Count == 0)
        {
            
            panelHistory.Push(settingsPanel);
        }
        else
        {
            
            panelHistory.Push(GetCurrentPanel());
        }

        
        GetCurrentPanel()?.SetActive(false);

       
        panelToOpen.SetActive(true);
    }

    
    public void GoBack()
    {
        if (panelHistory.Count == 0)
        {
            CloseSettings(); 
            return;
        }

        
        GetCurrentPanel()?.SetActive(false);

        
        GameObject previous = panelHistory.Pop();
        previous.SetActive(true);
    }

    
    private GameObject GetCurrentPanel()
    {
        if (settingsPanel.activeSelf) return settingsPanel;
        if (skinsPanel.activeSelf) return skinsPanel;
        if (ayudaPanel.activeSelf) return ayudaPanel;

        return null;
    }

    
    public void OpenSkins()
    {
        OpenPanel(skinsPanel);
    }

    public void OpenAyuda()
    {
        OpenPanel(ayudaPanel);
    }
}
