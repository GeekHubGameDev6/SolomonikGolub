using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MemoryGame
{
    public class CardPooler : MonoBehaviour
    {

        public GameObject CardPrefab;
        private int _intCount = 8;
        private List<Card> CardList;

        private void Start()
        {
            for (int i = 0; i < _intCount; i++)
            {
                GetNewCard();
            }
        }

        public Card GetCard()
        {
            for (int i = 0; i < CardList.Count; i++)
            {
                if (!CardList[i].isActiveAndEnabled)
                    return CardList[i];
            }
            return GetNewCard();
        }

        private Card GetNewCard()
        {
            GameObject go = Instantiate(CardPrefab);
            go.SetActive(false);
            CardList.Add(go.GetComponent<Card>());
            return go.GetComponent<Card>();
        }
    }
}
