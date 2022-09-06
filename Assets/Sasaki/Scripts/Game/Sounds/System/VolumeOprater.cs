using UnityEngine;

/// <summary>
/// Sound�̃{�����[������N���X
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
    /// ���ʂ��ύX���ꂽ�ۂ̃R�[���o�b�NAction
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
