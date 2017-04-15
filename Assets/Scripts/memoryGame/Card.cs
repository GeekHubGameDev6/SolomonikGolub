using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace MemoryGame
{
    public class Card : MonoBehaviour
    {

        public static bool DoNot = false;

        [SerializeField]
        private CardState _state;
        [SerializeField]
        private int _cardValue = 0;
        [SerializeField]
        private bool _initialized = false;

        public RectTransform rt;

        private Sprite _cardBack;
        private Sprite _cardFace;

        private MemoryGameManager _manager;

        public int CardValue
        {
            get
            {
                return _cardValue;
            }
            internal set
            {
                _cardValue = value;
            }
        }

        public CardState State
        {
            get
            {
                return _state;
            }
            set
            {
                _state = value;
            }
        }

        public bool Initialized
        {
            get
            {
                return _initialized;
            }
            set
            {
                _initialized = value;
            }
        }

        private void Awake()
        {
            _state = CardState.Opened;
            _manager = GameObject.FindGameObjectWithTag("MemoryGameManager").GetComponent<MemoryGameManager>();
        }

        public void ResetCard()
        {
            name = "Card00";
            _initialized = false;
            _cardValue = -1;
            _state = CardState.Opened;
            gameObject.SetActive(false);
        }

        public void FlipCard()
        {
            if (_state == CardState.Closed)
                _state = CardState.Opened;
            else if (_state == CardState.Opened)
                _state = CardState.Closed;

            if (_state == CardState.Static)
                _state = CardState.Closed;

            if (State == CardState.Closed && !DoNot)
                GetComponent<Image>().sprite = _cardBack;
            else if (State == CardState.Opened && !DoNot)
                GetComponent<Image>().sprite = _cardFace;

            _manager.CheckCards();

        }

        internal void FalseCheck()
        {
            StartCoroutine(Pause());
        }

        private IEnumerator Pause()
        {
            yield return new WaitForSeconds(0.5f);

            if (_state == CardState.Closed)
                GetComponent<Image>().sprite = _cardBack;
            else if (_state == CardState.Opened)
                GetComponent<Image>().sprite = _cardFace;
            DoNot = false;
        }

        internal void SetupGraphic()
        {
            _cardBack = _manager.GetCardBack();
            _cardFace = _manager.GetCardFace(_cardValue);

            FlipCard();
        }
    }
}
