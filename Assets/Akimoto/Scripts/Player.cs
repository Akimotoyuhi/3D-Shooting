using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Cysharp.Threading.Tasks;
using DG.Tweening;

public class Player : MonoBehaviour
{
    /// <summary>攻撃力</summary>
    [SerializeField] int _power;
    /// <summary>攻撃間隔</summary>
    [SerializeField] float _attackInterval;
    /// <summary>移動速度</summary>
    [SerializeField] float _moveSpeed;
    /// <summary>弾の速度</summary>
    [SerializeField] float _balletSpeed;
    /// <summary>フィールドの状態<br/>後にフィールド管理クラスに移動予定</summary>
    [SerializeField] FieldState _fieldState;
    [SerializeField] Rigidbody _rb;
    /// <summary>弾のプレハブ<br/>後に変える</summary>
    [SerializeField] Rigidbody _balletPrefab;
    /// <summary>攻撃ボタンの入力受け取り用</summary>
    private ReactiveProperty<bool> _inputAttackButton = new ReactiveProperty<bool>();
    /// <summary>FieldStateの設定</summary>
    public FieldState SetFieldState => _fieldState;

    private void Start()
    {
        _inputAttackButton
            .Where(x => x)
            .ThrottleFirst(System.TimeSpan.FromSeconds(_attackInterval))
            .Subscribe(_ => Attack())
            .AddTo(this);
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
        if (_fieldState == FieldState.Up || _fieldState == FieldState.Down)
        {
            v = new Vector3(
                Input.GetAxisRaw("Horizontal"),
                0,
                Input.GetAxisRaw("Vertical"));
        }
        else if (_fieldState == FieldState.Right || _fieldState == FieldState.Left)
        {
            v = new Vector3(
                0,
                Input.GetAxisRaw("Vertical"),
                Input.GetAxisRaw("Horizontal"));
        }
        else if (_fieldState == FieldState.Forward || _fieldState == FieldState.Behind)
        {
            v = new Vector3(
                Input.GetAxisRaw("Horizontal"),
                Input.GetAxisRaw("Vertical"),
                0);
        }

        _rb.velocity = v * _moveSpeed;
    }

    /// <summary>
    /// 攻撃
    /// </summary>
    private void Attack()
    {
        Rigidbody b = Instantiate(_balletPrefab, transform.position, Quaternion.identity);
        b.velocity = transform.forward * _balletSpeed;
    }
}