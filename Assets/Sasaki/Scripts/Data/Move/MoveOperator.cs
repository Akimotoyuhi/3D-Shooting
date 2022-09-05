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
    public Vector3 Move(Transform t)
    {
        Vector2 dir = SetDir() * _moveData.ShakeSize;

        Vector3 forward = SetToward(t) * _moveData.Speed;
        Vector3 right = t.right * dir.x;
        
        return new Vector3(forward.x + right.x, forward.y + dir.y, forward.z + right.z);
    }

    Vector3 SetToward(Transform t)
    {
        Vector3 dir;

        if (_moveData.MoveType == MoveType.ToPlayer)
        {
            Player player = GameManager.Instance.CurrentPlayer;

            if (player == null)
            {
                dir = t.forward;
                Debug.LogWarning($"Player��񂪎擾�ł��܂���ł����B�Ώ�Object => {gameObject.name}.");
            }
            else
            {
                dir = player.transform.position - t.position;
            }
        }
        else
        {
            dir = t.forward;
        }

        return dir.normalized;
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
