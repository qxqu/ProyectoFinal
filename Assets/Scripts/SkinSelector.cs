using UnityEngine;

public class SkinSelector : MonoBehaviour
{
    public static int savedSkinIndex = 0;

    public void SelectSkin(int index)
    {
        savedSkinIndex = index;
        Debug.Log("Skin seleccionada: " + index);
    }
}
