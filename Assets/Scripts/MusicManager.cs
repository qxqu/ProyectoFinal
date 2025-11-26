using UnityEngine;
using UnityEngine.UI;

public class MusicManager : MonoBehaviour
{
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private Slider volumeSlider;

    private static MusicManager instance;

    private void Awake()
    {
        // Patrón Singleton
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject); 
            return;
        }
    }

    private void Start()
    {
        if (musicSource == null)
            musicSource = GetComponent<AudioSource>();

        float savedVolume = PlayerPrefs.GetFloat("MusicVolume", 0.5f);
        musicSource.volume = savedVolume;

        if (volumeSlider != null)
        {
            volumeSlider.value = savedVolume;
            volumeSlider.onValueChanged.AddListener(ChangeVolume);
        }
    }

    private void ChangeVolume(float value)
    {
        musicSource.volume = value;
        PlayerPrefs.SetFloat("MusicVolume", value);
    }

    public void RegisterSlider(Slider newSlider)
{
    if (newSlider == null) return;

    volumeSlider = newSlider;
    volumeSlider.value = musicSource.volume;
    volumeSlider.onValueChanged.RemoveAllListeners();
    volumeSlider.onValueChanged.AddListener(ChangeVolume);
}

}
