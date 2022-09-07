using UnityEngine;

/// <summary>
/// 弾のデータクラス
/// </summary>
[System.Serializable]
public class BulletData
{
    [SerializeField] float _speed;
    [SerializeField, Range(-1, 1)] float _curveVal = 0;
    [SerializeField] float _curveSpeed;
    [SerializeReference, SubclassSelector] IBulletData _bulletData;
    
    public float Speed => _speed;
    public float CurveVal => _curveVal;
    public float CurveSpeed => _curveSpeed;
    public IBulletData IBulletData => _bulletData; 
}
