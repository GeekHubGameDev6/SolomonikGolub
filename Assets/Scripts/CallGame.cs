using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallGame : MonoBehaviour {
    public GameObject childNumber;
    public GameType game;
    public bool selfdisable = false;

    private void OnMouseDown()
    {
        GameManager.Instance.StartMiniGame(game, childNumber);
        if (selfdisable)
        {
            gameObject.SetActive(false);
        }
    }
   
}
