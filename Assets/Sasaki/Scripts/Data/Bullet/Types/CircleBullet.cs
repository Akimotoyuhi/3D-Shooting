using UnityEngine;

/// <summary>
/// 円形弾のデータ
/// </summary>
public class CircleBullet : IBulletData
{
    [SerializeField] int _wayCount;
    [SerializeField, Range(0, 360)] float _defaultAngle;

    float _angle;

    const int DefaultWayCount = 2;
    const float CircleAngle = 360;

    public BulletParam SendData()
    {
        BulletParam param = new BulletParam();

        if (_wayCount <= 1)
        {
            param.WayCount = DefaultWayCount;
            Debug.LogWarning($"設定データ補正をしました。WayCount. Before{_wayCount} => After{DefaultWayCount}");
        }
        else
        {
            param.WayCount = _wayCount;
        }

        return param;
    }

    public Vector3 SetNormalizeDir(FieldStateHelper.State state, Transform user)
    {
        float angle = CircleAngle / _wayCount;
        _angle += angle;

        float rad = (_angle + _defaultAngle) * Mathf.Deg2Rad;

        Vector3 dir = Vector3.zero;

        switch (state)
        {
            case FieldStateHelper.State.TopView: dir = new Vector3(Mathf.Sin(rad), 0, Mathf.Cos(rad));
                break;
            case FieldStateHelper.State.SideView: dir = new Vector3(0, Mathf.Sin(rad), Mathf.Cos(rad));
                break;
            case FieldStateHelper.State.BackView: dir = new Vector3(Mathf.Sin(rad), Mathf.Cos(rad), 0);
                break;
        }

        return dir;
    }

    public void Initalize()
    {
        _angle = 0;
    }   
}