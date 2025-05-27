using UnityEngine;

public class PlayerTeleporter : MonoBehaviour
{
    public Transform[] spawnpoints; //All the Spawnpoints of the player
    private static int index = 0; //The current point the player spawns
    private bool teleport = false; //Toggles if the player teleports to a spawnpoint or not

    public void Teleport()
    {
        teleport = true;
    }
    public static int CurrentIndex { get { return index; } }

    public static void setIndex(int newIndex)
    { 
       index = newIndex; 
    }
    void Update()
    {
        if(teleport && spawnpoints != null && index < spawnpoints.Length)
        {
            teleport = false;
            Transform target = spawnpoints[index];
            transform.position = target.position;
        }
    }
}
