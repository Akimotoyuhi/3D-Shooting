using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] MoveOperator _moveOperator;
    [SerializeField] BulletOperator _bulletOperator;

    Rigidbody _rb;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();

        _moveOperator.Initalize();
        _moveOperator.OprationRequest(true);

        _bulletOperator.IsAuto(true);
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            _bulletOperator.ShotRequest();
        }

        Vector3 move = _moveOperator.Move(transform);
        _rb.velocity = move;
    }
}
