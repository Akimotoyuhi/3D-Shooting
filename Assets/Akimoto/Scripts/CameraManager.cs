using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Cysharp.Threading.Tasks;
using DG.Tweening;

/// <summary>
/// �J�����̊Ǘ��N���X
/// </summary>
public class CameraManager : MonoBehaviour
{
    /// <summary>�������Ώۂ̃J����</summary>
    [SerializeField] Camera _camera;
    /// <summary>FieldState�ɉ������ʒu��A�J�����̊p�x�̐ݒ荀��</summary>
    [SerializeField] List<CameraPositionSetting> _cameraPositonSettings;
    /// <summary>�ړ��ɂ����鎞��</summary>
    [SerializeField] float _moveDuraiton;
    private Sequence _sequence;

    private void Start()
    {
        //FieldState�̕ω����Ď�
        GameManager.Instance.FieldStateObservable
            .Subscribe(x => CameraMove(x))
            .AddTo(this);
    }

    /// <summary>
    /// FieldState�ɉ����ăJ�����̈ʒu�ƌ�����ύX����
    /// </summary>
    /// <param name="fieldState"></param>
    private void CameraMove(FieldState fieldState)
    {
        //�J�����̈ʒu�ݒ荀�ڂ���A���݂�FieldState�ƈ�v������̂�T��
        CameraPositionSetting cps = default;
        _cameraPositonSettings.ForEach(x =>
        {
            if (x.FieldState == fieldState)
                cps = x;
        });

        if (_camera == null)
        {
            Debug.LogError("�J������null�ł�");
            return;
        }


        //��]
        _sequence = DOTween.Sequence();
        _sequence.Append(_camera.transform.DOMove(cps.Position.position, _moveDuraiton))
            .Join(_camera.transform.DOLocalRotateQuaternion(Quaternion.Euler(cps.Rotation), _moveDuraiton));
    }

    /// <summary>
    /// �J�����̈ʒu��p�x��ݒ肷��ׂ̃N���X
    /// </summary>
    [System.Serializable]
    public class CameraPositionSetting
    {
        [SerializeField] FieldState _fieldState;
        [SerializeField] Transform _position;
        [SerializeField] Vector3 _rotation;
        /// <summary>�f�[�^�����o��State</summary>
        public FieldState FieldState => _fieldState;
        /// <summary>�ʒu</summary>
        public Transform Position => _position;
        /// <summary>�p�x</summary>
        public Vector3 Rotation => _rotation;
    }
}
