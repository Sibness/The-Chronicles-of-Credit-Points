using UnityEngine;
using UnityEngine.UI;

public class EndCreditsScript : MonoBehaviour
{
    public float scrollSpeed = 80f;
    private RectTransform rectTransform;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Get the RectTransform component of the UI element
        rectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        //Move the txt upwards over time
        rectTransform.anchoredPosition += new Vector2(0, scrollSpeed * Time.deltaTime);
    }
}
