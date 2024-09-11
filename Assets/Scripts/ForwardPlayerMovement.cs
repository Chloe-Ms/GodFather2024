using System.Collections;
using UnityEngine;

public class ForwardPlayerMovement : MonoBehaviour
{
    [SerializeField] float _forwardMaxSpeed = 10f;
    [SerializeField] float _timeToAccelerate = 2f;
    [SerializeField] float _timeToDeccelerate = 2f;
    Camera _cam;

    Coroutine _coroutine;
    float _speed = 0f;

    private void Start()
    {
        _cam = Camera.main;
    }

    IEnumerator RoutineAccelerate()
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
            _coroutine = StartCoroutine(RoutineAccelerate());
        }
    }

    public void StopMovingForward()
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
            _coroutine = null;
        }
        _speed = 0f;
    }

    public void StopDecelerateMovingForward()
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
            _coroutine = null;
        }
        _coroutine = StartCoroutine(RoutineDecelerate());
    }

    IEnumerator RoutineDecelerate()
    {
        float timer = 0f;
        while (timer < _timeToAccelerate)
        {
            timer += Time.deltaTime;
            _speed = Mathf.Lerp(_forwardMaxSpeed, 0f, timer / _timeToDeccelerate);
            yield return null;
        }
        _speed = 0f;
    }

    void MoveForward()
    {
        if (_speed > 0f)
        {
            transform.position += _cam.transform.forward * _speed * Time.deltaTime;
        }
    }

    //Start instead of the board
    void InputStartMoving()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            StartMovingForward();
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position -= transform.right * Time.deltaTime * 5;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += transform.right * Time.deltaTime * 5;
        }
    }

    private void Update()
    {
        InputStartMoving();
        MoveForward();
    }
}
