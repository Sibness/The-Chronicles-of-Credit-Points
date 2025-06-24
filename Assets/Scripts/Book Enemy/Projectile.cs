using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Vector2 direction = Vector2.right;
    public float speed = 5f;
    public float maxDistance = 10f;

    private Vector3 startPosition;
    private AudioManager audioManager;

    void Start()
    {
        startPosition = transform.position;
        direction.Normalize();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    void Update()
    {
        transform.position += (Vector3)(direction * speed * Time.deltaTime);

        if (Vector3.Distance(startPosition, transform.position) >= maxDistance)
        {
            Destroy(gameObject);
        }
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
                Destroy(gameObject);
            }
        }
    }
}
