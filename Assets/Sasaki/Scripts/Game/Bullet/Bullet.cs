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

    float _curve;
    FieldStateHelper.State _state;
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

    public void SetData(Vector3 velocity, float curve, FieldStateHelper.State state)
    {
        _velocity = velocity;
        _curve = curve;
        _state = state;
    }

    public void OnEnableEvent()
    {
        transform.position = _parent.position;
        transform.rotation = Quaternion.LookRotation(_velocity);
    }

    public bool Execute()
    {
        _timer += Time.deltaTime;

        float curve = _timer * _curve;
        _rb.velocity = _velocity + SetCurve();

        return _timer > _activeTime;
    }

    Vector3 SetCurve()
    {
        float curve = _timer * _curve;

        Vector3 velcity = Vector3.zero;

        switch (_state)
        {
            case FieldStateHelper.State.TopView: velcity = transform.right * curve;
                break;
            case FieldStateHelper.State.SideView: velcity = transform.up * curve;
                break;
            case FieldStateHelper.State.BackView: velcity= transform.right * curve;
                break;
        }

        return velcity;
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
