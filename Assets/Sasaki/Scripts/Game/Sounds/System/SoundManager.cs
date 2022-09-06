using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using ObjectPool;

/// <summary>
/// Soundの管理クラス
/// </summary>
public class SoundManager : MonoBehaviour
{
    [SerializeField] Sounder _sounderPrefab;
    [SerializeField] int _createCount = 5;
    [SerializeField] List<SoundDataBase> _dataList;

    Pool<Sounder> _sounderPool = new Pool<Sounder>();
    List<Sounder> _bgmList = new List<Sounder>();

    void Awake()
    {
        _sounderPool
            .SetMono(_sounderPrefab, _createCount)
            .IsAutoActive()
            .CreateRequest();
    }

    /// <summary>
    /// Soundを鳴らす際の申請
    /// </summary>
    /// <param name="type"></param>
    /// <param name="path"></param>
    /// <param name="user"></param>
    public void PlayRequest(SoundType type, string path, Transform user = null)
    {
        SoundDataBase dataBase = _dataList
            .Where(d => d.SoundType == type)
            .FirstOrDefault(d => d.GetData(path) != null ? true : false);

        if (dataBase != null)
        {
            Play(dataBase.GetData(path), user, type);
        }
    }

    void Play(SoundData data, Transform user, SoundType type)
    {
        System.Action action;
        Sounder sounder = _sounderPool.UseRequest(out action);
        sounder.SetData(data, user, type);

        action.Invoke();

        if (type == SoundType.BGM)
        {
            _bgmList.Add(sounder);
        }
    }

    /// <summary>
    /// GBMを止める際の呼び出し
    /// </summary>
    public void StopCurrentBGM()
    {
        if (_bgmList.Count <= 0)
        {
            Debug.LogWarning("現在再生中のBGMがありません");
            return;
        }

        _bgmList.ForEach(b => b.Stop());
        _bgmList = new List<Sounder>();
    }
}
