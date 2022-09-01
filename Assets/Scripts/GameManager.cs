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
    /// <summary>FieldState�̕ύX��ʒm����<br/>�ŏ��̒ʒm�͖�������</summary>
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
    /// �F�X����
    /// </summary>
    private void Create()
    {
        CurrentPlayer = Instantiate(_playerPrefab);
        CurrentPlayer.Setup();
    }

    /// <summary>
    /// ���b�̑҂��Ă���FIeldState��ς���<br/>�e�X�g�p
    /// </summary>
    private async void DelayFieldStateChange(int second, FieldState fieldState)
    {
        await UniTask.Delay(second * 1000); //�~���b�ɕϊ�
        _fieldState.SetValueAndForceNotify(fieldState);
        Debug.Log($"FieldState��{_fieldState.Value}�ɕύX");
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