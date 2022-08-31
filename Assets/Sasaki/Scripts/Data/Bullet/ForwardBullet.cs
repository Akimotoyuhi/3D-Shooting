using UnityEngine;

public class ForwardBullet : IBulletData
{
    [SerializeField] float _blur;
    public BulletType Type => BulletType.Forward;

    public BulletParam SendData()
    {
        BulletParam param = new BulletParam();

        param.WayCount = 1;
        param.Angle = 0;
        param.Blur = _blur;

        return param;
    }
}
