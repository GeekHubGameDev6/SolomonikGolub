using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {

    public string icon;
    public string prefab;
    public Inventar inventar;

    private void OnMouseDown()
    {
        Debug.Log(this);
        inventar.addItem(this);
            }

}
