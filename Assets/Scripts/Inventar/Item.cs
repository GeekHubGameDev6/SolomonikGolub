using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {

    public string icon;
    public string prefab;
    public Inventar inventar;

    private void OnMouseDown()
    {
      //  GameManager.Instance.StartMiniGame(GameType.PuzzleGame);
        Debug.Log("Have done " + this);

        inventar.addItem(this);
            }

}
