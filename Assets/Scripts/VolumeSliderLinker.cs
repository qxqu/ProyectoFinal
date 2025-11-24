using UnityEngine;
using UnityEngine.UI;

public class VolumeSliderLinker : MonoBehaviour
{
    private void Start()
    {
        Slider slider = GetComponent<Slider>();

        if (FindObjectOfType<MusicManager>() != null)
        {
            FindObjectOfType<MusicManager>().RegisterSlider(slider);
        }
        else
        {
            Debug.LogWarning("🎧 No se encontró el MusicManager en la escena.");
        }
    }
}
