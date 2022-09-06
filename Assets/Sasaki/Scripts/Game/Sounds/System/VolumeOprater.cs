using UnityEngine;

public class VolumeOprater : MonoBehaviour
{
    [SerializeField, Range(0, 1)] float _master = 0.5f;
    [SerializeField, Range(0, 1)] float _bgm = 0.5f;
    [SerializeField, Range(0, 1)] float _se = 0.5f;

    void Update()
    {
        SoundMasterData.MasterVolume = _master;
        SoundMasterData.BGMVolume = _bgm;
        SoundMasterData.SEVolume = _se;
    }
}
