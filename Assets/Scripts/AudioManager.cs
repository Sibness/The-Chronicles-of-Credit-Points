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
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        PlayMenuMusicImmediate(); // Sofortiger Start beim ersten Laden
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
    }

    private void PlayMenuMusicImmediate()
    {
        musicSource.clip = menu_background;
        musicSource.loop = true;
        musicSource.volume = 1f;
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
        float targetVolume = 1f;
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
    }

    public void SetSFXVolume(float volume)
    {
        SFXSource.volume = volume;
    }
}
