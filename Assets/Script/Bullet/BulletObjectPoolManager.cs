using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BulletObjectPoolManager : ObjectPoolBase<PoolableBullet>
{
    [SerializeField] private PlayerMove _playerMove;
    public void FirePlayerBullet(Vector3 firePostion , Vector3 playerPostion, float multiplyForce)
    {
        PoolableBullet bulletObject = ObjectPool.Get();
        bulletObject.transform.position = firePostion;
        Vector2 force = new Vector2(firePostion.x - playerPostion.x, firePostion.y - playerPostion.y);
        force.Normalize();

        // ���x��^���� + �v���C���[�̃x�N�g�������Z  ���ƂłȂ���
        bulletObject.GetComponent<BulletMove>().AddForce((force * multiplyForce) + _playerMove._currentVelocity);

        // �v���C���[�ɔ�����^����
        _playerMove.AddRecoilForce(force);
    }

}
