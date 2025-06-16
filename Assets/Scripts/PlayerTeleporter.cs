using UnityEngine;

public class PlayerTeleporter : MonoBehaviour
{
    public Transform[] spawnpoints; //All the Spawnpoints of the player
    private static int index = 0; //The current point the player spawns
    private bool teleport = false; //Toggles if the player teleports to a spawnpoint or not
    public PlayerController playerController;

    public void Teleport()
    {
        if (spawnpoints != null && index < spawnpoints.Length)
        {
            Transform target = spawnpoints[index];
            transform.position = target.position;
            playerController.maxCredits = spawnpoints[index].GetComponent<SpawnpointCoins>().creditsInLevel;
        }
    }
    public static int CurrentIndex { get { return index; } }

    public static void setIndex(int newIndex)
    { 
       index = newIndex; 
    }

}
