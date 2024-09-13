using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalMovement : MonoBehaviour
{
    [SerializeField] private float _timerOffset = 0f;
    [SerializeField] private float _heightMovement = 1f;
    [SerializeField] private float _speedMovement = 1f;
    float _timer;
    ForwardWorldMovement _worldMovement;
    Vector3 _startingPosition;

    void Start()
    {
        _timer = _timerOffset;
        _worldMovement = Managers.WorldMovement;
        _startingPosition = transform.position;
        Vector3 position = transform.position;
        position.y = _startingPosition.y + _heightMovement * Mathf.Sin(_speedMovement * _timer);
        transform.position = position;
    }

    void Update()
    {
        if (_worldMovement.Speed > 0)
        {
            float ratioSpeed = _worldMovement.Speed / _worldMovement.ForwardMaxSpeed;
            _timer += Time.deltaTime * ratioSpeed * ratioSpeed;
            Vector3 position = transform.position;
            position.y = _startingPosition.y + _heightMovement * Mathf.Sin(_speedMovement * _timer);
            transform.position = position;
        }
    }
}
