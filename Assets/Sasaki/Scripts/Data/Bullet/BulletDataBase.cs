using UnityEngine;

public interface IBulletData
{
    /// <summary>
    /// 弾のタイプ
    /// </summary>
    BulletType Type { get; }

    /// <summary>
    /// 設定されたBulletParamのデータを伝える。
    /// </summary>
    /// <returns></returns>
    BulletParam SendData();

    /// <summary>
    /// WayCount分呼び出される
    /// </summary>
    /// <param name="state"></param>
    /// <param name="user"></param>
    /// <returns></returns>
    Vector3 SetNormalizeDir(FieldStateHelper.State state, Transform user);

    /// <summary>
    /// 初期化
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
/// 弾のデータベースクラス
/// </summary>
[CreateAssetMenu(fileName = "BulletData")]
public class BulletDataBase : ScriptableObject
{
    [SerializeField] BulletData _bulletData;

    public BulletData BulletData => _bulletData;
}
