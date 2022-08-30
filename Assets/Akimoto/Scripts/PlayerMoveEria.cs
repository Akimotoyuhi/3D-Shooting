using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Cysharp.Threading.Tasks;
using DG.Tweening;

public class PlayerMoveEria : MonoBehaviour
{
    [SerializeField] Transform _beginMoveLimitPos;
    [SerializeField] Transform _endMoveLimitPos;
    [SerializeField] Player _player;

    private void Start()
    {
        
    }

    private void Update()
    {
    }
}
