using NaughtyAttributes;
using UnityEngine;


public class BulletCollisionEvent : BaseCollisionEvent
{
    [Header("�U����")]
    [SerializeField] private int _attackPower = 10;

    [SerializeField, Tag] private string _enemyTag;
    [SerializeField, Tag] private string _debrisTag;

    private BulletMove _bulletMove;

    protected override void Awake()
    {
        base.Awake();
        _bulletMove = GetComponent<BulletMove>();
    }
    public override void OnCustomCollisionEnter(SimpleShapeCollider2D collision)
    {
        if (collision.gameObject.CompareTag(_debrisTag)){

            // HP�e�X�g�p�Ȃ̂Ō�ŕύX �f�u���̔��������͕K�v�Ȃ�
            DebrisComponents debrisComponents = DebrisComponentsManager.Instance.GetComponents(collision.gameObject);

            debrisComponents.debrisHP.DecreaseHP(10);
        }
        else if (collision.gameObject.CompareTag(_enemyTag))
        {
            EnemyComponents enemyComponents = EnemyComponentsManager.Instance.GetComponents(collision.gameObject);

            enemyComponents.enemyHealth.DamegedByEnemy(_attackPower);
            Debug.Log("�e���G�ɂ���������I�I".Warning());
        }
    }
    public override void OnCustomCollisionStay(SimpleShapeCollider2D collision)
    {
        // Implement your logic here
    }

    public override void OnCustomCollisionExit(SimpleShapeCollider2D collision)
    {
        // Implement your logic here
    }
}
