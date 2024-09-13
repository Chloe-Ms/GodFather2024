using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeCollision : MonoBehaviour
{
    [SerializeField] GameObject _objectToShake;
    [SerializeField] float _shakeDuration = 1f;
    [SerializeField] float _shakeViolence = 0.5f;

    public void StartShake()
    {
        _objectToShake.transform.DOShakePosition(_shakeDuration, _shakeViolence);
    }
}
