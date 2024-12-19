using UnityEngine;

public class SliderPostion : MonoBehaviour
{

    // 外から書き換え不可
    public bool _isTracking { get; private set; }

    [SerializeField] private Vector3 _offsetPostion = new Vector3();
    [SerializeField] private Transform targetEnemy = null;
    private RectTransform myRectTransform;

    private void Start()
    {
        // 位置とbool初期化
        myRectTransform.localPosition = Vector3.zero;
        _isTracking = false;

        myRectTransform = GetComponent<RectTransform>();
    }

    public void SetTarget(Transform enemy)
    {
        targetEnemy = enemy;
        _isTracking = true;
    }


    /// <summary>
    /// スライダーの位置とboolを初期化
    /// </summary>
    public void InitializeSliderPostion()
    {
        _isTracking = false;
        myRectTransform.localPosition = Vector3.zero;
    }



    private void Update()
    {
        if (!_isTracking)
        {
            // 追従処理が無効の場合は何もしない
            Debug.LogError("トラックしてはいけない");
            return;
        }

        TrackingTartget();
    }


    /// <summary>
    /// ターゲットの追従処理
    /// </summary>
    public void TrackingTartget()
    {
        // World座標からScreen座標に変換
        Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(Camera.main, targetEnemy.position - _offsetPostion);

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
