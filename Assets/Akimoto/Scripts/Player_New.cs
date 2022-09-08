using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Cysharp.Threading.Tasks;
using DG.Tweening;

public class Player_New : CharaBase, IDamageble
{
    /// <summary>�U���Ԋu</summary>
    [SerializeField] float _attackInterval;
    /// <summary>�t�B�[���h�̏��<br/>��Ƀt�B�[���h�Ǘ��N���X�Ɉړ��\��</summary>
    [SerializeField] FieldState _fieldState;
    [SerializeField] BulletOperator _bulletOperator;
    /// <summary>�U���{�^���̓��͎󂯎��p</summary>
    private ReactiveProperty<bool> _inputAttackButton = new ReactiveProperty<bool>();

    protected override void Setup()
    {
        //�U���{�^���̓��͒ʒm���Ď�
        _inputAttackButton
            .Where(x => x)
            .ThrottleFirst(System.TimeSpan.FromSeconds(_attackInterval))
            .Subscribe(_ => Attack())
            .AddTo(this);

        if (GameManager.Instance != null)
        {
            //FieldState���Ď�
            GameManager.Instance.FieldStateObservable
                .Subscribe(s => _fieldState = s)
                .AddTo(this);
        }

        _bulletOperator.IsAuto(false);
    }

    private void Update()
    {
        Move();

        //�U���{�^���̓��͎󂯎��
        _inputAttackButton.SetValueAndForceNotify(Input.GetButton("Fire1"));
    }

    /// <summary>
    /// �ړ�
    /// </summary>
    private void Move()
    {
        Vector3 v = Vector3.zero;
        //�t�B�[���h�̏�Ԃɉ����Ĉړ�������ς���
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
    /// �U��
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