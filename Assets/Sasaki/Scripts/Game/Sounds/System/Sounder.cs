using UnityEngine;
using ObjectPool;

/// <summary>
/// Sound‚ð–Â‚ç‚·ƒNƒ‰ƒX
/// </summary>
public class Sounder : MonoBehaviour, IPool, IPoolEvent
{
    AudioSource _source;

    float _volume;
    bool _isSet;

    SoundType _soundType;

    public bool IsDone { get; set; }

    public void Setup(Transform parent)
    {
        _source = gameObject.AddComponent<AudioSource>();

        _volume = 0;
        _isSet = false;
    }

    public void OnEnableEvent()
    {
        _source.Play();
    }

    public void SetData(SoundData data, Transform user, SoundType type)
    {
        _volume = data.Volume;
        SetVolume(data.Volume, type);

        _source.spatialBlend = data.SpatialBlend;

        if (type == SoundType.BGM) _source.loop = true;
        else _source.loop = false;

        if (user != null) transform.position = user.position;
        else transform.position = Vector3.zero;

        _soundType = type;
        _isSet = true;
    }

    void SetVolume(float volume, SoundType type)
    {
        float master = volume * SoundMasterData.MasterVolume;
        float set = 0;

        switch (type)
        {
            case SoundType.BGM:
                set = master * SoundMasterData.BGMVolume;

                break;
            case SoundType.SE:
                set = master * SoundMasterData.SEVolume;

                break;
        }

        _source.volume = set;
    }

    public bool Execute()
    {
        if (!_isSet)
        {
            return false;
        }

        SetVolume(_volume, _soundType);

        return !_source.isPlaying;
    }

    public void Stop()
    {
        IsDone = true;
    }

    public void Delete()
    {
        _source.Stop();
        _volume = 0;
        _isSet = false;
    }
}
