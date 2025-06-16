using UnityEngine;
using UnityEngine.Tilemaps;

public class Teleporter : MonoBehaviour
{
    [Header("Player Components")]
    public PlayerController playerController;
    public PlayerTeleporter teleporter;
    [Header("Tilemap Renderer")]
    public TilemapRenderer tileRenderer;
    public void Update()
    {
        if (playerController == null)
        {
            return;
        }
        if (playerController.credits < playerController.maxCredits)
        {
            // If the player has less than the maximum credits, disable the teleporter
            tileRenderer.enabled = false;
        }
        else
        {
            // If the player has enough credits, enable the teleporter
            tileRenderer.enabled = true;
        }
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (playerController.credits < playerController.maxCredits)
        {
            return;
        }
        Debug.Log("Teleporting player to next spawn point." + PlayerTeleporter.CurrentIndex);
        // Increment the index for the next teleport
        PlayerTeleporter.setIndex(PlayerTeleporter.CurrentIndex + 1);
        playerController.ChangeCredits(0); // Reset the player's credits after teleporting
        // Teleport the player to the next spawn point
        teleporter.Teleport();
    }

}
