using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditCollectable : MonoBehaviour
{
    public int creditIncrease = 1;
    private AudioManager audioManager;

    void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController controller = other.GetComponent<PlayerController>();
        if (controller != null)
        {
            Debug.Log("Current Credits: " + controller.credits + " / Max: " + controller.maxCredits);

            if (controller.credits < controller.maxCredits)
            {
                audioManager.PlaySFX(audioManager.coin);
                controller.ChangeCredits(creditIncrease);
                Destroy(gameObject);
            }
        }
    }
}
