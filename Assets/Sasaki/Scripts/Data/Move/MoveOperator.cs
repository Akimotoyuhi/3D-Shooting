using UnityEngine;

/// <summary>
/// 行動データの補正
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
    /// 補正されたデータを返す
    /// </summary>
    /// <param name="t">使用者</param>
    /// <returns>Velocity</returns>
    public Vector3 MoveCollect(Transform t)
    {
        Vector2 dir = SetDir() * _moveData.ShakeSize;

        Vector3 forward = Rotate(t) * _moveData.Speed;
        Vector3 right = t.right * dir.x;
        
        return new Vector3(forward.x + right.x, forward.y + dir.y, forward.z + right.z);
    }

    Vector3 Rotate(Transform t)
    {
        Vector3 dir;

        if (_moveData.MoveType == MoveType.ToPlayer)
        {
            // tbd Playerを想定
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
    /// 初期化
    /// </summary>
    public void Initalize()
    {
        _shakeTimer = 0;
        _isRequest = false;
    }

    /// <summary>
    /// 行動データの補正リクエスト。Trueの場合補正を行う
    /// </summary>
    /// <param name="isRequest">Request</param>
    public void OprationRequest(bool isRequest)
    {
        _isRequest = isRequest;
    }
}
