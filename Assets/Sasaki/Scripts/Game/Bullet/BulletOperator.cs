using System.Collections.Generic;
using UnityEngine;
using UniRx;
using ObjectPool;

/// <summary>
/// 弾データの補正
/// </summary>
public class BulletOperator : MonoBehaviour
{
    [SerializeField] Bullet _bulletPrefab;
    [SerializeField] bool _isHideFlag;
    [SerializeField] int _createCount = 5;
    [SerializeField] float _intervalTime;
    [SerializeField] List<BulletDataBase> _bulletDataList;

    float _timer;
    bool _isAuto;

    FieldState _currentFieldState;

    Pool<Bullet> _bulletPool = new Pool<Bullet>();

    void Awake()
    {
        _bulletPool
            .SetMono(_bulletPrefab, _createCount)
            .IsSetParent(transform)
            .IsAutoActive();

        if (_isHideFlag)
        {
            _bulletPool.SetHideFlags();
        }
           
        _bulletPool.CreateRequest();
    }

    void Start()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.FieldStateObservable
            .Subscribe(s => _currentFieldState = s)
            .AddTo(this);
        }
    }

    void Update()
    {
        if (!_isAuto)
        {
            return;
        }

        _timer += Time.deltaTime;

        if (_timer > _intervalTime)
        {
            _timer = 0;
            ShotRequest();
        }
    }

    /// <summary>
    /// 弾を飛ばす際のリクエスト
    /// </summary>
    public void ShotRequest()
    {
        foreach (BulletDataBase dataBase in _bulletDataList)
        {
            BulletData data = dataBase.BulletData;
            BulletParam param = data.IBulletData.SendData();

            for (int index = 0; index < param.WayCount; index++)
            {
                System.Action action;
                Bullet bullet = _bulletPool.UseRequest(out action);

                FieldStateHelper.State state = FieldStateHelper.CollectState(_currentFieldState);
                Vector3 dir = data.IBulletData.SetNormalizeDir(state, transform).normalized;

                bullet.SetData(data.Speed * dir, data.CurveVal, state);

                action.Invoke();
            }
        }
    }

    /// <summary>
    /// 弾を自動で飛ばす場合に呼び出す
    /// </summary>
    /// <param name="isAuto"></param>
    public void IsAuto(bool isAuto)
    {
        _isAuto = isAuto;
    }
}
