using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �X�e�[�^�X���̏����܂Ƃ߂��f�[�^�̏W�܂�
/// </summary>
[CreateAssetMenu(fileName = "Enemy Data")]
public class StageData : ScriptableObject
{
    [SerializeField] List<WaveData> _datas;
    public List<WaveData> Datas => _datas;
}

/// <summary>�E�F�[�u���̎��_���A�o������G����܂Ƃ߂��f�[�^</summary>
[System.Serializable]
public class WaveData
{
    [SerializeField] FieldState _fieldState;
    [SerializeField] List<WaveEnemiesData> _waveEnemiesDatas;
    /// <summary>�ύX����FieldState</summary>
    public FieldState FieldState => _fieldState;
    /// <summary>���̃E�F�[�u�ŏo������G�Ƃ��̏o���^�C�~���O</summary>
    public List<WaveEnemiesData> WaveEnemiesDatas => _waveEnemiesDatas;
}

/// <summary>�o������G�Ƃ��̏o���^�C�~���O</summary>
[System.Serializable]
public class WaveEnemiesData
{
    [SerializeField] float _spawnTime;
    [SerializeField] Vector3 _spawnPosition;
    [SerializeField] List<Enemy> _enemyPrefabs;
    /// <summary>�E�F�[�u���n�܂��Ă��牽�b��ɐ�������邩</summary>
    public float SpawnTime => _spawnTime;
    /// <summary>�����ʒu</summary>
    public Vector3 SpawnPosition => _spawnPosition;
    /// <summary>���������G����</summary>
    public List<Enemy> EnemyPrefabs => _enemyPrefabs;
}
