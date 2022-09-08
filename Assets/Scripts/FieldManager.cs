using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Cysharp.Threading.Tasks;
using DG.Tweening;

/// <summary>
/// �G�̐��������s��Field�̊Ǘ��N���X
/// </summary>
public class FieldManager : MonoBehaviour
{
    /// <summary>���݂�FieldState�ۑ��p</summary>
    private FieldState _fieldState;

    public void Setup()
    {
        GameManager.Instance.FieldStateObservable
            .Subscribe(s => _fieldState = s)
            .AddTo(this);
    }

    /// <summary>
    /// Wave�̊J�n
    /// </summary>
    /// <param name="waveEnemiesDatas">�J�n����Wave�̃f�[�^</param>
    public void StartWave(List<WaveEnemiesData> waveEnemiesDatas)
    {
        waveEnemiesDatas.ForEach(data =>
        {
            CreateEnemies(data.SpawnTime, data.SpawnPosition, data.EnemyPrefabs).Forget();
        });
    }

    /// <summary>
    /// �G�̐���
    /// </summary>
    private async UniTask CreateEnemies(float duration, Vector3 spawnPosition, List<Enemy> enemies)
    {
        //�~���b�ɕϊ�
        float i = duration * 1000;
        await UniTask.Delay((int)i);

        //�G�𐶐�
        enemies.ForEach(enemy =>
        {
            Enemy e = Instantiate(enemy);
            e.transform.position = spawnPosition;
        });
    }
}
