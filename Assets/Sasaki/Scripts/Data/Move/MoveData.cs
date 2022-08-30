using UnityEngine;

public enum MoveType
{
    Forward,
    ToPlayer,
}

/// <summary>
/// Enemyの行動データclass
/// </summary>

[System.Serializable]
public class MoveData
{
    [SerializeField] MoveType _moveType;
    [SerializeField] float _speed;
    [SerializeField] ShakeCondition _shakeCondition;

    /// <summary>
    /// 揺れ幅のデータクラス
    /// </summary>
    [System.Serializable]
    class ShakeCondition
    {
        [SerializeField, Range(0, 1)] float _horizontal = 0;
        [SerializeField, Range(0, 1)] float _verticle = 0;

        [SerializeField] float _size = 1;
        [SerializeField] float _loopSpeed = 1;

        public float Horizontal => _horizontal;
        public float Verticle => _verticle;
        public float Size => _size;
        public float LoopSpeed => _loopSpeed;
    }

    /// <summary>
    /// 行動タイプ
    /// </summary>
    public MoveType MoveType => _moveType;

    /// <summary>
    /// Enemyのスピード
    /// </summary>
    public float Speed => _speed;

    /// <summary>
    /// 振れ具合
    /// </summary>
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

    /// <summary>
    /// 振れ具合の大きさ
    /// </summary>
    public float ShakeSize => _shakeCondition.Size;

    /// <summary>
    /// 振れ具合の速さ
    /// </summary>
    public float LoopSpeed => _shakeCondition.LoopSpeed;
}
