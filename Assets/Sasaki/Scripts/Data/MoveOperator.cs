using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveOperator : MonoBehaviour
{
    [SerializeField] MoveData _moveData;

    float _timer;
    bool _isRequest;



    void Update()
    {
        if (!_isRequest)
        {
            return;
        }

        _timer += Time.deltaTime;


    }

    public Vector3 MoveCollect(Transform t)
    {
        Vector3 forward = t.forward;



        return Vector3.zero;
    }

    public void Initalize()
    {
        _timer = 0;
        _isRequest = false;
    }

    public void OprationRequest(bool isRequest)
    {
        _isRequest = isRequest;
    }
}
