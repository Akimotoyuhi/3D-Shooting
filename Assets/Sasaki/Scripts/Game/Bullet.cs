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

    float _speed;
    float _curve;
    Vector3 _dir;
    
    Rigidbody _rb;
    Transform _parent;

    public bool IsDone { get; set; }

    public void Setup(Transform parent)
    {
        _rb = GetComponent<Rigidbody>();
        _rb.useGravity = false;

        _parent = parent;
    }

    public void SetData(float speed, Vector3 dir, float curve)
    {
        _speed = speed;
        _dir = dir;
        _curve = curve;
    }

    public void OnEnableEvent()
    {
        transform.position = _parent.position;
    }

    public bool Execute()
    {
        _timer += Time.deltaTime;

        _rb.velocity = _dir * _speed;

        return _timer > _activeTime;
    }

    public void Delete()
    {
        _timer = 0;
        _speed = 0;
    }

    private void OnCollisionEnter(Collision collision)
    {
        //IsDone = true;
    }
}
