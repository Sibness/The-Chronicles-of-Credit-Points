using UnityEngine;
using UnityEngine.InputSystem;

public class SceneInputTrigger : MonoBehaviour
{
    public SceneLoader sceneLoader;

    void Update()
    {
        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            sceneLoader.BackToTitle();
        }

        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            sceneLoader.BackToTitle();
        }

        if (Keyboard.current.xKey.wasPressedThisFrame)
        {
            sceneLoader.BackToTitle();
        }
    }
}