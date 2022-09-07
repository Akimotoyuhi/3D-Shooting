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
    }

    public bool Execute()
    {
        _timer += Time.deltaTime;

        _rb.velocity = SetVeleocity();

        return _timer > _activeTime;
    }

    Vector3 SetVeleocity()
    {
        float curve = _timer * _curve;

        Vector3 velcity = _velocity;

        switch (_state)
        {
            case FieldStateHelper.State.TopView: velcity.x += curve;
                break;
            case FieldStateHelper.State.SideView: velcity.y += curve;
                break;
            case FieldStateHelper.State.BackView: velcity.z += curve;
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
