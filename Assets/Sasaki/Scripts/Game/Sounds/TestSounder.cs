# if UNITY_EDITOR

using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Soundのテスト用クラス
/// </summary>
public class TestSounder : MonoBehaviour
{
    [SerializeField] SoundManager _soundManager;
    [SerializeField] Transform _user;
    [SerializeField] string _bgmPath;
    [SerializeField] string _sePath;

    [SerializeField] Button _playBGMButton;
    [SerializeField] Button _stopBGMButton;
    [SerializeField] Button _playSEButton;
    [SerializeField] VolumeOprater.VolumeType _volumeType;
    [SerializeField, Range(0, 1)] float _volume;

    void Start()
    {
        _playBGMButton?.onClick.AddListener(() => PlayBGM());
        _playSEButton?.onClick.AddListener(() => PlaySE());
        _stopBGMButton?.onClick.AddListener(() => StopBGM());
    }

    void Update()
    {
        _soundManager.ChangeVolume(_volumeType, _volume);
    }

    void PlayBGM()
    {
        _soundManager.PlayRequest(SoundType.BGM, _bgmPath, _user);
    }

    void StopBGM()
    {
        _soundManager.StopCurrentBGM();
    }

    void PlaySE()
    {
        _soundManager.PlayRequest(SoundType.SE, _sePath, _user);
    }
}

# endif