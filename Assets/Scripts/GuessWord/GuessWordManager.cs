using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GuessWord
{
    public class GuessWordManager : MonoBehaviour
    {
        public static GuessWordManager Instance;
        private string _alphabet = "ABCDEFGHIJKLMNOPQRSTUVW XYZ";

        GameState state;

        public Color[] textColors;

        public RectTransform _keyboardPanel;
        public GameObject _letterPrefab;
        public KeyboardManager _keyboardManager;

        private string _temp;
        private int _triesAmn = 7;
        private int _fails = 0;
        private int _matches;

        public Image _imageToShow;
        public Text _textToShow;
        public string wordToGuess;
        private char _hider = '*';

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else if (!(Instance == null && Instance == this))
            {
                Destroy(this.gameObject);
            }
        }
        private void Start()
        {
            StartGame();
        }

        private void StartGame()
        {
            _fails = 0;
            _keyboardManager.InitValues(_alphabet);
            _keyboardManager.InitKeyboard(_keyboardPanel);
            //Set random Image
            //GetImageName
            wordToGuess = _imageToShow.sprite.name.ToUpper();
            Debug.Log(wordToGuess);
            _textToShow.text = new string(_hider, wordToGuess.Length);
        }



        internal bool CheckClick(int i)
        {
            if (wordToGuess.IndexOf(_alphabet[i]) >= 0)
            {
                //  if selected letter exist in array
                //  show all letters in word

                string showWord = "";

                for (int b = 0; b < _textToShow.text.Length; b++)
                {
                    if (_textToShow.text[b] == _hider)
                        if (wordToGuess[b] == _alphabet[i])
                            showWord += _alphabet[i];
                        else
                            showWord += _hider;
                    else
                        showWord += _textToShow.text[b];
                }
                _textToShow.text = showWord;
                if (showWord.IndexOf(_hider) == -1)
                {
                    Debug.Log("GameOver, Game Won!");
                    _keyboardManager.DeactivateKeyboard();
                }
                return true;
            }
            else
            {
                _fails++;
                if (_fails >= _triesAmn)
                {
                    Debug.Log("GameOVer, you lost!");
                    _keyboardManager.DeactivateKeyboard();
                }
            }

            return false;
        }
    }
    public enum TextColor
    {
        Default = 0,
        Right = 1,
        Wrong = 2
    }
    public enum GameState
    {
        Idle,
        Playing,
        GameOVer
    }
}
