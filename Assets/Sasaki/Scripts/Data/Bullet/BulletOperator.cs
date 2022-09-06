using System.Collections.Generic;
using UnityEngine;
using UniRx;
using ObjectPool;

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

    public void ShotRequest(int id = 0)
    {
        BulletData data = _bulletDataList[id].BulletData;
        BulletParam param = data.IBulletData.SendData();

        for (int index = 0; index < param.WayCount; index++)
        {
            System.Action action;
            Bullet bullet = _bulletPool.UseRequest(out action);

            Vector3 dir = SetDir(data.IBulletData);
            SetBlur(param.Blur, ref dir);

            bullet.SetData(data.Speed * dir, data.CurveVal, data.CurveSpeed);

            action.Invoke();
        }
    }

    // tbd
    Vector3 SetDir(IBulletData data)
    {
        Vector3 dir = transform.forward;

        switch (data.Type)
        {
            case BulletType.Forward:
                dir = transform.forward;

                break;
            case BulletType.Way:


                break;
            case BulletType.Circle:


                break;
        }

        return dir.normalized;
    }

    // Note. ƒJƒƒ‰‚É‘Î‚·‚éƒuƒŒ‚ð‘z’è
    void SetBlur(float blur, ref Vector3 dir)
    {
        // tbd.

        switch (_currentFieldState)
        {
            case FieldState.Up:
                break;
            case FieldState.Down:
                break;
            case FieldState.Right:
                break;
            case FieldState.Left:
                break;
            case FieldState.Forward:
                break;
            case FieldState.Behind:
                break;
        }
    }

    public void IsAuto(bool isAuto)
    {
        _isAuto = isAuto;
    }
}
