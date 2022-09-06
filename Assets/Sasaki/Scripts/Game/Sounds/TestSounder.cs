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

    void Start()
    {
        _playBGMButton?.onClick.AddListener(() => PlayBGM());
        _playSEButton?.onClick.AddListener(() => PlaySE());
        _stopBGMButton?.onClick.AddListener(() => StopBGM());
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