using UnityEngine;
using UnityEngine.UI;

public class CreditController : MonoBehaviour
{
    public Slider slider;
    public PlayerController playerController;

    public void SetCredits(int creditAmount)
    {
        slider.value = creditAmount;
    }

    public void SetMaxCredits(int maxCredits)
    {
        slider.maxValue = maxCredits;
    }

    public void Update()
    {
        slider.value = playerController.currentCredits;
        slider.maxValue = playerController.maxCredits;
        if (playerController.maxCredits == 0)
        {
            slider.value = 1;
            slider.maxValue = 1;
        }
    }

}
