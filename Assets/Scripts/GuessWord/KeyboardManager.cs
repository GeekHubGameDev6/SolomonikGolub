using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace GuessWord
{
    public class KeyboardManager : MonoBehaviour
    {

        public GameObject _buttonPrefab;

        private string _alphabet;

        public Text _inputField;

        public int keysInARow;
        public int keysInAColumn;
        public float buttonOffset;
        private bool firstClick;

        private List<Button> _gameKeyboard = new List<Button>();

        public void InitValues(string alphabet)
        {
            this._alphabet = alphabet;
            firstClick = true;
        }

        public void InitKeyboard(RectTransform parent)
        {
            if (_gameKeyboard.Count <= 0)
            {
                float keyWidth = (parent.rect.width - buttonOffset) / keysInARow ;
                float keyHeight = (parent.rect.height - buttonOffset) / keysInAColumn ;
                bool beforeSpace = true;

                for (int i = 0; i < _alphabet.Length; i++)
                {
                    InstantiateButton(i, keyWidth, keyHeight, _alphabet[i].ToString(), ref beforeSpace, parent);
                }

            }
            else
            {
                foreach (var item in _gameKeyboard)
                {
                    item.interactable = true;
                    item.transform.GetChild(0).GetComponent<Text>().color = GuessWordManager.Instance.textColors[(int)TextColor.Default];
                }
            }
        }

        private void InstantiateButton(int i, float keyWidth, float keyHeight, string letter, ref bool beforeSpace, RectTransform parent)
        {
            Button _newBtn = Instantiate(_buttonPrefab).GetComponent<Button>();

            _newBtn.gameObject.name = letter;
            _newBtn.gameObject.transform.GetChild(0).GetComponent<Text>().text = letter;

            // Adding onClick event
            _newBtn.onClick.AddListener(() => OnKeyboardBtnClick(i));

            RectTransform btnRect = _newBtn.GetComponent<RectTransform>();

            // Check if space initialized
            btnRect.transform.localPosition = new Vector2(
                (beforeSpace) ? 
                (i % keysInARow) * keyWidth + buttonOffset :                //  X value before space
                (i % keysInARow) * keyWidth + keyWidth * 3 + buttonOffset,  //  X value after space
                (i / keysInARow) * -keyHeight - buttonOffset);              //  Y value

            btnRect.transform.localScale = Vector2.one;

            // making space biger
            if(letter == " ")
            {
                _newBtn.gameObject.name = "space";
                keyWidth *= 4;
                //_newBtn.image.type = Image.Type.Sliced;
                beforeSpace = false;
                _newBtn.interactable = false;
            }

            btnRect.sizeDelta = new Vector2(keyWidth - buttonOffset, keyHeight - buttonOffset);

            btnRect.transform.SetParent(parent, false);
            _gameKeyboard.Add(_newBtn);
        }

        private void OnKeyboardBtnClick(int i)
        {
            _gameKeyboard[i].interactable = false;
            _gameKeyboard[i].transform.GetChild(0).GetComponent<Text>().color =
                (GuessWordManager.Instance.CheckClick(i)) ?
                GuessWordManager.Instance.textColors[(int)TextColor.Right] :
                GuessWordManager.Instance.textColors[(int)TextColor.Wrong];
        }
        public void DeactivateKeyboard()
        {
            foreach (var item in _gameKeyboard)
            {
                item.interactable = false;
            }
        }
    }
}
