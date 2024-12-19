using UnityEngine;

public class SliderPostion : MonoBehaviour
{

    // �O���珑�������s��
    public bool _isTracking { get; private set; }

    [SerializeField] private Vector3 _offsetPostion = new Vector3();
    [SerializeField] private Transform targetEnemy = null;
    private RectTransform myRectTransform;

    private void Start()
    {
        // �ʒu��bool������
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
    /// �X���C�_�[�̈ʒu��bool��������
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
            // �Ǐ]�����������̏ꍇ�͉������Ȃ�
            Debug.LogError("�g���b�N���Ă͂����Ȃ�");
            return;
        }

        TrackingTartget();
    }


    /// <summary>
    /// �^�[�Q�b�g�̒Ǐ]����
    /// </summary>
    public void TrackingTartget()
    {
        // World���W����Screen���W�ɕϊ�
        Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(Camera.main, targetEnemy.position - _offsetPostion);

        // Screen���W����UI���W�ɕϊ�
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
            myRectTransform.parent as RectTransform,
            screenPoint,
            null,  // Overlay���[�h�̏ꍇ��null
            out Vector2 localPoint))
        {
            myRectTransform.localPosition = localPoint;
        }
    }
}
