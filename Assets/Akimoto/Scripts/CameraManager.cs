using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Cysharp.Threading.Tasks;
using DG.Tweening;

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
        GameManager.Instance.FieldStateObservable.Subscribe(s => CameraMove(s)).AddTo(this);
    }

    private void CameraMove(FieldState fieldState)
    {
        //カメラの位置設定項目から、現在のFieldStateと一致するものを探す
        CameraPositionSetting cps = default;
        _cameraPositonSettings.ForEach(x =>
        {
            if (x.FieldState == fieldState)
                cps = x;
        });
        Debug.Log($"FieldState:{cps.FieldState}");

        if (_camera == null)
        {
            Debug.LogError("カメラがnullです");
            return;
        }


        //回転
        _sequence = DOTween.Sequence();
        _sequence.Append(_camera.transform.DOMove(cps.CameraPosition.position, _moveDuraiton))
            .Join(_camera.transform.DOLocalRotateQuaternion(Quaternion.Euler(cps.Rotation), _moveDuraiton));
    }

    [System.Serializable]
    public class CameraPositionSetting
    {
        [SerializeField] FieldState _fieldState;
        [SerializeField] Transform _cameraPosition;
        [SerializeField] Vector3 _rotation;
        public FieldState FieldState => _fieldState;
        public Transform CameraPosition => _cameraPosition;
        public Vector3 Rotation => _rotation;
    }
}
