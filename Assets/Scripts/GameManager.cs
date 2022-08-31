using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Cysharp.Threading.Tasks;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    [SerializeField] Player _playerPrefab;
    private ReactiveProperty<FieldState> _fieldState = new ReactiveProperty<FieldState>();
    public static GameManager Instance { get; private set; }
    /// <summary>���ݐ�������Ă���v���C���[</summary>
    public Player CurrentPlayer { get; private set; }
    /// <summary>FieldState�̕ύX��ʒm����</summary>
    public System.IObservable<FieldState> FieldStateObservable => _fieldState;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        Create();
        _fieldState.Value = FieldState.Behind;
    }

    /// <summary>
    /// �F�X����
    /// </summary>
    private void Create()
    {
        CurrentPlayer = Instantiate(_playerPrefab);
        CurrentPlayer.Setup();
    }
}

/// <summary>
/// �t�B�[���h�̏��
/// </summary>
public enum FieldState
{
    Up,
    Down,
    Right,
    Left,
    Forward,
    Behind,
}