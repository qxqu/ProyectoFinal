using UnityEngine;
using UnityEngine.UI;

public class SkinSelector : MonoBehaviour

{

    public static int savedSkinIndex = 0;
    public Image[] skinButtons;
    public Color normalColor = Color.blue;
    public Color selectedColor1 = Color.white;
    public Color selectedColor2 = Color.orange;
    private void Start()

    {
        savedSkinIndex = PlayerPrefs.GetInt("SelectedSkin", 0);
        if (skinButtons != null && skinButtons.Length > 0)
        {
            UpdateButtonVisuals();
        }
    }

    public void SelectSkin(int index)
    {
        savedSkinIndex = index;
        PlayerPrefs.SetInt("SelectedSkin", index);
        PlayerPrefs.Save();
        if (skinButtons != null && skinButtons.Length > 0)
        {
            UpdateButtonVisuals();
        }
    }



    private void UpdateButtonVisuals()
    {
        if (skinButtons == null || skinButtons.Length == 0)
        {
            return;
        }

        for (int i = 0; i < skinButtons.Length; i++)
        {
            if (skinButtons[i] != null)
            {
                if (i == savedSkinIndex)
                {
                    if (i==1){
                        skinButtons[i].color = selectedColor1;
                    }

                    if (i==2){
                        skinButtons[i].color = selectedColor2;
                    }
                }

                else
                {
                    skinButtons[i].color = normalColor;
                }
            }
        }
    }

}
