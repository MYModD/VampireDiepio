using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NoiseBasedPlacer : MonoBehaviour
{
    [Header("Texture Settings")]
    public Texture2D noiseTexture;
    public float whiteThreshold = 0.5f;

    [Header("Placement Settings")]
    public GameObject prefabToPlace;
    public int maxAttempts = 1000;
    public float minDistance = 1f;
    public Vector2 placementArea = new Vector2(10f, 10f);
    public float yPosition = 0f;

    [Header("Object Settings")]
    public Vector2 scaleRange = new Vector2(0.8f, 1.2f);
    public Vector2 rotationRange = new Vector2(0f, 360f);
    public int maxObjects = 100;

    private List<Vector3> placedPositions = new List<Vector3>();

    public void PlaceObjects()
    {
        ClearExistingObjects();
        placedPositions.Clear();

        for (int i = 0; i < maxAttempts && placedPositions.Count < maxObjects; i++)
        {
            Vector3 randomPosition = GetRandomPosition();

            // テクスチャ上の位置を計算
            Vector2 texturePosition = new Vector2(
                (randomPosition.x / placementArea.x + 0.5f),
                (randomPosition.z / placementArea.y + 0.5f)
            );

            // 範囲内に収める
            texturePosition.x = Mathf.Clamp01(texturePosition.x);
            texturePosition.y = Mathf.Clamp01(texturePosition.y);

            // ピクセルの色を取得
            Color pixelColor = noiseTexture.GetPixelBilinear(texturePosition.x, texturePosition.y);
            float brightness = (pixelColor.r + pixelColor.g + pixelColor.b) / 3f;

            if (brightness >= whiteThreshold && IsValidPosition(randomPosition))
            {
                GameObject newObject = PlaceObject(randomPosition);
                placedPositions.Add(randomPosition);
            }
        }
    }

    private Vector3 GetRandomPosition()
    {
        return new Vector3(
            Random.Range(-placementArea.x / 2f, placementArea.x / 2f),
            yPosition,
            Random.Range(-placementArea.y / 2f, placementArea.y / 2f)
        );
    }

    private bool IsValidPosition(Vector3 position)
    {
        foreach (Vector3 placedPosition in placedPositions)
        {
            if (Vector3.Distance(position, placedPosition) < minDistance)
            {
                return false;
            }
        }
        return true;
    }

    private GameObject PlaceObject(Vector3 position)
    {
        GameObject obj = Instantiate(prefabToPlace, position, Quaternion.identity);
        obj.transform.parent = transform;

        // ランダムなスケールと回転を適用
        float scale = Random.Range(scaleRange.x, scaleRange.y);
        obj.transform.localScale = Vector3.one * scale;

        float rotation = Random.Range(rotationRange.x, rotationRange.y);
        obj.transform.rotation = Quaternion.Euler(0f, rotation, 0f);

        return obj;
    }

    private void ClearExistingObjects()
    {
        foreach (Transform child in transform)
        {
            if (Application.isPlaying)
            {
                Destroy(child.gameObject);
            }
            else
            {
                DestroyImmediate(child.gameObject);
            }
        }
    }

    // エディタ上での配置エリアの可視化
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Vector3 center = transform.position + new Vector3(0f, yPosition, 0f);
        Vector3 size = new Vector3(placementArea.x, 0.1f, placementArea.y);
        Gizmos.DrawWireCube(center, size);
    }
}