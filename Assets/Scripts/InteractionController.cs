using UnityEngine; // Brauchen wir für allgemeine Unity-Funktionen
using System.Collections; // Brauchen wir für Coroutinen (z.B. für Textanzeige, später)



public class InteractionController : MonoBehaviour
{
    // Dies speichert das GameObject, mit dem der Spieler gerade interagieren kann (es muss in Reichweite sein)
    private GameObject currentInteractable = null;
    // Referenz zum Rigidbody2D-Komponente des Spielers
    private Rigidbody2D rb; 

    // Geschwindigkeit, mit der sich der Spieler bewegt
    public float moveSpeed = 5f; 

    // Die Start-Methode wird einmal aufgerufen, wenn das Skript aktiviert wird
    void Start()
    {
        // Hole die Rigidbody2D-Komponente beim Start
        rb = GetComponent<Rigidbody2D>();
    }    

    // Die Update-Methode wird einmal pro Frame aufgerufen
    void Update()
    {

        // ----- Spielerbewegung -----
        // Hole die Eingaben des Spielers für die horizontale (A/D oder Pfeiltasten links/rechts)
        // und vertikale (W/S oder Pfeiltasten hoch/runter) Achse
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        // Erstelle einen 2D-Vektor für die Bewegungsrichtung
        Vector2 movement = new Vector2(moveX, moveY).normalized; // .normalized sorgt für gleichbleibende Geschwindigkeit bei Diagonalbewegungen

        // Setze die Geschwindigkeit des Rigidbody2D
        rb.linearVelocity = movement * moveSpeed;

        // ----- Interaktionsabfrage (ist bereits da, bleibt unverändert) -----
        // if (Input.GetKeyDown(KeyCode.E) && currentInteractable != null)
        // {
        //     InteractWithObject(currentInteractable);
        // }
        
        // Prüfen, ob die "E"-Taste (oder eine andere Taste, die ihr wollt) gedrückt wurde
        // UND ob es ein Objekt gibt, mit dem wir gerade interagieren können
        if (Input.GetKeyDown(KeyCode.E) && currentInteractable != null)
        {
            // Wenn die Taste gedrückt und ein Objekt in Reichweite ist, starten wir die Interaktion
            InteractWithObject(currentInteractable);
        }
    }

    // Diese Funktion wird automatisch aufgerufen, wenn der Spieler (mit seinem Collider2D)
    // in den Trigger-Bereich eines anderen 2D-Colliders eindringt
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Prüfen, ob das Objekt, in das wir eintreten, den Tag "Interactable" hat
        if (other.CompareTag("Interactable"))
        {
            // Wenn ja, speichern wir dieses Objekt als unser aktuell interagierbares Objekt
            currentInteractable = other.gameObject;
            Debug.Log("In Reichweite von: " + currentInteractable.name); // Eine Nachricht in der Konsole zur Info
        }
    }

    // Diese Funktion wird automatisch aufgerufen, wenn der Spieler
    // den Trigger-Bereich eines anderen 2D-Colliders wieder verlässt
    private void OnTriggerExit2D(Collider2D other)
    {
        // Wenn das Objekt, das wir verlassen, dasjenige ist, mit dem wir gerade interagieren konnten
        if (other.gameObject == currentInteractable)
        {
            // Setze es auf null, da es nicht mehr in Reichweite ist
            currentInteractable = null;
            Debug.Log("Außer Reichweite von: " + other.name); // Eine Nachricht in der Konsole zur Info
        }
    }

    // Diese zentrale Funktion entscheidet, was basierend auf dem Typ des Objekts passiert
    private void InteractWithObject(GameObject interactableObject)
    {
        Debug.Log("Interagiere mit: " + interactableObject.name);

        // Hier nutzen wir die Namen der Objekte, um zu entscheiden, was zu tun ist
        // Später könnte man hier komplexere IDs oder Tags nutzen
        if (interactableObject.name == "NPC")
        {
            HandleNPCInteraction();
        }
        else if (interactableObject.name == "Cabinet")
        {
            HandleCabinetInteraction();
        }
        else if (interactableObject.name == "Door")
        {
            HandleDoorInteraction();
        }
    }

    // ----- Spezifische Interaktions-Funktionen (noch leer) -----

    private void HandleNPCInteraction()
    {
        // Hier kommt die Logik für den NPC-Dialog hin
        Debug.Log("NPC-Dialog-Logik wird hier gestartet.");
    }

    private void HandleCabinetInteraction()
    {
        // Hier kommt die Logik für das Öffnen des Schranks und das Aufnehmen von Gegenständen hin
        Debug.Log("Schrank-Logik wird hier gestartet.");
    }

    private void HandleDoorInteraction()
    {
        // Hier kommt die Logik für das Wechseln der Türgrafik und des Colliders hin
        Debug.Log("Tür-Logik wird hier gestartet.");
    }
}