using UnityEngine;

public interface IBulletData
{
    BulletType Type { get; }
    BulletParam SendData();
}

public enum BulletType
{
    Forward,
    Way,
    Circle,
}

[CreateAssetMenu(fileName = "BulletData")]
public class BulletDataBase : ScriptableObject
{
    [SerializeField] BulletData _bulletData;

    public BulletData BulletData => _bulletData;
}
