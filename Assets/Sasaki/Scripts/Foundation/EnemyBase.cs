using UnityEngine;

/// <summary>
/// Enemy‚ÌŠî’êƒNƒ‰ƒX
/// </summary>

[RequireComponent(typeof(MoveOperator))]
[RequireComponent(typeof(BulletOperator))]
public abstract class EnemyBase : CharaBase
{
    MoveOperator _moveOperator;
    BulletOperator _bulletOperator;

    readonly string PlayerLayer = "Player"; 

    protected MoveOperator MoveOperator => _moveOperator;
    protected BulletOperator BulletOperator => _bulletOperator;

    protected override void Setup()
    {
        _moveOperator = GetComponent<MoveOperator>();
        _bulletOperator = GetComponent<BulletOperator>();

        _moveOperator.Initalize();
        _moveOperator.SetSpeed(UserData.Speed);
        _moveOperator.OprationRequest(true);

        _bulletOperator.IsAuto(true);

        SetRotate();
    }

    void SetRotate()
    {
        if (GameManager.Instance != null)
        {
            Vector3 forward = GameManager.Instance.CurrentPlayer.transform.forward;
            transform.rotation = Quaternion.LookRotation(forward * -1);
        }
    }

    protected override void DeadEvent()
    {
        _moveOperator.OprationRequest(false);
    }

    protected virtual void OnCollisionEvent(Collision collision)
    {
        string layer = LayerMask.LayerToName(collision.gameObject.layer);
        if (layer == PlayerLayer)
        {
            IDamageble damageble = collision.gameObject.GetComponent<IDamageble>();
            damageble.GetDamage(UserData.Power);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        OnCollisionEvent(collision);
    }
}
