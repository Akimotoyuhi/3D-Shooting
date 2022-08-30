using UnityEngine;

/// <summary>
/// �s���f�[�^�̕␳
/// </summary>
public class MoveOperator : MonoBehaviour
{
    [SerializeField] MoveData _moveData;

    float _shakeTimer;
    bool _isRequest;

    void Update()
    {
        if (!_isRequest)
        {
            return;
        }

        _shakeTimer += Time.deltaTime * _moveData.LoopSpeed;
    }

    /// <summary>
    /// �␳���ꂽ�f�[�^��Ԃ�
    /// </summary>
    /// <param name="t">�g�p��</param>
    /// <returns>Velocity</returns>
    public Vector3 MoveCollect(Transform t)
    {
        Vector3 forward = Rotate(t) * _moveData.Speed;
        Vector2 dir = SetDir() * _moveData.ShakeSize;
        
        return new Vector3(forward.x + dir.x, forward.y + dir.y, forward.z);
    }

    Vector3 Rotate(Transform t)
    {
        Vector3 dir;

        if (_moveData.MoveType == MoveType.ToPlayer)
        {
            // tbd Player��z��
            dir = t.forward;
        }
        else
        {
            dir = t.forward;
        }

        return dir;
    }

    Vector2 SetDir()
    {
        float sin = Mathf.Sin(_shakeTimer) * _moveData.Shake.x;
        float cos = Mathf.Cos(_shakeTimer) * _moveData.Shake.y;

        return new Vector2(sin, cos);
    }

    /// <summary>
    /// ������
    /// </summary>
    public void Initalize()
    {
        _shakeTimer = 0;
        _isRequest = false;
    }

    /// <summary>
    /// �s���f�[�^�̕␳���N�G�X�g�BTrue�̏ꍇ�␳���s��
    /// </summary>
    /// <param name="isRequest">Request</param>
    public void OprationRequest(bool isRequest)
    {
        _isRequest = isRequest;
    }
}
