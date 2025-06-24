using UnityEngine;

public class OrbitingVirus : MonoBehaviour
{
    public Transform center;            // Der Laptop
    public float radius = 1.5f;         // Abstand zur Mitte
    public float speed = 50f;           // Winkelgeschwindigkeit (°/Sekunde)
    public float angleOffset = 0f;      // Startwinkel in Grad
    public bool clockwise = true;       // Drehrichtung (true = Uhrzeigersinn)
    private AudioManager audioManager;
    private float angle;

    void Start()
    {
        angle = angleOffset;
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    void Update()
    {
        // Richtung bestimmen
        float direction = clockwise ? -1f : 1f;

        // Winkel berechnen
        angle += direction * speed * Time.deltaTime;
        float radians = angle * Mathf.Deg2Rad;

        // Position aktualisieren
        Vector3 offset = new Vector3(Mathf.Cos(radians), Mathf.Sin(radians), 0) * radius;
        transform.position = center.position + offset;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerController controller = collision.GetComponent<PlayerController>();
            if (controller != null)
            {
                audioManager.PlaySFX(audioManager.death);
                controller.Pt.Teleport(); // Spieler teleportieren
            }
        }
    }
}