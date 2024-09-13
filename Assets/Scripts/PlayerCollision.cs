using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    ForwardWorldMovement _worldMovement;
    [SerializeField] float _timeAccelerationAfterCollide;
    Coroutine _coroutine;

    private void Awake()
    {
        Managers.PlayerCollision = this;
    }
    private void Start()
    {
        _worldMovement = Managers.WorldMovement;
    }

    public void CollideWithObstacle()
    {
        _worldMovement?.StopMovingForward();
        if ( _coroutine != null )
        {
            StopCoroutine( _coroutine);
            _coroutine = null;
        }
        _worldMovement?.Knockback();
        //_coroutine = StartCoroutine(WaitBeforeAcceleration());
    }

    IEnumerator WaitBeforeAcceleration()
    {
        float timer = 0f;
        while (timer < _timeAccelerationAfterCollide)
        {
            timer += Time.deltaTime;
            yield return null;
        }
        _worldMovement?.StartMovingForward();
    }
}
