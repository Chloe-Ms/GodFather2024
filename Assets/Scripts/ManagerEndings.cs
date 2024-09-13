using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerEndings : MonoBehaviour
{
    public event Action OnWinEvent;
    public event Action OnLoseEvent;
    private void Awake()
    {
        Managers.ManagerEndings = this;
    }

    public void OnWin(){
        OnWinEvent?.Invoke();
    }

    public void OnLose()
    {
        OnLoseEvent?.Invoke();
    }
}
