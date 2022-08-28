using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Cysharp.Threading.Tasks;
using DG.Tweening;

public class Player : MonoBehaviour
{
    [SerializeField] int _power;
    [SerializeField] float _attackInterval;
    [SerializeField] float _moveSpeed;
    [SerializeField] FieldState _fieldState;
    [SerializeField] Rigidbody _rb;

    private void Start()
    {
    }

    private void Update()
    {
        Move();
        Attack();
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
                Input.GetAxis("Horizontal"),
                0,
                Input.GetAxis("Vertical"));
        }
        else if (_fieldState == FieldState.Right || _fieldState == FieldState.Left)
        {
            v = new Vector3(
                0,
                Input.GetAxis("Vertical"),
                Input.GetAxis("Horizontal"));
        }
        else if (_fieldState == FieldState.Forward || _fieldState == FieldState.Behind)
        {
            v = new Vector3(
                Input.GetAxis("Horizontal"),
                Input.GetAxis("Vertical"),
                0);
        }

        _rb.velocity = v * _moveSpeed;
    }

    /// <summary>
    /// 攻撃
    /// </summary>
    private void Attack()
    {
        if (Input.GetButton("Fire1"))
            Debug.Log("攻撃");
    }
}

public enum FieldState
{
    Up,
    Down,
    Right,
    Left,
    Forward,
    Behind,
}