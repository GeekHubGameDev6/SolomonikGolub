using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MemoryGame
{
    public class Card : MonoBehaviour
    {
        public static bool DO_NOT = false;

        [SerializeField]
        private int state;
        [SerializeField]
        private int cardValue = 0;
        [SerializeField]
        private bool initialized = false;

        private Sprite cardBack;
        private Sprite cardFace;

        private GameObject manager;

        public int CardValue
        {
            get
            {
                return cardValue;
            }
            internal set
            {
                cardValue = value;
            }
        }

        public int State
        {
            get
            {
                return state;
            }
            set
            {
                state = value;
            }
        }

        public bool Initialized
        {
            get
            {
                return initialized;
            }
            set
            {
                initialized = value;
            }
        }

        private void Awake()
        {
            state = 1;
            manager = GameObject.FindGameObjectWithTag("MemoryGameManager");
        }

        public void FlipCard()
        {
            if (state == 0)
                state = 1;
            else if (state == 1)
                state = 0;

            if (state == 0 && !DO_NOT)
                GetComponent<Image>().sprite = cardBack;
            else if (state == 1 && !DO_NOT)
                GetComponent<Image>().sprite = cardFace;
        }

        internal void FalseCheck()
        {
            StartCoroutine(Pause());
        }

        private IEnumerator Pause()
        {
            yield return new WaitForSeconds(0.5f);
            if (state == 0)
                GetComponent<Image>().sprite = cardBack;
            else if (state == 1)
                GetComponent<Image>().sprite = cardFace;
            DO_NOT = false;
        }

        internal void SetupGraphic()
        {
            cardBack = manager.GetComponent<MemoryGameManager>().GetCardBack();
            cardFace = manager.GetComponent<MemoryGameManager>().GetCardFace(cardValue);

            FlipCard();
        }
    }
}
