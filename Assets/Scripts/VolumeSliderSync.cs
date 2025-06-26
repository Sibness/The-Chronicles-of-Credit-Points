using UnityEngine;
using UnityEngine.UI;

public class VolumeSliderSync : MonoBehaviour
{
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;

    void Start()
    {
        // Verhindert, dass OnValueChanged beim Setzen erneut speichert
        if (musicSlider != null)
        {
            musicSlider.onValueChanged.RemoveAllListeners(); // Erst alte Listener entfernen
            musicSlider.value = PlayerPrefs.GetFloat("MusicVolume", 1f);
            musicSlider.onValueChanged.AddListener((v) => AudioManager.Instance.SetMusicVolume(v));
        }

        if (sfxSlider != null)
        {
            sfxSlider.onValueChanged.RemoveAllListeners();
            sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume", 1f);
            sfxSlider.onValueChanged.AddListener((v) => AudioManager.Instance.SetSFXVolume(v));
        }
    }
}
