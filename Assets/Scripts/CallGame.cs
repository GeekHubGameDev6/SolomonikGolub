using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallGame : MonoBehaviour {
    public GameObject childNumber;
    public GameType game;

    private void OnMouseDown()
    {
        GameManager.Instance.StartMiniGame(game, childNumber);
    }
   
}
