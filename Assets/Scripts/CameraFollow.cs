using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform followTransform;
    public BoxCollider2D[] mapBounds;

    private float xMin, xMax, yMin, yMax;
    private float camX, camY;
    private float camOrthsize;
    private float cameraRatio;
    private Camera mainCam;

    private int pcIndex; //Index of which Level the Player is currently playing at!
    private int oldIndex; //The last index, stops the programm from countlessly recalibrating the cam

    public void CalibrateCam()
    {
        xMin = mapBounds[pcIndex].bounds.min.x;
        xMax = mapBounds[pcIndex].bounds.max.x;
        yMin = mapBounds[pcIndex].bounds.min.y;
        yMax = mapBounds[pcIndex].bounds.max.y;

        mainCam = GetComponent<Camera>();
        camOrthsize = mainCam.orthographicSize;
        cameraRatio = mainCam.aspect * camOrthsize;  // FIX: Berechne die halbe Breite

        oldIndex = pcIndex;
    }
    private void Start()
    {
        pcIndex = PlayerTeleporter.CurrentIndex;
        oldIndex = pcIndex;
        CalibrateCam();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        pcIndex = PlayerTeleporter.CurrentIndex; //Looks if the Index has changed to update the cam if needed!
        if (pcIndex != oldIndex)
        {
            CalibrateCam();
        }
        camY = Mathf.Clamp(followTransform.position.y, yMin + camOrthsize, yMax - camOrthsize);
        camX = Mathf.Clamp(followTransform.position.x, xMin + cameraRatio, xMax - cameraRatio);

        // Position berechnen
        Vector3 newPosition = new Vector3(camX, camY, transform.position.z);

        // Kamera auf Pixelgrenzen runden
        float pixelsPerUnit = 16f; // <-- Hier den Wert deiner Sprites/Tiles einsetzen!
        newPosition.x = Mathf.Round(newPosition.x * pixelsPerUnit) / pixelsPerUnit;
        newPosition.y = Mathf.Round(newPosition.y * pixelsPerUnit) / pixelsPerUnit;

        transform.position = newPosition;
    }
}
