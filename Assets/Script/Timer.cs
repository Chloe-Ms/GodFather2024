using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UIElements;

public class Timer : MonoBehaviour
{
    
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField]float remainingTime;
    [SerializeField]float _timeBeforePanelAppears = 2f;
    public GameObject gameOverPanel;
    public GameObject winPanel;
    public GameObject timerPanel;
    private bool startTimer = false;
    private bool _isGameFinished = false;
    private int nbShake = 0;

    AudioManager audioManager;

    public void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void Start()
    {
        gameOverPanel.SetActive(false);
        winPanel.SetActive(false);
        Managers.ManagerEndings.OnLoseEvent += OnLoseEvent;
        Managers.ManagerEndings.OnWinEvent += OnWinEvent;
    }

    private void OnLoseEvent()
    {
        _isGameFinished = true;
        gameOverPanel.SetActive(true);
        audioManager.PlaySFX(audioManager.loseMusic);
        audioManager.musicSource.Stop();
    }

    private void OnWinEvent()
    {
        _isGameFinished = false;
        StartCoroutine(WaitToDisplayPanel());
    }

    IEnumerator WaitToDisplayPanel()
    {
        yield return new WaitForSeconds(_timeBeforePanelAppears);
        winPanel.SetActive(true);
        audioManager.PlayMusic(audioManager.winMusic);
    }

    void Update()
    {
        if (!_isGameFinished)
        {
            switch (startTimer)
            {
                case true:
                    if (remainingTime > 0)
                    {
                        remainingTime -= Time.deltaTime;
                        if (remainingTime < 10)
                        {
                            timerText.color = Color.red;
                            switch (nbShake)
                            {
                                case 1:
                                    timerText.rectTransform.DOShakePosition(10, new Vector3(20, 6, 0), 60, 90, true, false);
                                    nbShake += 1;
                                    break;
                            }
                        }
                        else if (remainingTime < 50)
                        {
                            timerText.color = Color.yellow;
                            switch (nbShake)
                            {
                                case 0:
                                    timerText.rectTransform.DOShakePosition(10, new Vector3(10, 3, 0), 60, 90, true, false);
                                    nbShake += 1;
                                    break;
                            }
                        }
                    }
                    else if (remainingTime < 0)
                    {
                        remainingTime = 0;
                        // GameOver
                        OnLoseEvent();
                    }
                    int minutes = Mathf.FloorToInt(remainingTime / 60);
                    int seconds = Mathf.FloorToInt(remainingTime % 60);
                    timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
                    break;
            }
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            startTimer = true;
            timerPanel.SetActive(true);
            audioManager.PlayMusic(audioManager.mainMenuMusic);
        }
    }
}
