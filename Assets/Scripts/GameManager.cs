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
    /// <summary>FieldStateの変更を通知する<br/>最初の通知は無視する</summary>
    public System.IObservable<FieldState> FieldStateObservable => _fieldState.SkipLatestValueOnSubscribe();

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Create();
    }

    /// <summary>
    /// 色々生成
    /// </summary>
    private void Create()
    {
        CurrentPlayer = Instantiate(_playerPrefab);
        CurrentPlayer.Setup();
    }

    /// <summary>
    /// 数秒の待ってからFIeldStateを変える<br/>テスト用
    /// </summary>
    private async void DelayFieldStateChange(int second, FieldState fieldState)
    {
        await UniTask.Delay(second * 1000); //ミリ秒に変換
        _fieldState.SetValueAndForceNotify(fieldState);
        Debug.Log($"FieldStateを{_fieldState.Value}に変更");
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