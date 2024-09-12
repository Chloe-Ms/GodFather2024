using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField]float remainingTime;
    public GameObject gameOverPanel;
    public GameObject timerPanel;
    private bool startTimer = false;
    void Update()
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
                    }
                    else if (remainingTime < 50)
                    {
                        timerText.color = Color.yellow;
                    }
                }
                else if (remainingTime < 0)
                {
                    remainingTime = 0;
                    // GameOver
                    gameOverPanel.SetActive(true);
                    Time.timeScale = 0;
                }
                int minutes = Mathf.FloorToInt(remainingTime / 60);
                int seconds = Mathf.FloorToInt(remainingTime % 60);
                timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
                break;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            startTimer = true;
            timerPanel.SetActive(true);
        }
        
    }
}
