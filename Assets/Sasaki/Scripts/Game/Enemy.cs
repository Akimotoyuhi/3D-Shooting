using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] MoveOperator _moveOperator;

    Rigidbody _rb;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();

        _moveOperator.Initalize();
        _moveOperator.OprationRequest(true);
    }

    void Update()
    {
        Vector3 move = _moveOperator.MoveCollect(transform);
        _rb.velocity = move;
    }
}
