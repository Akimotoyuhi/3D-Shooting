using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum SoundType
{
    BGM,
    SE,
}

/// <summary>
/// �T�E���h�̃f�[�^�N���X
/// </summary>
[CreateAssetMenu(fileName = "SoundData")]
public class SoundDataBase : ScriptableObject
{
    [SerializeField] SoundType _soundType;
    [SerializeField] List<SoundData> _dataList;

    public SoundType SoundType => _soundType;

    /// <summary>
    /// SoundData�̎擾
    /// </summary>
    /// <param name="path">Sound�̖��O</param>
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
            Debug.LogWarning($"SoundData������܂���ł����BFindPath => {path}");
        }

        return data;
    }
}
