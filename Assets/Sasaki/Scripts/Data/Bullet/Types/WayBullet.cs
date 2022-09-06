using UnityEngine;

/// <summary>
/// 拡散弾のデータ
/// </summary>
public class WayBullet : IBulletData
{
    [SerializeField] int _wayCount = 2;
    [SerializeField] float _angle = 0;

    const int DefaultWayCount = 2;

    public BulletType Type => BulletType.Way;

    public BulletParam SetupData()
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

    public BulletParam SendData()
    {
        throw new System.NotImplementedException();
    }

    public Vector3 SetNormalizeDir(FieldStateHelper.State state, Transform user)
    {
        throw new System.NotImplementedException();
    }

    public void Initalize()
    {
        throw new System.NotImplementedException();
    }
}
