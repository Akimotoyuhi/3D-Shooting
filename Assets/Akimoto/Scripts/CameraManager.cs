using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Cysharp.Threading.Tasks;
using DG.Tweening;

/// <summary>
/// カメラの管理クラス
/// </summary>
public class CameraManager : MonoBehaviour
{
    /// <summary>動かす対象のカメラ</summary>
    [SerializeField] Camera _camera;
    /// <summary>FieldStateに応じた位置や、カメラの角度の設定項目</summary>
    [SerializeField] List<CameraPositionSetting> _cameraPositonSettings;
    [SerializeField] float _moveDuraiton;
    private Sequence _sequence;

    private void Start()
    {
        GameManager.Instance.FieldStateObservable
            .Subscribe(x => CameraMove(x))
            .AddTo(this);
    }

    /// <summary>
    /// FieldStateに応じてカメラの位置と向きを変更する
    /// </summary>
    /// <param name="fieldState"></param>
    private void CameraMove(FieldState fieldState)
    {
        //カメラの位置設定項目から、現在のFieldStateと一致するものを探す
        CameraPositionSetting cps = default;
        _cameraPositonSettings.ForEach(x =>
        {
            if (x.FieldState == fieldState)
                cps = x;
        });

        if (_camera == null)
        {
            Debug.LogError("カメラがnullです");
            return;
        }


        //回転
        _sequence = DOTween.Sequence();
        _sequence.Append(_camera.transform.DOMove(cps.Position.position, _moveDuraiton))
            .Join(_camera.transform.DOLocalRotateQuaternion(Quaternion.Euler(cps.Rotation), _moveDuraiton));
    }

    /// <summary>
    /// カメラの位置や角度を設定する為のクラス
    /// </summary>
    [System.Serializable]
    public class CameraPositionSetting
    {
        [SerializeField] FieldState _fieldState;
        [SerializeField] Transform _position;
        [SerializeField] Vector3 _rotation;
        /// <summary>データを取り出すState</summary>
        public FieldState FieldState => _fieldState;
        /// <summary>位置</summary>
        public Transform Position => _position;
        /// <summary>角度</summary>
        public Vector3 Rotation => _rotation;
    }
}
