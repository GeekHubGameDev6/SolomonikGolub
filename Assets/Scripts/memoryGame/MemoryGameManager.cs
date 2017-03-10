using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MemoryGame
{
    public class MemoryGameManager : MonoBehaviour
    {
        public Sprite[] CardFaces;
        public Sprite CardBack;
        public GameObject[] Cards;

        public Text MatchText;

        private bool _init = false;

        [SerializeField]
        private int _matches = 13;

        private void Update()
        {
            if (!_init)
                InitializeCards();

            if (Input.GetMouseButtonUp(0))
            {
                CheckCards();
            }
        }

        private void CheckCards()
        {
            List<int> c = new List<int>();

            for (int i = 0; i < Cards.Length; i++)
            {
                if (Cards[i].GetComponent<Card>().State == CardState.Opened)
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

            if (Cards[c[0]].GetComponent<Card>().CardValue == Cards[c[1]].GetComponent<Card>().CardValue)
            {
                x = CardState.Static;
                _matches--;
                MatchText.text = "Number of Matches: " + _matches;

                if (_matches == 0)
                    UnityEngine.SceneManagement.SceneManager.LoadScene("memoryGame");
            }

            for (int i = 0; i < c.Count; i++)
            {
                Cards[c[i]].GetComponent<Card>().State = x;
                Cards[c[i]].GetComponent<Card>().FalseCheck();
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
                        choice = UnityEngine.Random.Range(0, Cards.Length);
                        test = !(Cards[choice].GetComponent<Card>().Initialized);
                    }

                    Cards[choice].GetComponent<Card>().CardValue = i;
                    Cards[choice].GetComponent<Card>().Initialized = true;
                }
            }

            foreach (var item in Cards)
            {
                item.GetComponent<Card>().SetupGraphic();
            }

            if (!_init)
                _init = true;
        }

        internal Sprite GetCardFace(int i)
        {
            if (i <= 0)
                i = 1;

            return CardFaces[i - 1];
        }

        internal Sprite GetCardBack()
        {
            return CardBack;
        }
    }
}
