using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Cysharp.Threading.Tasks;
using DG.Tweening;

/// <summary>
/// 敵の生成等を行うFieldの管理クラス
/// </summary>
public class FieldManager : MonoBehaviour
{
    /// <summary>現在のFieldState保存用</summary>
    private FieldState _fieldState;

    public void Setup()
    {
        GameManager.Instance.FieldStateObservable
            .Subscribe(s => _fieldState = s)
            .AddTo(this);
    }

    /// <summary>
    /// Waveの開始
    /// </summary>
    /// <param name="waveEnemiesDatas">開始するWaveのデータ</param>
    public void StartWave(List<WaveEnemiesData> waveEnemiesDatas)
    {
        waveEnemiesDatas.ForEach(data =>
        {
            CreateEnemies(data.SpawnTime, data.SpawnPosition, data.EnemyPrefabs).Forget();
        });
    }

    /// <summary>
    /// 敵の生成
    /// </summary>
    private async UniTask CreateEnemies(float duration, Vector3 spawnPosition, List<Enemy> enemies)
    {
        //ミリ秒に変換
        float i = duration * 1000;
        await UniTask.Delay((int)i);

        //敵を生成
        enemies.ForEach(enemy =>
        {
            Enemy e = Instantiate(enemy);
            e.transform.position = spawnPosition;
        });
    }
}
