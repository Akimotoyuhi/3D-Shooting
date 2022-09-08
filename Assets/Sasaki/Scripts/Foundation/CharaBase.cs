using UnityEngine;
using UniRx;

public interface IDamageble
{
    void GetDamage(int damage);
}

/// <summary>
/// Character�̊��N���X
/// </summary>

[RequireComponent(typeof(Rigidbody))]
public abstract class CharaBase : MonoBehaviour
{
    [SerializeField] UserData _statusData;

    protected Rigidbody Rigidbody { get; private set; }

    protected UserData UserStatusData => _statusData;

    void Start()
    {
        Rigidbody = GetComponent<Rigidbody>();
        Rigidbody.useGravity = false;

        _statusData.Setup();
        SetEvent();

        Setup();
    }

    void SetEvent()
    {
        _statusData.HPObservable
            .Select(h => h <= 0)
            .Subscribe(_ => 
            {
                DeadEvent();
                Destroy(gameObject);
            })
            .AddTo(this);
    }

    protected abstract void Setup();

    protected abstract void DeadEvent();
}
