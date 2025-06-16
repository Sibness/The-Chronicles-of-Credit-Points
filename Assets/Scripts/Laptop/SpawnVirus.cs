using UnityEngine;

public class SpawnVirus : MonoBehaviour
{
    public int minCount = 3;
    public int maxCount = 500;

    public GameObject virusPrefab;
    public LaptopInteractable laptopInteractable;

    void Start()
    {
        int count = Random.Range(minCount, maxCount + 1);
        for (int i = 0; i < count; i++)
        {
            Vector3 spawnPosition = new Vector3(Random.Range(-8f, 8f), Random.Range(-4f, 4f), 0f);
            GameObject virusObject = Instantiate(virusPrefab, spawnPosition, Quaternion.identity);
            OrbitingVirus virusParams = virusObject.GetComponent<OrbitingVirus>();
            virusParams.radius = Random.Range(0.5f, 1.5f);
            virusParams.speed = Random.Range(20, 300);
            virusParams.angleOffset = Random.Range(0f, 360f);
            virusParams.clockwise = Random.Range(0, 2) == 0; // Zufällige Richtung (Uhrzeigersinn oder Gegenuhrzeigersinn)
            virusParams.center = transform;
            laptopInteractable.AddVirus(virusObject);
        }
    }
}
