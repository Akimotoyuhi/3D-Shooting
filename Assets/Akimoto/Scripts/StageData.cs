using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ステータス毎の情報をまとめたデータの集まり
/// </summary>
[CreateAssetMenu(fileName = "Enemy Data")]
public class StageData : ScriptableObject
{
    [SerializeField] List<WaveData> _datas;
    public List<WaveData> Datas => _datas;
}

/// <summary>ウェーブ事の視点情報、出現する敵らをまとめたデータ</summary>
[System.Serializable]
public class WaveData
{
    [SerializeField] FieldState _fieldState;
    [SerializeField] List<WaveEnemiesData> _waveEnemiesDatas;
    /// <summary>変更するFieldState</summary>
    public FieldState FieldState => _fieldState;
    /// <summary>このウェーブで出現する敵とその出現タイミング</summary>
    public List<WaveEnemiesData> WaveEnemiesDatas => _waveEnemiesDatas;
}

/// <summary>出現する敵とその出現タイミング</summary>
[System.Serializable]
public class WaveEnemiesData
{
    [SerializeField] float _spawnTime;
    [SerializeField] Vector3 _spawnPosition;
    [SerializeField] List<Enemy> _enemyPrefabs;
    /// <summary>ウェーブが始まってから何秒後に生成されるか</summary>
    public float SpawnTime => _spawnTime;
    /// <summary>生成位置</summary>
    public Vector3 SpawnPosition => _spawnPosition;
    /// <summary>生成される敵たち</summary>
    public List<Enemy> EnemyPrefabs => _enemyPrefabs;
}
