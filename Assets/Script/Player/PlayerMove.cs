using NaughtyAttributes;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMove : MonoBehaviour
{
    [Header("���͂̉����x"), Range(0, 10f)]
    [SerializeField] private float _inputAcceleration = 5f;

    [Header("���͂̌����x"), Range(0, 10f)]
    [SerializeField] private float _inputDeceleration = 2f;

    [Header("���͂̍ō����x"), Range(0, 20f)]
    [SerializeField] private float _inputMaxSpeed = 10f;

    [Header("�����̋���"), Range(0, 10f)]
    [SerializeField] private float _recoilForceMultiplier = 5f;

    [Header("�����̌�����"), Range(0, 10f)]
    [SerializeField] private float _recoilDeceleration = 2f;

    [Header("�����̍ō����x"), Range(0, 20f)]
    [SerializeField] private float _recoilMaxSpeed = 10f;

    [Header("�S�̂̍ō����x"), Range(0, 20f)]
    [SerializeField] private float _velocityMaxSpeed = 10f;

    [Header("�f�u���Փˎ��̌�����")]
    [SerializeField, Range(0, 1f)] private float _debrisDeceleration = 0.5f;

    [Header("�f�u���Փˎ��̌�����")]
    [SerializeField, Range(0, 1f)] private float _enemyDeceleration = 0.5f;


    [SerializeField] private DetaPlayerMove _playerMoveData; // ScriptableObject�ւ̎Q��


    [SerializeField] private TextMeshProUGUI _debugText; // �f�o�b�O�p�e�L�X�g



    // �S�̂̑��x�x�N�g��
    public Vector2 _currentVelocity { private set; get; }
    // ���͂̑��x�x�N�g��
    private Vector2 _currentInputVelocity;
    // �����̑��x�x�N�g��
    private Vector2 _currentRecoilVelocity;

    // ���̓x�N�g��
    private Vector2 _inputDirection;


    private PlayerFire _playerFire;

    private void Awake()
    {
        _playerFire = GetComponent<PlayerFire>();



    }


    private void FixedUpdate()
    {
        UpdateVelocityAndPosition();
    }


    private void UpdateVelocityAndPosition()
    {

        // ���̓x�N�g���ɉ����x��������
        _currentInputVelocity += _inputDirection * _inputAcceleration * Time.fixedDeltaTime;

        // ���̓x�N�g���̐���
        _currentInputVelocity = Vector2.ClampMagnitude(_currentInputVelocity, _inputMaxSpeed);

        // ���͂��Ȃ��ꍇ�͌���
        if (_inputDirection == Vector2.zero)
        {
            _currentInputVelocity = Vector2.Lerp(_currentInputVelocity, Vector2.zero, _inputDeceleration * Time.fixedDeltaTime);
        }

        // �����x�N�g���̐���
        _currentRecoilVelocity = Vector2.ClampMagnitude(_currentRecoilVelocity, _recoilMaxSpeed);

        // �����x�N�g���̌���
        _currentRecoilVelocity = Vector2.Lerp(_currentRecoilVelocity, Vector2.zero, _recoilDeceleration * Time.fixedDeltaTime);

        // �S�̂̑��x�x�N�g�������� �A�S�̂̑��x�x�N�g���̐���
        _currentVelocity = _currentInputVelocity + _currentRecoilVelocity;
        _currentVelocity = Vector2.ClampMagnitude(_currentVelocity, _velocityMaxSpeed);

        // �f�o�b�O�p�e�L�X�g�X�V
        _debugText.text = $"Velocity: {_currentVelocity.sqrMagnitude}";

        // �ʒu���X�V
        transform.position += (Vector3)_currentVelocity * Time.fixedDeltaTime;

    }


    /// <summary>
    /// �f�u���ɓ����������̑��x��������
    /// </summary>
    public void ReduceVelocityOnDebrisHit()
    {
        // �S�̓I�Ɍ���������

        _currentInputVelocity *= _debrisDeceleration;  // ���͑��x������
        _currentRecoilVelocity *= _debrisDeceleration; // �������x������
    }

    /// <summary>
    /// �G�ɓ����������̑��x��������
    /// </summary>
    public void ReduceVelocityOnEnemyHit()
    {
        _currentInputVelocity *= _debrisDeceleration;  // ���͑��x������
        _currentRecoilVelocity *= _debrisDeceleration; // �������x������
    }


    public void AddRecoilForce(Vector2 force)
    {
        // ���K�� ���������邽�ߋt�ɂ���
        force = force.normalized * -1;

        // ����������(��)
        _currentRecoilVelocity += force * _recoilForceMultiplier;
    }


    /// <summary>
    /// ���̓C�x���g�p���\�b�h
    /// </summary>
    /// <param name="context">���̓R���e�L�X�g</param>
    public void OnMove(InputAction.CallbackContext context)
    {
        // ���͂̕������X�V
        _inputDirection = context.ReadValue<Vector2>();
    }



#if UNITY_EDITOR
    [Button]
    private void DetaSaveScriptableObject()
    {

        // ScriptableObject�ɒl��ۑ�
        _playerMoveData.InputAcceleration = _inputAcceleration;
        _playerMoveData.InputDeceleration = _inputDeceleration;
        _playerMoveData.InputMaxSpeed = _inputMaxSpeed;
        _playerMoveData.RecoilForceMultiplier = _recoilForceMultiplier;
        _playerMoveData.RecoilDeceleration = _recoilDeceleration;
        _playerMoveData.RecoilMaxSpeed = _recoilMaxSpeed;
        _playerMoveData.VelocityMaxSpeed = _velocityMaxSpeed;
        _playerMoveData.DebrisDeceleration = _debrisDeceleration;
        _playerMoveData.EnemyDeceleration = _enemyDeceleration;

        // �ύX��ۑ�
        UnityEditor.EditorUtility.SetDirty(_playerMoveData);
        UnityEditor.AssetDatabase.SaveAssets();

        Debug.Log("�ݒ��ۑ����܂���");
    }

    [SerializeField]    
    [Button]
    private void DetaLoadScriptableObject()
    {
        // ScriptableObject����l��ǂݍ���
        _inputAcceleration = _playerMoveData.InputAcceleration;
        _inputDeceleration = _playerMoveData.InputDeceleration;
        _inputMaxSpeed = _playerMoveData.InputMaxSpeed;
        _recoilForceMultiplier = _playerMoveData.RecoilForceMultiplier;
        _recoilDeceleration = _playerMoveData.RecoilDeceleration;
        _recoilMaxSpeed = _playerMoveData.RecoilMaxSpeed;
        _velocityMaxSpeed = _playerMoveData.VelocityMaxSpeed;
        _debrisDeceleration = _playerMoveData.DebrisDeceleration;
        _enemyDeceleration = _playerMoveData.EnemyDeceleration;

    }
#endif

    /// <summary>
    /// Debug�p��Gizmos�`��
    /// </summary>
    private void OnDrawGizmos()
    {
        // Gizmos���g�p���ăx�N�g��������

        // x, y �̂��ꂼ��̒������v�Z
        Vector3 xLength = new Vector3(_currentVelocity.x, 0, 0);
        Vector3 yLength = new Vector3(0, _currentVelocity.y, 0);

        // �S�̂̒������v�Z
        Vector3 totalLength = new Vector3(_currentVelocity.x, _currentVelocity.y, 0);

        // x, y �� Gizmos ��`��
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + xLength);
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + yLength);

        // �S�̂̃x�N�g����`��
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + totalLength);

    }
}
