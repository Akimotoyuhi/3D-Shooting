using UnityEngine;

public enum MoveType
{
    Forward,
    ToPlayer,
}

/// <summary>
/// Enemy�̍s���f�[�^class
/// </summary>

[System.Serializable]
public class MoveData
{
    [SerializeField] MoveType _moveType;
    [SerializeField] float _speed;
    [SerializeField] ShakeCondition _shakeCondition;

    /// <summary>
    /// �h�ꕝ�̃f�[�^�N���X
    /// </summary>
    [System.Serializable]
    class ShakeCondition
    {
        [SerializeField, Range(0, 1)] float _horizontal = 0;
        [SerializeField, Range(0, 1)] float _verticle = 0;

        [SerializeField] float _horizontalSpeed;
        [SerializeField] float _verticleSpeed;

        [SerializeField] float _size = 1;
        [SerializeField] float _loopSpeed = 1;

        public float Horizontal => _horizontal;
        public float Verticle => _verticle;
        public float HrizontalSpeed => _horizontalSpeed;
        public float VerticleSpeed => _verticleSpeed;
        public float Size => _size;
        public float LoopSpeed => _loopSpeed;
    }

    /// <summary>
    /// �s���^�C�v
    /// </summary>
    public MoveType MoveType => _moveType;

    /// <summary>
    /// Enemy�̃X�s�[�h
    /// </summary>
    public float Speed => _speed;

    /// <summary>
    /// �U��
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
    /// �U���̑傫��
    /// </summary>
    public float ShakeSize => _shakeCondition.Size;

    public float HorizontalSpeed => _shakeCondition.HrizontalSpeed;
    public float VerticleSpeed => _shakeCondition.VerticleSpeed;
    /// <summary>
    /// �U���̑���
    /// </summary>
    public float LoopSpeed => _shakeCondition.LoopSpeed;
}
