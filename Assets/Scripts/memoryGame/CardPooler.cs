using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MemoryGame
{
    public class CardPooler : MonoBehaviour, ICardPooler
    {
        public GameObject objectPrefab;
        private int _startAmn = 6;
        private List<GameObject> _cardList = new List<GameObject>();

        private void Start()
        {
            for (int i = 0; i < _startAmn; i++)
            {
                GameObject go = Instantiate(objectPrefab) as GameObject;
                go.gameObject.SetActive(false);
                go.transform.SetParent(this.transform);
                _cardList.Add(go);
            }
        }

        private GameObject AddNewGameObject()
        {
            GameObject go = Instantiate(objectPrefab) as GameObject;
            go.transform.SetParent(this.transform);
            go.gameObject.SetActive(false);
            _cardList.Add(go);
            return go;
        }

        public void DeactivateObjects()
        {
            for (int i = 0; i < _cardList.Count; i++)
            {
                if (_cardList[i].gameObject.activeSelf)
                {
                    _cardList[i].GetComponent<Card>().ResetCard();
                    _cardList[i].transform.SetParent(this.transform);
                    _cardList[i].gameObject.SetActive(false);
                }
            }
            Card.DoNot = false;
        }

        public GameObject GetPooledObject()
        {
            for (int i = 0; i < _cardList.Count; i++)
            {
                if (_cardList[i].activeSelf == false)
                {
                    _cardList[i].gameObject.SetActive(true);
                    return _cardList[i];
                }
            }

            return AddNewGameObject();
        }
    }
}
