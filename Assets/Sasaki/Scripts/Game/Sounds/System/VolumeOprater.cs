using UnityEngine;

/// <summary>
/// Soundのボリューム制御クラス
/// </summary>
public class VolumeOprater : MonoBehaviour
{
    public enum VolumeType
    {
        Master,
        BGM,
        SE
    }

    [SerializeField, Range(0, 1)] float _master = 0.5f;
    [SerializeField, Range(0, 1)] float _bgm = 0.5f;
    [SerializeField, Range(0, 1)] float _se = 0.5f;

    /// <summary>
    /// 音量が変更された際のコールバックAction
    /// </summary>
    public static System.Action<VolumeType, float> CallBack { get; private set; }

    void Awake()
    {
        CallBack = ChangeVolume;
    }

    void Update()
    {
        SoundMasterData.MasterVolume = _master;
        SoundMasterData.BGMVolume = _bgm;
        SoundMasterData.SEVolume = _se;
    }

    void ChangeVolume(VolumeType type, float volume)
    {
        switch (type)
        {
            case VolumeType.Master: _master = volume;
                break;
            case VolumeType.BGM: _bgm = volume;
                break;
            case VolumeType.SE: _se = volume;
                break;
        }
    }
}
