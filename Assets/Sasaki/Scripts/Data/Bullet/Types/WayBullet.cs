using UnityEngine;

/// <summary>
/// 拡散弾のデータ
/// </summary>
public class WayBullet : IBulletData
{
    [SerializeField] int _wayCount = 2;
    [SerializeField] float _angle = 0;

    int _counter = 0;

    const int DefaultWayCount = 2;

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

        _counter = 0;
        param.WayCount = _wayCount;

        return param;
    }

    public Vector3 SetNormalizeDir(FieldStateHelper.State state, Transform user)
    {
        Vector3 dir = Vector3.zero;

        float angleRange = Mathf.PI * (_angle / 180);
        float rad = (angleRange / (_wayCount - 1)) * _counter + 0.5f * (Mathf.PI - angleRange);
        
        switch (state)
        {
            case FieldStateHelper.State.TopView: dir = new Vector3(Mathf.Cos(rad), 0, Mathf.Sin(rad));
                break;
            case FieldStateHelper.State.SideView: dir = new Vector3(0, Mathf.Cos(rad), Mathf.Sin(rad));
                break;
            case FieldStateHelper.State.BackView: dir = new Vector3(Mathf.Cos(rad), Mathf.Sin(rad), 0);
                break;
        }

        _counter++;

        return dir;
    }

    public void Initalize()
    {
        _counter = 0;
    }
}
