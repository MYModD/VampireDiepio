using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerTurretRotation : MonoBehaviour
{
    [SerializeField] private Transform _rotateTurret;
    [SerializeField] private Transform _debugSquare;

    // Update is called once per frame
    private void Update()
    {
        // �}�E�X�̍��W���擾�A���[���h���W�ɕϊ��A�f�o�b�O�p�̎l�p�̍��W�ɐݒ�
        Vector3 screenPositon = new Vector3(Mouse.current.position.ReadValue().x, Mouse.current.position.ReadValue().y, 0);
        Vector3 worldPostion = Camera.main.ScreenToWorldPoint(screenPositon);
        _debugSquare.position = new Vector3(worldPostion.x, worldPostion.y, 0);

        //�^���b�g���}�E�X�̍��W�Ɍ�����
        _rotateTurret.LookAt2D(_debugSquare);

    }


}
