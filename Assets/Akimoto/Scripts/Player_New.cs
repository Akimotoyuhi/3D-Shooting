using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Cysharp.Threading.Tasks;
using DG.Tweening;

public class Player_New : CharaBase, IDamageble
{
    /// <summary>攻撃間隔</summary>
    [SerializeField] float _attackInterval;
    /// <summary>フィールドの状態<br/>後にフィールド管理クラスに移動予定</summary>
    [SerializeField] FieldState _fieldState;
    [SerializeField] BulletOperator _bulletOperator;
    /// <summary>攻撃ボタンの入力受け取り用</summary>
    private ReactiveProperty<bool> _inputAttackButton = new ReactiveProperty<bool>();

    protected override void Setup()
    {
        //攻撃ボタンの入力通知を監視
        _inputAttackButton
            .Where(x => x)
            .ThrottleFirst(System.TimeSpan.FromSeconds(_attackInterval))
            .Subscribe(_ => Attack())
            .AddTo(this);

        if (GameManager.Instance != null)
        {
            //FieldStateを監視
            GameManager.Instance.FieldStateObservable
                .Subscribe(s => _fieldState = s)
                .AddTo(this);
        }

        _bulletOperator.IsAuto(false);
    }

    private void Update()
    {
        Move();

        //攻撃ボタンの入力受け取り
        _inputAttackButton.SetValueAndForceNotify(Input.GetButton("Fire1"));
    }

    /// <summary>
    /// 移動
    /// </summary>
    private void Move()
    {
        Vector3 v = Vector3.zero;
        //フィールドの状態に応じて移動方向を変える
        switch (_fieldState)
        {
            case FieldState.Up:
                v = new Vector3(
                    Input.GetAxisRaw("Horizontal"),
                    0,
                    Input.GetAxisRaw("Vertical"));
                break;
            case FieldState.Down:
                v = new Vector3(
                    Input.GetAxisRaw("Horizontal"),
                    0,
                    -Input.GetAxisRaw("Vertical"));
                break;
            case FieldState.Right:
                v = new Vector3(
                    0,
                    Input.GetAxisRaw("Vertical"),
                    Input.GetAxisRaw("Horizontal"));
                break;
            case FieldState.Left:
                v = new Vector3(
                    0,
                    Input.GetAxisRaw("Vertical"),
                    -Input.GetAxisRaw("Horizontal"));
                break;
            case FieldState.Forward:
                v = new Vector3(
                    -Input.GetAxisRaw("Horizontal"),
                    Input.GetAxisRaw("Vertical"),
                    0);
                break;
            case FieldState.Behind:
                v = new Vector3(
                    Input.GetAxisRaw("Horizontal"),
                    Input.GetAxisRaw("Vertical"),
                    0);
                break;
            default:
                break;
        }

        Rigidbody.velocity = v * UserData.Speed;
    }

    /// <summary>
    /// 攻撃
    /// </summary>
    private void Attack()
    {
        _bulletOperator.ShotRequest();
    }

    protected override void DeadEvent()
    {
        // tbd
    }

    public void GetDamage(int damage)
    {
        int hp = UserData.CurrentHP - damage;
        UserData.UpdateHP(hp);
    }
}