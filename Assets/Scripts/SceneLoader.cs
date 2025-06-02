using UnityEngine;
using UnityEngine.SceneManagement; 

public class SceneLoader : MonoBehaviour
{
    public void LoadGame()
    {
        SceneManager.LoadScene("SampleScene"); 
    }

    public void QuitGame() // this quits the game
    {
        Application.Quit();
        Debug.Log("Game has been quit. (This does work ingame but not in the unity inspector, don't worry.)"); // works probably, pretty sure. The smat guy from the video said so
        // to attach it to a button: scroll down to on click event + add one and drag the scene loader game object in the spot. Add the function sceneLoader > quitgame()
    }
}
