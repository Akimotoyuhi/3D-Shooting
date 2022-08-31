using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Cysharp.Threading.Tasks;
using DG.Tweening;

public class CameraManager : MonoBehaviour
{
    /// <summary>�������Ώۂ̃J����</summary>
    [SerializeField] Camera _camera;
    /// <summary>FieldState�ɉ������ʒu��A�J�����̊p�x�̐ݒ荀��</summary>
    [SerializeField] List<CameraPositionSetting> _cameraPositonSettings;
    [SerializeField] float _moveDuraiton;
    private Sequence _sequence;

    private void Start()
    {
        GameManager.Instance.FieldStateObservable.Subscribe(s => CameraMove(s)).AddTo(this);
    }

    private void CameraMove(FieldState fieldState)
    {
        //�J�����̈ʒu�ݒ荀�ڂ���A���݂�FieldState�ƈ�v������̂�T��
        CameraPositionSetting cps = default;
        _cameraPositonSettings.ForEach(x =>
        {
            if (x.FieldState == fieldState)
                cps = x;
        });
        Debug.Log($"FieldState:{cps.FieldState}");

        if (_camera == null)
        {
            Debug.LogError("�J������null�ł�");
            return;
        }


        //��]
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
