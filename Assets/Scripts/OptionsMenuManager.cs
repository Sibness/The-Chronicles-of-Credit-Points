using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class OptionsMenuManager : MonoBehaviour
{
    public GameObject optionsMenu;
    public GameObject defaultSelectedButton;

    private bool isOpen = false;

    void Update()
    {
        // Verhindert NullReference bei altem Input System oder fehlender Tastatur
        if (Keyboard.current == null) return;

        if (Keyboard.current.escapeKey.wasPressedThisFrame ||
            Keyboard.current.tabKey.wasPressedThisFrame ||
            Keyboard.current.oKey.wasPressedThisFrame)
        {
            ToggleMenu();
        }
    }

    public void ToggleMenu()
    {
        isOpen = !isOpen;
        optionsMenu.SetActive(isOpen);

        if (isOpen)
        {
            Time.timeScale = 0f; // Spiel pausieren
            EventSystem.current?.SetSelectedGameObject(defaultSelectedButton);
        }
        else
        {
            Time.timeScale = 1f; // Spiel fortsetzen
            EventSystem.current?.SetSelectedGameObject(null);
        }
    }

    public void ResumeGame()
    {
        isOpen = false;
        optionsMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void QuitGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("StartScreen");
    }
}
