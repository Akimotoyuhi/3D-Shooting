using UnityEngine;
using UniRx;

public interface IDamageble
{
    void GetDamage(int damage);
}

/// <summary>
/// Characterの基底クラス
/// </summary>

[RequireComponent(typeof(Rigidbody))]
public abstract class CharaBase : MonoBehaviour
{
    [SerializeField] UserData _statusData;

    protected Rigidbody Rigidbody { get; private set; }

    protected UserData UserData => _statusData;

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

    /// <summary>
    /// 初期化
    /// </summary>
    protected abstract void Setup();

    /// <summary>
    /// 死んだ際の処理
    /// </summary>
    protected abstract void DeadEvent();
}
