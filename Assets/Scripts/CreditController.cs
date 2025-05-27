using UnityEngine;
using UnityEngine.UI;

public class CreditController : MonoBehaviour
{
    public Slider slider;

    public void SetCredits(int creditAmount)
    {
        slider.value = creditAmount;
    }

    public void SetMaxCredits(int maxCredits)
    {
        slider.maxValue = maxCredits;
    }

}
