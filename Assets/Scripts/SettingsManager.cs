using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    public static SettingsManager Instance { get; set; }

    private Difficulty _currentDifficulty;

    public Difficulty CurrentDifficulty
    {
        get { return _currentDifficulty; }
        set { _currentDifficulty = value; }
    }

    public string[] keys = new string[Enum.GetNames(typeof(SettingsProperty)).Length];

    private int _currentGameLevel;

    public int CurrentGameLevel
    {
        get { return _currentGameLevel; }
        set { _currentGameLevel = value; }
    }


    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != null && Instance != this)
            Destroy(this.gameObject);

        DontDestroyOnLoad(this.gameObject);
    }

    private void LoadStats()
    {
        print("loadig");
        _currentGameLevel = PlayerPrefs.GetInt("gameLevel", 0);
        _currentDifficulty = (Difficulty)PlayerPrefs.GetInt("dificulty", 0);
    }

    private void SaveStats()
    {
        print("saving");
        PlayerPrefs.SetInt("gameLevel", _currentGameLevel);
        PlayerPrefs.SetInt("dificulty", (int)_currentDifficulty);
    }

    private void OnEnable()
    {
        LoadStats();
    }
    private void OnDestroy()
    {
        SaveStats();
    }

    public enum SettingsProperty
    {
        RoomLevel = 0,
        Difficulty
    }

    public enum Difficulty
    {
        Easy,
        Medium,
        Hard
    }

}
