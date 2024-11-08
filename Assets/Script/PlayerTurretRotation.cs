using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerTurretRotation : MonoBehaviour
{
    public Transform _rotateTurret;
    public Transform _debugSquare;
    private Vector2 _hoge;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        Vector3 screenPositon = new Vector3(Mouse.current.position.ReadValue().x, Mouse.current.position.ReadValue().y, 0);
        Vector3 worldPostion = Camera.main.ScreenToWorldPoint(screenPositon);
        _debugSquare.position = new Vector3(worldPostion.x, worldPostion.y, 0);


        _rotateTurret.LookAt2D(_debugSquare);

    }


}
