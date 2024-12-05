using UnityEngine;

public class SliderPostion : MonoBehaviour
{
    [SerializeField] private Vector3 _offsetPostion = new Vector3();
    [SerializeField] private Transform targetEnemy = null;
    private RectTransform myRectTransform;

    private void Start()
    {
        myRectTransform = GetComponent<RectTransform>();
    }

    public void SetTarget(Transform enemy)
    {
        targetEnemy = enemy;
    }

    private void Update()
    {
        if (targetEnemy == null)
        {
            Debug.LogError("ターゲットが設定されてないよ！");
            return;
         }

        // World座標からScreen座標に変換
        Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(Camera.main, targetEnemy.position -_offsetPostion);

        // Screen座標からUI座標に変換
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
            myRectTransform.parent as RectTransform,
            screenPoint,
            null,  // Overlayモードの場合はnull
            out Vector2 localPoint))
        {
            myRectTransform.localPosition = localPoint;
        }
    }
}