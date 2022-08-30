using UnityEngine;
using ObjectPool;

/// <summary>
/// Bullet‚ÌPool
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour, IPool, IPoolEvent
{
    [SerializeField] float _activeTime;

    float _timer;
    BulletData _bulletData;

    Rigidbody _rb;
    Transform _parent;

    public bool IsDone { get; set; }

    public void Setup(Transform parent)
    {
        _rb = GetComponent<Rigidbody>();
        _rb.useGravity = false;

        _parent = parent;
    }

    public void SetData(BulletData data)
    {
        _bulletData = data;
    }

    public void OnEnableEvent()
    {
        transform.position = _parent.position;
    }

    public bool Execute()
    {
        _timer += Time.deltaTime;

        return _timer > _activeTime;
    }

    public void Delete()
    {
        _timer = 0;
    }

    private void OnCollisionEnter(Collision collision)
    {
        IsDone = true;
    }
}
