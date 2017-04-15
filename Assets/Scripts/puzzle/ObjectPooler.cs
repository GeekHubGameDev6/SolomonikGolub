using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour, IPooler
{
    public PuzzleSelection objectPrefab;
    public int startAmn;
    private List<PuzzleSelection> _objList = new List<PuzzleSelection>();

    private void Start()
    {
        for (int i = 0; i < startAmn; i++)
        {
            PuzzleSelection go = Instantiate(objectPrefab);
            go.gameObject.SetActive(false);
            go.transform.SetParent(this.transform);
            _objList.Add(go);
        }
    }

    public PuzzleSelection GetPooledObject()
    {
        for (int i = 0; i < _objList.Count; i++)
        {
            if (!_objList[i].gameObject.activeInHierarchy)
            {
                return _objList[i];
            }
        }

        return AddNewGameObjcet();
    }

    private PuzzleSelection AddNewGameObjcet()
    {
        PuzzleSelection go = Instantiate(objectPrefab);
        go.gameObject.SetActive(false);
        _objList.Add(go);
        return go;
    }

    public void DeactivateObjects()
    {
        for (int i = 0; i < _objList.Count; i++)
        {
            if (_objList[i].gameObject.activeInHierarchy)
            {
                _objList[i].transform.SetParent(this.transform);
                _objList[i].transform.localScale = Vector3.one;
                _objList[i].gameObject.SetActive(false);
            }
        }
    }
}
