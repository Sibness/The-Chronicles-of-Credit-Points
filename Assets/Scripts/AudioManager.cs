using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("--- Audio Sources ---")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("--- Music Clips ---")]
    public AudioClip game_background;
    public AudioClip menu_background;

    [Header("--- Sound Effects ---")]
    public AudioClip coin;
    public AudioClip level_completed;
    public AudioClip laptop_button;
    public AudioClip death;
    public AudioClip close_menu;
    public AudioClip menu_button;

    [Header("--- Fade Settings ---")]
    public float fadeDuration = 1f;

    private void Awake()
    {
        // Singleton
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            // Lautstärke aus PlayerPrefs laden
            float savedMusicVolume = PlayerPrefs.GetFloat("MusicVolume", 1f);
            float savedSFXVolume = PlayerPrefs.GetFloat("SFXVolume", 1f);

            musicSource.volume = savedMusicVolume;
            SFXSource.volume = savedSFXVolume;

            PlayMenuMusicImmediate(); // Sofortiger Start beim ersten Laden
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "StartScreen" || scene.name == "CreditScene")
        {
            if (musicSource.clip != menu_background)
                StartCoroutine(FadeToNewClip(menu_background));
        }
        else if (scene.name == "SampleScene")
        {
            if (musicSource.clip != game_background)
                StartCoroutine(FadeToNewClip(game_background));
        }

        // Beim Szenenwechsel sicherstellen, dass Volume korrekt bleibt
        musicSource.volume = PlayerPrefs.GetFloat("MusicVolume", 1f);
        SFXSource.volume = PlayerPrefs.GetFloat("SFXVolume", 1f);
    }

    private void PlayMenuMusicImmediate()
    {
        musicSource.clip = menu_background;
        musicSource.loop = true;
        musicSource.Play();
    }

    private IEnumerator FadeToNewClip(AudioClip newClip)
    {
        if (musicSource.isPlaying)
        {
            yield return StartCoroutine(FadeOut());
        }

        musicSource.clip = newClip;
        musicSource.Play();
        yield return StartCoroutine(FadeIn());
    }

    private IEnumerator FadeOut()
    {
        float startVolume = musicSource.volume;

        for (float t = 0; t < fadeDuration; t += Time.unscaledDeltaTime)
        {
            musicSource.volume = Mathf.Lerp(startVolume, 0f, t / fadeDuration);
            yield return null;
        }

        musicSource.volume = 0f;
        musicSource.Stop();
    }

    private IEnumerator FadeIn()
    {
        float targetVolume = PlayerPrefs.GetFloat("MusicVolume", 1f);
        musicSource.volume = 0f;

        for (float t = 0; t < fadeDuration; t += Time.unscaledDeltaTime)
        {
            musicSource.volume = Mathf.Lerp(0f, targetVolume, t / fadeDuration);
            yield return null;
        }

        musicSource.volume = targetVolume;
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }

    public AudioSource GetSFXSource()
    {
        return SFXSource;
    }

    public void SetMusicVolume(float volume)
    {
        musicSource.volume = volume;
        PlayerPrefs.SetFloat("MusicVolume", volume); // Speichern
    }

    public void SetSFXVolume(float volume)
    {
        SFXSource.volume = volume;
        PlayerPrefs.SetFloat("SFXVolume", volume); // Speichern
    }
        public void OnMusicVolumeChanged(float value)
    {
        AudioManager.Instance.SetMusicVolume(value);
    }

    public void OnSFXVolumeChanged(float value)
    {
        AudioManager.Instance.SetSFXVolume(value);
    }
}
