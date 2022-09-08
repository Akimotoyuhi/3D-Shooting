using System;
using UnityEngine;
using UniRx;

/// <summary>
/// Charactorのステータスデータ
/// </summary>

[Serializable]
public class UserData
{
    [SerializeField] int _hp;
    [SerializeField] int _power;

    ReactiveProperty<int> _reactiveHP = new ReactiveProperty<int>();

    public IObservable<int> HPObservable => _reactiveHP.SkipLatestValueOnSubscribe();

    public int CurrentHP => _reactiveHP.Value;
    public int MaxHP { get; private set; }
    public int Power => _power;

    public void Setup()
    {
        _reactiveHP.Value = _hp;
        MaxHP = _hp;
    }

    /// <summary>
    /// 更新されたHP情報の設定
    /// </summary>
    /// <param name="hp">新しいHP</param>
    public void UpdateHP(int hp)
    {
        _reactiveHP.Value = hp;
    }
}
