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
    public Player CurrentPlayer { get; private set; }
    public System.IObservable<FieldState> FieldStateObservable => _fieldState;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        Create();   
    }

    private void Create()
    {
        CurrentPlayer = Instantiate(_playerPrefab);
        CurrentPlayer.Setup();
    }
}

public enum FieldState
{
    Up,
    Down,
    Right,
    Left,
    Forward,
    Behind,
}