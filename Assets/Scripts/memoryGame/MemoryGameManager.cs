using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MemoryGame
{
    public class MemoryGameManager : MonoBehaviour
    {
        public Sprite[] cardFaces;
        public Sprite cardBack;
        public GameObject[] cards;

        public Text matchText;

        private bool init = false;

        [SerializeField]
        private int matches = 13;

        private void Update()
        {
            if (!init)
                InitializeCards();

            if (Input.GetMouseButtonUp(0))
            {
                CheckCards();
            }
        }

        private void CheckCards()
        {
            List<int> c = new List<int>();

            for (int i = 0; i < cards.Length; i++)
            {
                if (cards[i].GetComponent<Card>().State == 1)
                    c.Add(i);
            }

            if (c.Count == 2)
            {
                CardComparison(c);
            }
        }

        private void CardComparison(List<int> c)
        {
            Card.DO_NOT = true;

            int x = 0;

            if (cards[c[0]].GetComponent<Card>().CardValue == cards[c[1]].GetComponent<Card>().CardValue)
            {
                x = 2;
                matches--;
                matchText.text = "Number of Matches: " + matches;

                if (matches == 0)
                    UnityEngine.SceneManagement.SceneManager.LoadScene("memoryGame");
            }

            for (int i = 0; i < c.Count; i++)
            {
                cards[c[i]].GetComponent<Card>().State = x;
                cards[c[i]].GetComponent<Card>().FalseCheck();
            }
        }

        private void InitializeCards()
        {
            for (int id = 0; id < 2; id++)
            {
                for (int i = 1; i < matches + 1; i++)
                {
                    bool test = false;
                    int choice = 0;

                    while (!test)
                    {
                        choice = UnityEngine.Random.Range(0, cards.Length);
                        test = !(cards[choice].GetComponent<Card>().Initialized);
                    }

                    cards[choice].GetComponent<Card>().CardValue = i;
                    cards[choice].GetComponent<Card>().Initialized = true;
                }
            }

            foreach (var item in cards)
            {
                item.GetComponent<Card>().SetupGraphic();
            }

            if (!init)
                init = true;
        }

        internal Sprite GetCardFace(int i)
        {
            if (i <= 0)
                i = 1;

            return cardFaces[i - 1];
        }

        internal Sprite GetCardBack()
        {
            return cardBack;
        }
    }
}
