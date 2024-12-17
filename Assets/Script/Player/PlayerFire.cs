using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerFire : MonoBehaviour
{

    [SerializeField, Header("攻撃力")] private int _attackPower = 10;

    [SerializeField,Header("クールタイム")] private float _coolTime = 0.5f; 
    [SerializeField,Header("反動の強さ")] private float _multiplyForce = 1f;

    [SerializeField] private Transform _firePostion;
    [SerializeField] private Transform _bulletParent;

    [SerializeField] private BulletObjectPoolManager _bulletObjectPoolManager;

    private float _coolTimeValue;   //クールタイム計算するための変数
    private bool _isFiring = false; // 長押しを判定するフラグ

    private PlayerMove _playerMove;



    private void Awake()
    {
        _playerMove = GetComponent<PlayerMove>();
        
    }
    void Update()
    {
        // 長押し中に発射処理を行う
        if (_isFiring)
        {
            _coolTimeValue -= Time.deltaTime;
            if (_coolTimeValue <= 0f)
            {
                
                FireBullet();               // ここで発射
                _coolTimeValue = _coolTime; // クールダウンをリセット
            }
        }
    }


    /// <summary>
    /// InputSytemのクリックイベント
    /// </summary>
    /// <param name="context"></param>
    public void OnFire(InputAction.CallbackContext context)
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

        
        // 弾を生成して発射
        GameObject bulletObject = _bulletObjectPoolManager.GetBulletObject();
        bulletObject.transform.position = _firePostion.position;

        // 方向を求めた後に正規化
        Vector2 force = new Vector2(_firePostion.position.x - transform.position.x, _firePostion.position.y - transform.position.y);
        force.Normalize();


        // 速度を与える + プレイヤーのベクトルも加算  あとでなおす
        BulletComponents bulletComponents = BulletComponentsManager.Instance.GetComponents(bulletObject);

        bulletComponents.bulletMove.AddForce((force * _multiplyForce) + _playerMove._currentVelocity);
        bulletComponents.bulletCollisionEvent.ChengeBulletAttackPower(_attackPower);

        // プレイヤーに反動を与える
        _playerMove.AddRecoilForce(force);

    }



    
   


}
