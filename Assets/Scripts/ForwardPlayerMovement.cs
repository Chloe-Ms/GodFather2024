using System.Collections;
using UnityEngine;

public class ForwardPlayerMovement : MonoBehaviour
{
    [SerializeField] float _forwardMaxSpeed = 10f;
    [SerializeField] float _timeToAccelerate = 2f;
    Camera _cam;

    Coroutine _coroutine;
    float _speed = 0f;

    private void Start()
    {
        _cam = Camera.main;
    }

    IEnumerator RoutineStart()
    {
        while (_speed < _forwardMaxSpeed)
        {
            _speed += Time.deltaTime / _timeToAccelerate;
            yield return null;
        }
        _speed = _forwardMaxSpeed;
    }

    public void StartMovingForward()
    {
        if (_coroutine == null && _speed != _forwardMaxSpeed)
        {
            _coroutine = StartCoroutine(RoutineStart());
        }
    }

    public void StopMovingForward()
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
            _coroutine = null;
        }
    }

    void MoveForward()
    {
        if (_speed > 0f)
        {
            transform.position += _cam.transform.forward * _speed;
        }
    }

    //Use instead of the board
    void InputStartMoving()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            StartMovingForward();
        }
    }

    private void Update()
    {
        InputStartMoving();
        MoveForward();
    }
}
