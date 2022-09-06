using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using ObjectPool;

/// <summary>
/// Sound�̊Ǘ��N���X
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
            .SetHideFlags()
            .CreateRequest();
    }

    /// <summary>
    /// Sound��炷�ۂ̐\��
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
    /// GBM���~�߂�ۂ̌Ăяo��
    /// </summary>
    public void StopCurrentBGM()
    {
        if (_bgmList.Count <= 0)
        {
            Debug.LogWarning("���ݍĐ�����BGM������܂���");
            return;
        }

        _bgmList.ForEach(b => b.Stop());
        _bgmList = new List<Sounder>();
    }

    /// <summary>
    /// Volume�ύX
    /// </summary>
    /// <param name="type">VolumeOprater.VolumeType</param>
    /// <param name="volume">�ύX��̉���</param>
    public void ChangeVolume(VolumeOprater.VolumeType type, float volume)
    {
        VolumeOprater.CallBack.Invoke(type, volume);
    }
}
