using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerFire : MonoBehaviour
{
    public Transform _firePostion;
    public BulletMove _bulletMoveObject;
    public Transform _bulletParent;

    public float _coolTime;
    public float _multiplyForce = 1;

    public PlayerMove _playerMove;
    private float _coolTimeValue;
   

    // Update is called once per frame
    public void Onfire(InputAction.CallbackContext context)
    {
        Debug.Log(context.phase);
        if (context.started)
        {
            var bulletObject = Instantiate(_bulletMoveObject, _firePostion.position, Quaternion.identity, _bulletParent);
            Vector2 force = new Vector2(_firePostion.position.x - this.transform.position.x, (_firePostion.position.y - this.transform.position.y)) ;
            // 速度与える + プレイヤーのベクトルも+
            bulletObject.GetComponent<BulletMove>().AddForce((force * _multiplyForce) + _playerMove._velocity);
            // プレイヤーの反動
            _playerMove.AddForce(force * -1);

        }
    }
}
