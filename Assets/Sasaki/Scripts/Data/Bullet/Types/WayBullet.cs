using UnityEngine;

/// <summary>
/// �g�U�e�̃f�[�^
/// </summary>
public class WayBullet : IBulletData
{
    [SerializeField] int _wayCount = 2;
    [SerializeField] float _angle = 0;

    const int DefaultWayCount = 2;

    public BulletType Type => BulletType.Way;

    public BulletParam SendData()
    {
        BulletParam param = new BulletParam();

        if (_wayCount <= 1)
        {
            param.WayCount = DefaultWayCount;
            Debug.LogWarning($"�ݒ�f�[�^�␳�����܂����BWayCount. Before{_wayCount} => After{DefaultWayCount}");
        }
        else
        {
            param.WayCount = _wayCount;
        }
        
        param.Angle = _angle;

        return param;
    }
}
