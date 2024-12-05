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
            Debug.LogError("�^�[�Q�b�g���ݒ肳��ĂȂ���I");
            return;
         }

        // World���W����Screen���W�ɕϊ�
        Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(Camera.main, targetEnemy.position -_offsetPostion);

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