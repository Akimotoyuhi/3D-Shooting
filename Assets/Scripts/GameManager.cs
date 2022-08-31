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
    /// <summary>現在生成されているプレイヤー</summary>
    public Player CurrentPlayer { get; private set; }
    /// <summary>FieldStateの変更を通知する</summary>
    public System.IObservable<FieldState> FieldStateObservable => _fieldState;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Create();
        Count();
    }

    /// <summary>
    /// 色々生成
    /// </summary>
    private void Create()
    {
        CurrentPlayer = Instantiate(_playerPrefab);
        CurrentPlayer.Setup();
    }

    private async void Count()
    {
        await UniTask.Delay(5 * 1000);
        _fieldState.Value = FieldState.Behind;
        Debug.Log("FieldStateをBehindに変更");
    }
}

/// <summary>
/// フィールドの状態
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