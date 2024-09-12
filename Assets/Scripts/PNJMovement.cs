using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PNJMovement : MonoBehaviour
{
    [SerializeField] AnimationCurve _curveSpeed;
    [SerializeField] float _movementDuration = 1.0f;
    [SerializeField] GameObject _startingPoint;
    [SerializeField] GameObject _endingPoint;
    Coroutine _coroutine;

    private void Start()
    {
        gameObject.transform.position = _startingPoint?.transform.position ?? Vector3.zero;
    }

    public void Move()
    {
        if (_coroutine == null)
        {
            _coroutine = StartCoroutine(RoutineMove());
        }
    }

    IEnumerator RoutineMove()
    {
        ForwardWorldMovement forwardWorldMovement = Managers.WorldMovement;
        yield return new WaitUntil(() => forwardWorldMovement.HasStarted);
        float timer = 0f;
        while (timer < _movementDuration)
        {
            gameObject.transform.localPosition = Vector3.Lerp(
                _startingPoint.transform.position, 
                _endingPoint.transform.position,
                _curveSpeed.Evaluate(timer / _movementDuration)
                );
            timer += Time.deltaTime;
            yield return null;
        }
        gameObject.transform.position = _endingPoint.transform.position;
    }
}
