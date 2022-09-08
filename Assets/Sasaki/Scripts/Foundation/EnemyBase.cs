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

    protected MoveOperator MoveOperator => _moveOperator;
    protected BulletOperator BulletOperator => _bulletOperator;

    protected override void Setup()
    {
        _moveOperator = GetComponent<MoveOperator>();
        _bulletOperator = GetComponent<BulletOperator>();

        _moveOperator.Initalize();
        _moveOperator.OprationRequest(true);

        _bulletOperator.IsAuto(true);

        SetRotate();
    }

    void SetRotate()
    {
        
    }

    protected override void DeadEvent()
    {
        _moveOperator.OprationRequest(false);
    }
}
