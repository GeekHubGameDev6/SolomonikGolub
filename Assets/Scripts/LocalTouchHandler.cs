using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalTouchHandler : MonoBehaviour {
    public GameObject canvasName;

    private void OnMouseDown()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().enabled = false;
        GameObject[] tmans = GameObject.FindGameObjectsWithTag("TouchManager");
        foreach (GameObject tman in tmans)
        {
            tman.SetActive(false);
        }
     canvasName.SetActive(true);
       
    }
}
