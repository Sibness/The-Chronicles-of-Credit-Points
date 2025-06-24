using UnityEngine;
using UnityEngine.UI;

public class PlayMenuSound : MonoBehaviour
{
    private AudioManager audioManager;

    void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        GetComponent<Button>().onClick.AddListener(PlaySound);
    }

    void PlaySound()
    {
        if (audioManager != null)
            audioManager.PlaySFX(audioManager.menu_button);
    }
}
