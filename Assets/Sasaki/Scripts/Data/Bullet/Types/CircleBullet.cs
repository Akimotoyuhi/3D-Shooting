using UnityEngine;

/// <summary>
/// �~�`�e�̃f�[�^
/// </summary>
public class CircleBullet : IBulletData
{
    [SerializeField] int _wayCount;

    const int DefaultWayCount = 2;

    public BulletType Type => BulletType.Circle;

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

        return param;
    }
}
