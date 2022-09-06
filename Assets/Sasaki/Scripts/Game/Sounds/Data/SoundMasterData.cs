/// <summary>
/// Soundのマスター音量
/// </summary>
public static class SoundMasterData
{
    static float _masterVolume = 0;
    static float _bgmVolume = 0;
    static float _seVolume = 0;

    public static float MasterVolume
    {
        get => _masterVolume;

        set
        {
            if (value > 1)
            {
                _masterVolume = 1;
            }
            else
            {
                _masterVolume = value;
            }
        }
    }

    public static float BGMVolume
    {
        get => _bgmVolume;

        set
        {
            if (value > 1)
            {
                _bgmVolume = 1;
            }
            else
            {
                _bgmVolume = value;
            }
        }
    }

    public static float SEVolume
    {
        get => _seVolume;

        set
        {
            if (value > 1)
            {
                _seVolume = 1;
            }
            else
            {
                _seVolume = value;
            }
        }
    }
}
