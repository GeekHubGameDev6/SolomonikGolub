using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MemoryGame
{
    public class MemoryGameManager : MonoBehaviour
    {
        public GameObject cardPrefab;
        public int totalCards = 26;
        private bool _started = false;

        [SerializeField]
        private float _cardOffset;
        [SerializeField]
        private int _cardsInARow;
        [SerializeField]
        private int _cardsInAColumn;

        [SerializeField]
        private RectTransform _cardsParent;

        public Sprite[] CardFaces;
        public Sprite CardBack;

        //public GameObject[] Cards;
        public List<Card> Cards;

        public Text MatchText;

        private bool _init = false;

        [SerializeField]
        private int _matches = 13;

        private void Start()
        {
            InstantiateCards();
        }

        private void Update()
        {
            if (!_started)
                return;

            // if (!_init)

            if (Input.GetMouseButtonUp(0))
            {
                CheckCards();
            }
        }        

        public void InstantiateCards()
        {
            _started = false;
            Cards = new List<Card>();
            float cardWidth = (_cardsParent.rect.width - _cardOffset) / _cardsInARow;
            float cardHeight = (_cardsParent.rect.height - _cardOffset) / _cardsInAColumn;

            if (totalCards % 2 != 0)
            {
                totalCards++;
                _matches = totalCards / 2;
                MatchText.text = "Matches left : " + _matches;

                // SetColmnsAndRows();
            }

            for (int i = 0; i < totalCards; i++)
            {
                InstantiateCard(i, cardWidth, cardHeight, _cardsParent);
            }

            _started = true;
            InitializeCards();

        }

        private void SetColmnsAndRows()
        {
            if (totalCards > 8)
            {
                _cardsInARow = totalCards / 3;
                _cardsInAColumn = _cardsInARow / 3;
            }
            else
            {
                _cardsInARow = totalCards / 2;
                _cardsInAColumn = _cardsInARow / 2;
            }
        }

        private void InstantiateCard(int i, float width, float height, RectTransform parent)
        {
            Card newCard = Instantiate(cardPrefab).GetComponent<Card>();
            Cards.Add(newCard);

            newCard.name = "Card " + i;
            newCard.rt.localPosition = new Vector2(
                i % _cardsInARow * width + _cardOffset,
                i / _cardsInARow * -height - _cardOffset);
            newCard.transform.localScale = Vector2.one;

            newCard.rt.sizeDelta = new Vector2(width - _cardOffset, height - _cardOffset);
            newCard.rt.SetParent(parent, false);
        }



        private void CheckCards()
        {
            List<int> c = new List<int>();

            for (int i = 0; i < Cards.Count; i++)
            {
                if (Cards[i].State == CardState.Opened)
                    c.Add(i);
            }

            if (c.Count == 2)
            {
                CardComparison(c);
            }
        }

        private void CardComparison(List<int> c)
        {
            Card.DoNot = true;

            var x = CardState.Closed;

            if (Cards[c[0]].CardValue == Cards[c[1]].CardValue)
            {
                x = CardState.Static;
                _matches--;
                MatchText.text = "Matches left : " + _matches;

                if (_matches == 0)
                    UnityEngine.SceneManagement.SceneManager.LoadScene("memoryGame");
            }

            for (int i = 0; i < c.Count; i++)
            {
                Cards[c[i]].State = x;
                Cards[c[i]].FalseCheck();
            }
        }

        private void InitializeCards()
        {
            for (int id = 0; id < 2; id++)
            {
                for (int i = 1; i < _matches + 1; i++)
                {
                    bool test = false;
                    int choice = 0;

                    while (!test)
                    {
                        choice = UnityEngine.Random.Range(0, Cards.Count);
                        test = !(Cards[choice].Initialized);
                    }

                    Cards[choice].CardValue = i;
                    Cards[choice].Initialized = true;
                }
            }

            foreach (var item in Cards)
            {
                item.SetupGraphic();
            }

            if (!_init)
                _init = true;
        }

        internal Sprite GetCardFace(int i)
        {
            if (i <= 0)
                i = 1;

            if (i > CardFaces.Length)
                i = CardFaces.Length;

            return CardFaces[i - 1];
        }

        internal Sprite GetCardBack()
        {
            return CardBack;
        }

        public void OnStartBtnClick()
        {
            InstantiateCards();
        }
    }
}
