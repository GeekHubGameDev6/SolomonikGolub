using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialigController : MonoBehaviour
{
    public GameObject _dialogPanel;
    public Text _dialogText;
    public Button _nextBtn;
    private string[] _dialogString = new string[] { "Welcome", "All this game is about doors", "walk around, collect cristals, solve puzzles and get your reward.", "There is various of picture puzzles needs to be solved, so go ahead and enjoy.", "And by the way, dont forget to leave a feedback." };

    private int _textIndex = 0;

    public void Start()
    {
        ShowText();
    }

    private void ShowText()
    {
        _textIndex = 0;
        _dialogPanel.SetActive(true);
        _nextBtn.interactable = true;
        _dialogText.text = _dialogString[_textIndex++];
        _nextBtn.onClick.AddListener(() => OnNextBtnClick());
    }
    public void ShowText(string[] text)
    {
        _textIndex = 0;
        _dialogPanel.SetActive(true);
        _nextBtn.interactable = true;
        _dialogText.text = _dialogString[_textIndex++];
        _nextBtn.onClick.AddListener(() => OnNextBtnClick());
    }

    public void OnNextBtnClick()
    {
        //void showNextText;
        _dialogText.text = _dialogString[_textIndex++];
        Debug.Log("LookHere");

        if (_textIndex >= _dialogString.Length)
        {
            _nextBtn.onClick.RemoveAllListeners();
            _nextBtn.onClick.AddListener(() => HideText());
        }
    }



    private void HideText()
    {
        _dialogPanel.SetActive(false);
        _textIndex = 0;
    }
}
