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

    float _curveVal;
    float _curveSpeed;
    Vector3 _velocity;
    
    Rigidbody _rb;
    Transform _parent;

    public bool IsDone { get; set; }

    public void Setup(Transform parent)
    {
        _rb = GetComponent<Rigidbody>();
        _rb.useGravity = false;

        _parent = parent;
    }

    public void SetData(Vector3 velocity, float curveVal, float curveSpeed)
    {
        _velocity = velocity;
        _curveVal = curveVal;
        _curveSpeed = curveSpeed;
    }

    public void OnEnableEvent()
    {
        transform.position = _parent.position;
    }

    public bool Execute()
    {
        _timer += Time.deltaTime;

        Vector3 velocity = _velocity;
        float curve = _timer * _curveSpeed;



        _rb.velocity = velocity;

        return _timer > _activeTime;
    }

    public void Delete()
    {
        _timer = 0;
        _velocity = Vector3.zero;
    }

    private void OnCollisionEnter(Collision collision)
    {
        //IsDone = true;
    }
}
