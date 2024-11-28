using UnityEngine;
using System.Collections;

public class NoiseBasedPlacer : MonoBehaviour
{
    [Header("Material Settings")]
    [SerializeField] private Material noiseMaterial;
    [SerializeField] private int textureSize = 256;

    [Header("Placement Settings")]
    [SerializeField] private GameObject prefab;
    [SerializeField] private float mapSize = 100f;
    [SerializeField, Range(0f, 1f)] private float threshold = 0.5f;
    [SerializeField] private float minDistance = 2f;

    [Header("Optimization")]
    [SerializeField] private int samplingStep = 4;
    [SerializeField] private int maxObjects = 1000;

    private RenderTexture noiseTexture;
    private Texture2D noiseResult;

    void Start()
    {
        StartCoroutine(GeneratePlacement());
    }

    IEnumerator GeneratePlacement()
    {
        // Clear existing objects
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        // Setup noise texture
        if (noiseTexture != null)
        {
            noiseTexture.Release();
            Destroy(noiseTexture);
        }

        noiseTexture = new RenderTexture(textureSize, textureSize, 0);
        noiseTexture.enableRandomWrite = true;
        noiseTexture.Create();

        // Render noise
        Graphics.Blit(null, noiseTexture, noiseMaterial);

        // Wait for end of frame to ensure rendering is complete
        yield return new WaitForEndOfFrame();

        try
        {
            // Read pixels
            noiseResult = new Texture2D(textureSize, textureSize, TextureFormat.RGB24, false);
            RenderTexture.active = noiseTexture;
            noiseResult.ReadPixels(new Rect(0, 0, textureSize, textureSize), 0, 0);
            noiseResult.Apply();

            PlaceObjects();
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Error in GeneratePlacement: {e.Message}");
        }
        finally
        {
            // Cleanup
            RenderTexture.active = null;
            if (noiseTexture != null)
            {
                noiseTexture.Release();
                Destroy(noiseTexture);
            }
            if (noiseResult != null)
            {
                Destroy(noiseResult);
            }
        }
    }

    void PlaceObjects()
    {
        float cellSize = mapSize / textureSize;
        int objectsPlaced = 0;

        for (int x = 0; x < textureSize && objectsPlaced < maxObjects; x += samplingStep)
        {
            for (int y = 0; y < textureSize && objectsPlaced < maxObjects; y += samplingStep)
            {
                Color pixel = noiseResult.GetPixel(x, y);
                if (pixel.r > threshold)
                {
                    // Convert texture coordinates to world position
                    float worldX = (x / (float)textureSize - 0.5f) * mapSize;
                    float worldZ = (y / (float)textureSize - 0.5f) * mapSize;
                    Vector3 position = new Vector3(worldX, 0, worldZ);

                    // Check for nearby objects
                    bool canPlace = true;
                    Collider[] colliders = Physics.OverlapSphere(position, minDistance);
                    if (colliders.Length == 0)
                    {
                        GameObject obj = Instantiate(prefab, position, Quaternion.identity);
                        obj.transform.parent = transform;
                        objectsPlaced++;

                        // Optional: Add random rotation
                        obj.transform.rotation = Quaternion.Euler(0, Random.Range(0f, 360f), 0);
                    }
                }
            }
        }
        Debug.Log($"Placed {objectsPlaced} objects");
    }

    void OnValidate()
    {
        samplingStep = Mathf.Max(1, samplingStep);
        maxObjects = Mathf.Max(1, maxObjects);
        textureSize = Mathf.Max(1, textureSize);
    }

    private void OnDestroy()
    {
        if (noiseTexture != null)
        {
            noiseTexture.Release();
            Destroy(noiseTexture);
        }
        if (noiseResult != null)
        {
            Destroy(noiseResult);
        }
    }
}