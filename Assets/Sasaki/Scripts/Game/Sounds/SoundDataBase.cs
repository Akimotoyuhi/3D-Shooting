using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum SoundType
{
    BGM,
    SE,
}

/// <summary>
/// サウンドのデータクラス
/// </summary>
[CreateAssetMenu(fileName = "SoundData")]
public class SoundDataBase : ScriptableObject
{
    [SerializeField] SoundType _soundType;
    [SerializeField] List<SoundData> _dataList;

    public SoundType SoundType => _soundType;

    /// <summary>
    /// SoundDataの取得
    /// </summary>
    /// <param name="path">Soundの名前</param>
    /// <returns></returns>
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
            Debug.LogWarning($"SoundDataがありませんでした。FindPath => {path}");
        }

        return data;
    }
}
