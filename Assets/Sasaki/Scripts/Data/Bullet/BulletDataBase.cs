using UnityEngine;

public interface IBulletData
{
    BulletType Type { get; }
    BulletParam SendData();
    Vector3 SetDir(FieldStateHelper.ViewState state, Transform user);
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
