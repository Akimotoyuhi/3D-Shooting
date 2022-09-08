using System;
using UnityEngine;
using UniRx;

/// <summary>
/// Charactor�̃X�e�[�^�X�f�[�^
/// </summary>

[System.Serializable]
public class UserStatusData
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
    /// �X�V���ꂽHP���̐ݒ�
    /// </summary>
    /// <param name="hp">�V����HP</param>
    public void UpdateHP(int hp)
    {
        _reactiveHP.Value = hp;
    }
}
