using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerFire : MonoBehaviour
{
    public Transform _firePostion;
    public Transform _bulletParent;

    public float _coolTime = 0.5f; // �e���˂̃N�[���_�E���^�C��
    public float _multiplyForce = 1f;

    private float _coolTimeValue;
    private bool _isFiring = false; // �������𔻒肷��t���O

    [SerializeField] private  PlayerMove _playerMove;
    [SerializeField] private BulletObjectPoolManager _bulletObjectPoolManager;


    void Update()
    {   
        // ���������ɔ��ˏ������s��
        if (_isFiring)
        {
            _coolTimeValue -= Time.deltaTime;
            if (_coolTimeValue <= 0f)
            {
                // �����Ŕ���
                _bulletObjectPoolManager.FirePlayerBullet(_firePostion.position,transform.position , _multiplyForce);
                _coolTimeValue = _coolTime; // �N�[���_�E�������Z�b�g
            }
        }
    }

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

    /*
    private void FireBullet()
    {

        // �������Ă���̂Ō�ŃI�u�W�F�N�g�v�[���ɕύX
        // �e�𐶐����Ĕ���
        // var bulletObject = _bulletObjectPoolManager.ObjectPool.Get(0);
        Vector2 force = new Vector2(_firePostion.position.x - transform.position.x, _firePostion.position.y - transform.position.y);
        force.Normalize();
        // ���x��^���� + �v���C���[�̃x�N�g�������Z  ���ƂłȂ���
        bulletObject.GetComponent<BulletMove>().AddForce((force * _multiplyForce) + _playerMove._currentVelocity);

        // �v���C���[�ɔ�����^����
        _playerMove.AddRecoilForce(force);

    }*/

    
}
