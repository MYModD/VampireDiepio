using UnityEngine;

public class NoiseBasedPlacer : MonoBehaviour
{
    [Header("Material Settings")]
    [SerializeField] private Material noiseMaterial;
    [SerializeField] private int textureSize = 256; // �𑜓x��������

    [Header("Placement Settings")]
    [SerializeField] private GameObject prefab;
    [SerializeField] private float mapSize = 100f;
    [SerializeField, Range(0f, 1f)] private float threshold = 0.5f;
    [SerializeField] private float minDistance = 2f;

    [Header("Optimization")]
    [SerializeField] private int samplingStep = 4; // �T���v�����O�Ԋu
    [SerializeField] private int maxObjects = 1000; // �ő�I�u�W�F�N�g��

    private RenderTexture noiseTexture; 
    private Texture2D noiseResult;

    void Start()
    {
        GeneratePlacement();
    }

    void GeneratePlacement()
    {
        // �����̃I�u�W�F�N�g���N���A
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        try
        {
            // �e�N�X�`���̐ݒ�
            noiseTexture = new RenderTexture(textureSize, textureSize, 0);
            noiseTexture.enableRandomWrite = true;
            noiseTexture.Create();

            Graphics.Blit(null, noiseTexture, noiseMaterial);

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
            // ���\�[�X�̉��
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
        float stepSize = mapSize / textureSize;
        int objectsPlaced = 0;

        // �T���v�����O�Ԋu�Ń��[�v
        for (int x = 0; x < textureSize && objectsPlaced < maxObjects; x += samplingStep)
        {
            for (int y = 0; y < textureSize && objectsPlaced < maxObjects; y += samplingStep)
            {
                Color pixel = noiseResult.GetPixel(x, y);
                if (pixel.r > threshold)
                {
                    Vector3 position = new Vector3(
                        x * stepSize - mapSize / 2,
                        0,
                        y * stepSize - mapSize / 2
                    );

                    // �ȗ������ꂽ�����`�F�b�N
                    bool canPlace = true;
                    Collider[] colliders = Physics.OverlapSphere(position, minDistance);
                    if (colliders.Length == 0)
                    {
                        GameObject obj = Instantiate(prefab, position, Quaternion.identity);
                        obj.transform.parent = transform;
                        objectsPlaced++;
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