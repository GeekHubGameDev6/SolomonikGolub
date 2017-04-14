using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalTouchHandler : MonoBehaviour {
    public GameObject canvasName;

    private void OnMouseDown()
    {
     setTouchManagerEnabled(false); 
     canvasName.SetActive(true);
       
    }

   public  void setTouchManagerEnabled(bool b)
    {
 GameObject.FindGameObjectWithTag("Player").GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().enabled = b;
        GameObject[] tmans = GameObject.FindGameObjectsWithTag("TouchManager");
        foreach (GameObject tman in tmans)
        {
            Debug.Log(tman + " is " + b);
            tman.SetActive(b);
        }
     }

    public void  closeGameCanvas(bool isWin)
    {
        Debug.Log("closeGameCanvas " +isWin);
        if (isWin)
        {
            setTouchManagerEnabled(true);
            canvasName.SetActive(false);
            
        }

    }

}
