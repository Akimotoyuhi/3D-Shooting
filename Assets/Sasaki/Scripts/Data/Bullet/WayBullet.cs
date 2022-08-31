using UnityEngine;

public class WayBullet : IBulletData
{
    [SerializeField] int _wayCount;
    [SerializeField] float _angle;

    public BulletType Type => BulletType.Way;

    public BulletParam SendData()
    {
        BulletParam param = new BulletParam();

        param.WayCount = _wayCount;
        param.Angle = _angle;

        return param;
    }
}
