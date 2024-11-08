using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerFire : MonoBehaviour
{
    public Transform _firePostion;
    public BulletMove _bulletMoveObject;
    public Transform _bulletParent;

    public float _coolTime = 0.5f; // 弾発射のクールダウンタイム
    public float _multiplyForce = 1f;

    public PlayerMove _playerMove;
    private float _coolTimeValue;
    private bool _isFiring = false; // 長押しを判定するフラグ

    void Update()
    {
        // 長押し中に発射処理を行う
        if (_isFiring)
        {
            _coolTimeValue -= Time.deltaTime;
            if (_coolTimeValue <= 0f)
            {
                FireBullet();
                _coolTimeValue = _coolTime; // クールダウンをリセット
            }
        }
    }

    public void Onfire(InputAction.CallbackContext context)
    {
        // 発射のトリガーを制御
        if (context.started)
        {
            _isFiring = true;
            _coolTimeValue = 0f; // 最初の弾はすぐに発射する
        }
        else if (context.canceled)
        {
            _isFiring = false; // 長押し解除時に発射を停止する
        }
    }

    private void FireBullet()
    {

        // 生成しているので後でオブジェクトプールに変更
        // 弾を生成して発射
        var bulletObject = Instantiate(_bulletMoveObject, _firePostion.position, Quaternion.identity, _bulletParent);
        Vector2 force = new Vector2(_firePostion.position.x - transform.position.x, _firePostion.position.y - transform.position.y);

        // 速度を与える + プレイヤーのベクトルも加算  あとでなおす
        bulletObject.GetComponent<BulletMove>().AddForce((force * _multiplyForce) + _playerMove._currentVelocity);

        // プレイヤーに反動を与える
        _playerMove.AddRecoilForce(force);

    }
}
