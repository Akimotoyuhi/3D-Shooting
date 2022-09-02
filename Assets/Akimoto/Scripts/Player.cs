using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Cysharp.Threading.Tasks;
using DG.Tweening;

public class Player : MonoBehaviour
{
    /// <summary>�U����</summary>
    [SerializeField] int _power;
    /// <summary>�U���Ԋu</summary>
    [SerializeField] float _attackInterval;
    /// <summary>�ړ����x</summary>
    [SerializeField] float _moveSpeed;
    /// <summary>�e�̑��x</summary>
    [SerializeField] float _balletSpeed;
    /// <summary>�t�B�[���h�̏��<br/>��Ƀt�B�[���h�Ǘ��N���X�Ɉړ��\��</summary>
    [SerializeField] FieldState _fieldState;
    [SerializeField] Rigidbody _rb;
    /// <summary>�e�̃v���n�u<br/>��ɕς���</summary>
    [SerializeField] Rigidbody _balletPrefab;
    /// <summary>�U���{�^���̓��͎󂯎��p</summary>
    private ReactiveProperty<bool> _inputAttackButton = new ReactiveProperty<bool>();

    public void Setup()
    {
        //�U���{�^���̓��͒ʒm���Ď�
        _inputAttackButton
            .Where(x => x)
            .ThrottleFirst(System.TimeSpan.FromSeconds(_attackInterval))
            .Subscribe(_ => Attack())
            .AddTo(this);

        //FieldState���Ď�
        GameManager.Instance.FieldStateObservable
            .Subscribe(s => _fieldState = s)
            .AddTo(this);
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

        _rb.velocity = v * _moveSpeed;
    }

    /// <summary>
    /// �U��
    /// </summary>
    private void Attack()
    {
        Rigidbody b = Instantiate(_balletPrefab, transform.position, Quaternion.identity);
        b.velocity = transform.forward * _balletSpeed;
    }
}