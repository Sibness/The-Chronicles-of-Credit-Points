using UnityEngine;
using UnityEngine.InputSystem;

public class LaptopInteractable : MonoBehaviour
{
    public GameObject interactionIcon;      // E-Icon über dem Laptop
    public GameObject[] viruses;            // Referenz zu den Virus-Objekten
    public InputAction moveAction;

    private bool playerInRange = false;
    private bool isDeactivated = false;

    private AudioManager audioManager;

    void Start()
    {
        interactionIcon.SetActive(false);
        moveAction.Enable();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    void Update()
    {
        if (playerInRange && !isDeactivated && moveAction.ReadValue<float>() == 1)
        {
            DeactivateLaptop();
        }
    }

    void DeactivateLaptop()
    {
        isDeactivated = true;
        interactionIcon.SetActive(false);
        audioManager.PlaySFX(audioManager.laptop_button);

        foreach (var virus in viruses)
        {
            Destroy(virus); // Alternativ: virus.SetActive(false);
        }

        Debug.Log("Laptop wurde deaktiviert!");
        // Optional: Animation, Sound, Effekte
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isDeactivated)
        {
            playerInRange = true;
            interactionIcon.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            interactionIcon.SetActive(false);
        }
    }

    public void AddVirus(GameObject virus)
    {
        if (virus != null && !isDeactivated)
        {
            GameObject[] newViruses = new GameObject[viruses.Length + 1];
            for (int i = 0; i < viruses.Length; i++)
            {
                newViruses[i] = viruses[i];
            }
            newViruses[newViruses.Length - 1] = virus;
            viruses = newViruses;
        }
    }
}