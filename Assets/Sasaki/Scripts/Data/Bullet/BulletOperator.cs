using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ObjectPool;

public class BulletOperator : MonoBehaviour
{
    [SerializeField] Bullet _bulletPrefab;
    [SerializeField] int _createCount = 5;
    [SerializeField] List<BulletDataBase> _bulletDataList;

    Pool<Bullet> _bulletPool = new Pool<Bullet>();

    void Awake()
    {
        _bulletPool
            .SetMono(_bulletPrefab, _createCount)
            .IsSetParent(transform)
            .IsAutoActive()
            .CreateRequest();
    }

    public void ShotRequest(int id = 0)
    {
        BulletData data = _bulletDataList[id].BulletData;

        SetData(data);
    }

    void SetData(BulletData data)
    {
        System.Action action;
        Bullet bullet = _bulletPool.UseRequest(out action);

        bullet.SetData(data);
        action.Invoke();
    }
}
