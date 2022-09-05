using System.Collections.Generic;
using UnityEngine;
using ObjectPool;

public class SoundManager : MonoBehaviour
{
    [SerializeField] Sounder _sounderPrefab;
    [SerializeField] int _createCount = 5;
    [SerializeField] List<SoundDataBase> _dataList;
    

    Pool<Sounder> _sounderPool = new Pool<Sounder>();

    void Awake()
    {
        _sounderPool
            .SetMono(_sounderPrefab, _createCount)
            .IsAutoActive()
            .CreateRequest();
    }

    public void PlayRequest(string path)
    {

    }
}
