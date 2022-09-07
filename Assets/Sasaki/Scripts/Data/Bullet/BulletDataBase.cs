using UnityEngine;

public interface IBulletData
{
    /// <summary>
    /// �e�̃^�C�v
    /// </summary>
    BulletType Type { get; }

    /// <summary>
    /// �ݒ肳�ꂽBulletParam�̃f�[�^��`����B
    /// </summary>
    /// <returns></returns>
    BulletParam SendData();

    /// <summary>
    /// WayCount���Ăяo�����
    /// </summary>
    /// <param name="state"></param>
    /// <param name="user"></param>
    /// <returns></returns>
    Vector3 SetNormalizeDir(FieldStateHelper.State state, Transform user);

    /// <summary>
    /// ������
    /// </summary>
    void Initalize();
}

public enum BulletType
{
    Forward,
    Way,
    Circle,
}

/// <summary>
/// �e�̃f�[�^�x�[�X�N���X
/// </summary>
[CreateAssetMenu(fileName = "BulletData")]
public class BulletDataBase : ScriptableObject
{
    [SerializeField] BulletData _bulletData;

    public BulletData BulletData => _bulletData;
}
