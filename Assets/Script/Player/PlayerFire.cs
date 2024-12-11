using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerFire : MonoBehaviour
{
    [SerializeField] private Transform _firePostion;
    [SerializeField] private Transform _bulletParent;

    [SerializeField] private float _coolTime = 0.5f; // �e���˂̃N�[���_�E���^�C��
    [SerializeField] private float _multiplyForce = 1f;

    [SerializeField] private BulletObjectPoolManager _bulletObjectPoolManager;

    private float _coolTimeValue;
    private bool _isFiring = false; // �������𔻒肷��t���O

    private PlayerMove _playerMove;



    private void Awake()
    {
        _playerMove = GetComponent<PlayerMove>();
        
    }
    void Update()
    {
        // ���������ɔ��ˏ������s��
        if (_isFiring)
        {
            _coolTimeValue -= Time.deltaTime;
            if (_coolTimeValue <= 0f)
            {
                
                FireBullet();               // �����Ŕ���
                _coolTimeValue = _coolTime; // �N�[���_�E�������Z�b�g
            }
        }
    }


    private void FireBullet()
    {

        // �������Ă���̂Ō�ŃI�u�W�F�N�g�v�[���ɕύX
        // �e�𐶐����Ĕ���
        GameObject bulletObject = _bulletObjectPoolManager.GetBulletObject();
        bulletObject.transform.position = _firePostion.position;
        Vector2 force = new Vector2(_firePostion.position.x - transform.position.x, _firePostion.position.y - transform.position.y);
        force.Normalize();


        // ���x��^���� + �v���C���[�̃x�N�g�������Z  ���ƂłȂ���
        BulletComponents bulletComponents = BulletComponentsManager.Instance.GetComponents(bulletObject);

        bulletComponents.bulletMove.AddForce((force * _multiplyForce) + _playerMove._currentVelocity);
        

        // �v���C���[�ɔ�����^����
        _playerMove.AddRecoilForce(force);

    }



    /// <summary>
    /// InputSytem�̃N���b�N�C�x���g
    /// </summary>
    /// <param name="context"></param>
    public void OnFire(InputAction.CallbackContext context)
    {
        // ���˂̃g���K�[�𐧌�
        if (context.started)
        {
            _isFiring = true;
            _coolTimeValue = 0f; // �ŏ��̒e�͂����ɔ��˂���
        }
        else if (context.canceled)
        {
            _isFiring = false; // �������������ɔ��˂��~����
        }
    }

    
   


}
