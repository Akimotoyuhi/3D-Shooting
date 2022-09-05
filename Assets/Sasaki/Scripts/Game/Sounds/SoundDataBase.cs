using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum SoundType
{
    BGM,
    SE,
}

/// <summary>
/// 
/// </summary>
[CreateAssetMenu(fileName = "SoundData")]
public class SoundDataBase : ScriptableObject
{
    [SerializeField] SoundType _soundType;
    [SerializeField] List<SoundData> _dataList;

    public SoundType SoundType => _soundType;
    public SoundData GetData(string path)
    {
        SoundData data;

        try
        {
            data = _dataList.First(d => d.Path == path);
        }
        catch
        {
            data = null;
            Debug.LogWarning($"SoundData‚ª‚ ‚è‚Ü‚¹‚ñ‚Å‚µ‚½BFindPath => {path}");
        }

        return data;
    }
}
