using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Cysharp.Threading.Tasks;
using DG.Tweening;

public class FieldManager : MonoBehaviour
{
    private FieldState _fieldState;

    public void Setup()
    {
        GameManager.Instance.FieldStateObservable
            .Subscribe(s => _fieldState = s)
            .AddTo(this);
    }

    public void StartWave(List<WaveEnemiesData> waveEnemiesDatas)
    {
        waveEnemiesDatas.ForEach(data =>
        {
            CreateEnemies(data.SpawnTime, data.SpawnPosition, data.EnemyPrefabs).Forget();
        });
    }

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
