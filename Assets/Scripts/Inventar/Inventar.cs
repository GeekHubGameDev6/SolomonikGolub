using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventar : MonoBehaviour {
    List<Item> list;
    public GameObject inventory;
    public GameObject inventIco;

	// Use this for initialization
	void Start () {
        list = new List<Item>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonUp(1))
        {
            
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                Item item = hit.collider.GetComponent<Item>();
                Debug.Log(item);

                if (item != null)
                {
                    list.Add(item);
                    GameObject img = Instantiate(inventIco);
                    img.transform.SetParent(inventory.transform.GetChild(list.Count-1).transform);
                    img.GetComponent<Image>().sprite = Resources.Load<Sprite>(item.icon);
                  //  img.AddComponent<Button>().onClick.AddListener(() => remove(item, img));
                     Destroy(hit.collider.gameObject);
                 //    hit.collider.gameObject.SetActive(false);
                }
            }

        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            if (inventory.activeSelf)
            {
                inventory.SetActive(false);
            
            }
            else
            {
                inventory.SetActive(true);
          
            }
        }


    }

    public void addItem (Item it)
    {
        list.Add(it);
        GameObject img = Instantiate(inventIco);
       // Debug.Log("invnt" + list.Count + img);
        img.transform.SetParent(inventory.transform.GetChild(list.Count - 1).transform);
        img.GetComponent<Image>().sprite = Resources.Load<Sprite>(it.icon);
        img.AddComponent<Button>().onClick.AddListener(() => remove(it, img));
        Destroy(it.gameObject);
    }


    void remove(Item it, GameObject obj)
    {
        Debug.Log("entered in remove");

        GameObject newo = Instantiate<GameObject>(Resources.Load<GameObject>(it.prefab));

        Debug.Log(newo);
        
        GameObject cam = GameObject.FindGameObjectWithTag("MainCamera");
        Debug.Log("camera is "+cam+ cam.transform.position);

        newo.transform.SetParent(cam.transform);

        newo.transform.position = new Vector3(0, 10, 100);
      //  newo.transform.position = cam.transform.position - transform.up*10 + transform.forward*10 ;
        //newo.transform.localRotation = cam.transform.rotation;
        list.Remove(it);

        Destroy(obj);
        list.Remove(it);

        
    }
}
