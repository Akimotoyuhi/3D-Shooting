using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MoveData
{
    [SerializeField] float _speed;
    [SerializeField] ShakeCondition _shakeCondition;

    [System.Serializable]
    class ShakeCondition
    {
        [SerializeField, Range(-1, 1)] float _horizontal = 0;
        [SerializeField, Range(-1, 1)] float _verticle = 0;

        [SerializeField] float _size = 1;
        [SerializeField] float _loopSpeed = 1;

        public float Horizontal => _horizontal;
        public float Verticle => _verticle;
        public float Size => _size;
        public float LoopSpeed => _loopSpeed;
    }

    public float Speed => _speed;

    public Vector2 Shake
    {
        get
        {
            float h = _shakeCondition.Horizontal;
            float v = _shakeCondition.Verticle;

            Vector2 vec = new Vector3(h, v);

            return vec;
        }
    }

    public float LoopSpeed => _shakeCondition.LoopSpeed;
}
