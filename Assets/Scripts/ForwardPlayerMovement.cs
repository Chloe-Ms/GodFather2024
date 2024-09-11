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
        float timer = 0f;
        while (timer < _timeToAccelerate)
        {
            timer += Time.deltaTime;
            _speed = Mathf.Lerp(0f, _forwardMaxSpeed, timer / _timeToAccelerate);
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
            transform.position += _cam.transform.forward * _speed * Time.deltaTime;
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
