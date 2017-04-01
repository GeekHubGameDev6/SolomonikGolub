using System.Collections;
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

        private GameObject _manager;

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
            _manager = GameObject.FindGameObjectWithTag("MemoryGameManager");
        }

        public void ResetCart()
        {
            _cardValue = 0;
            _initialized = false;
        }

        public void FlipCard()
        {

            if(_state == CardState.Closed)
                _state = CardState.Opened;
            else if(_state == CardState.Opened)
                _state = CardState.Closed;

            if (State == CardState.Closed && !DoNot)
                GetComponent<Image>().sprite = _cardBack;
            else if (State == CardState.Opened && !DoNot)
                GetComponent<Image>().sprite = _cardFace;
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
            _cardBack = _manager.GetComponent<MemoryGameManager>().GetCardBack();
            _cardFace = _manager.GetComponent<MemoryGameManager>().GetCardFace(_cardValue);

            FlipCard();
        }
    }

    public enum CardState
    {
        Closed,
        Opened,
        Static
    }
    interface ICardPooler
    {
        Card GetCard();
    }
}
