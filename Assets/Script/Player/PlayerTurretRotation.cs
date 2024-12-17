using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerTurretRotation : MonoBehaviour
{
    [SerializeField] private Transform _rotateTurret;
    [SerializeField] private Transform _debugSquare;

    // Update is called once per frame
    private void Update()
    {
        // マウスの座標を取得、ワールド座標に変換、デバッグ用の四角の座標に設定
        Vector3 screenPositon = new Vector3(Mouse.current.position.ReadValue().x, Mouse.current.position.ReadValue().y, 0);
        Vector3 worldPostion = Camera.main.ScreenToWorldPoint(screenPositon);
        _debugSquare.position = new Vector3(worldPostion.x, worldPostion.y, 0);

        //タレットをマウスの座標に向ける
        _rotateTurret.LookAt2D(_debugSquare);

    }


}
