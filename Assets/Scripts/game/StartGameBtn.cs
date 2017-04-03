using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartGameBtn : MonoBehaviour
{
    public GameType gameType;

    public float msToWait = 5000.0f;

    private Text _timerText;
    private Button _button;
    private ulong _lastBtnClick;
    private string _saveKey = "";

    private void Start()
    {
        _saveKey = gameType.ToString() + "Played";
        _button = GetComponent<Button>();
        _timerText = GetComponentInChildren<Text>();
        _lastBtnClick = ulong.Parse(PlayerPrefs.GetString(_saveKey,"0"));

        if (!IsGameReady())
            _button.interactable = false;
    }
    private void Update()
    {
        if (!_button.IsInteractable())
        {
            if (IsGameReady())
            {
                _button.interactable = true;
                _timerText.text = gameType.ToString();
                return;
            }
            //  Set the timer here

            ulong diff = (ulong)DateTime.Now.Ticks - _lastBtnClick;
            ulong m = diff / TimeSpan.TicksPerMillisecond;
            float secondsLeft = (msToWait - m) / 1000.0f;

            string r = "";
            //  Minutes
            r += ((int)secondsLeft / 60).ToString("00") + " min ";
            //  Seconds
            r += (secondsLeft % 60).ToString("00") + " sec left";
            _timerText.text = r;
        }

        
    }

    public void OnBtnCLick()
    {
        _lastBtnClick = (ulong)DateTime.Now.Ticks;
        PlayerPrefs.SetString(_saveKey, _lastBtnClick.ToString());
        _button.interactable = false;

        // Start Game here
        GameManager.Instance.StartMiniGame(gameType);        
    }

    /// <summary>
    /// return true if game is ready to play
    /// </summary>
    /// <returns></returns>
    private bool IsGameReady()
    {
        ulong diff = ((ulong)DateTime.Now.Ticks - _lastBtnClick);
        ulong m = diff / TimeSpan.TicksPerMillisecond;

        float secondsLeft = (msToWait - m) / 1000.0f;       

        if (secondsLeft < 0)
        {
            _timerText.text = gameType.ToString();
            return true;
        }

        return false;
    }
}
