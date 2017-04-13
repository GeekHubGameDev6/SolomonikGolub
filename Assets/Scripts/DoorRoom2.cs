using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorRoom2 : MonoBehaviour {

    public GameObject door;

    private void OnTriggerEnter(Collider other)
    {
        door.GetComponent<Animator>().Play("DoorRoom2_open");
    }

    private void OnTriggerExit (Collider other)
    {
        door.GetComponent<Animator>().Play("DoorRoom2_close");
    }
}
