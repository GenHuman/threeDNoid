using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerText : MonoBehaviour
{
    #region Singleton
    private static TimerText _instance;
    public static TimerText Instance { get { return _instance; } }
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }
    #endregion

    public Text secondsText;
    public float seconds = 30f;
    public bool timerOn = false;
    public bool textTimerOn = false;

    void Start()
    {
        //secondsText = GetComponent<Text>();
        secondsText.text = ((int)seconds).ToString();
    }

    // Update is called once per frame
    public bool toggleTimer()
    {
        timerOn = !timerOn;
        if (timerOn)
        {
            secondsText.color = Color.white;
        }
        else
        {
            secondsText.color = Color.grey;
        }
        return timerOn;
    }

    public bool toggleTimer(bool boo)
    {
        timerOn = boo;
        return timerOn;
    }

    public bool toggleTextTimer()
    {
        textTimerOn = !textTimerOn;
        
        return textTimerOn;
    }

    public bool toggleTextTimer(bool boo)
    {
        textTimerOn = boo;
        if (textTimerOn)
        {
            secondsText.color = Color.white;
        }
        else
        {
            secondsText.color = Color.grey;
        }
        return textTimerOn;
    }

    void Update()
    {
        
        if (timerOn)
        {

            seconds -= Time.deltaTime;
            if (textTimerOn && (int)seconds >= 0)
            {
                secondsText.text = ((int)seconds).ToString();
            }

            if (seconds <= 0)
            {
                toggleTextTimer();
                toggleTimer();
                
            }

        }
        
    }

    public void resetTimer(float s)
    {
        seconds = s;
        timerOn = true;
        //secondsText.color = Color.white;
        //secondsText.text = ((int)seconds).ToString();
    }

    public void resetTextTimer(float s)
    {
        seconds = s;
        timerOn = true;
        textTimerOn = true;
        secondsText.color = Color.white;
        secondsText.text = ((int)seconds).ToString();
    }

    public float checkTextTime()
    {
        return seconds;
    }

    public void stopTimer()
    {
        timerOn = false;
        textTimerOn = false;
        secondsText.color = Color.grey;
    }

    public void startTimer()
    {
        timerOn = true;
        textTimerOn = true;
        secondsText.color = Color.white;
    }
}
