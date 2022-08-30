using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BulletData
{
    [SerializeField] MoveType _moveType;
    [SerializeField] float _speed;
    [SerializeField] int _wayCount = 1;
    [SerializeField, Range(0, 360)] float _angle;

    public MoveType MoveType => _moveType; 
    public float Speed => _speed;
}
