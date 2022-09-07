using UnityEngine;

/// <summary>
/// 弾のデータクラス
/// </summary>
[System.Serializable]
public class BulletData
{
    [SerializeField] float _speed;
    [SerializeField] CurveData _curveData;
    [SerializeReference, SubclassSelector] IBulletData _bulletData;
    
    [System.Serializable]
    public class CurveData
    {
        [SerializeField, Range(-1, 1)] float _curveVal = 0;
        [SerializeField] float _curveSpeed;

        public float Valume => _curveVal;
        public float Speed => _curveSpeed;
    }

    public float Speed => _speed;
    public float CurveVal => _curveData.Valume;
    public float CurveSpeed => _curveData.Speed;
    public IBulletData IBulletData => _bulletData; 
}
