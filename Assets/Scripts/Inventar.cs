using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventar : MonoBehaviour {
    List<Item> list;
    public GameObject inventory;
	// Use this for initialization
	void Start () {
        list = new List<Item>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonUp(1))
        {
            Debug.Log("mouse pressed");
        
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                Item item = hit.collider.GetComponent<Item>();
                Debug.Log(item);

                if (item != null)
                {
                    list.Add(item);
                    Destroy(hit.collider.gameObject);
                }
            }

        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            if (inventory.activeSelf)
            {
                inventory.SetActive(false);
                for (int i = 0; i < inventory.transform.childCount; i++)
                {
                    if (inventory.transform.GetChild(i).transform.childCount > 0)
                    {
                        Destroy(inventory.transform.GetChild(i).transform.GetChild(0).gameObject);
                    }
                }
            }
            else
            {
                inventory.SetActive(true);
                int count = list.Count;
                for (int i = 0; i < count; i++)
                {
                    Item it = list[i];
                    if (inventory.transform.childCount >= i)
                    {

                      //  GameObject img = Instantiate(Cont);
                      //  img.transform.SetParent(inventory.transform.GetChild(i).transform);
                      //  img.GetComponent<Image>().sprite = Resources.Load<Sprite>(it.sprite);
                      //  img.AddComponent<Button>().onClick.AddListener(() => remove(it, img));




                    }
                    else break;

                }
            }
        }


    }
    void remove(Item it, GameObject obj)
    {


        GameObject newo = Instantiate<GameObject>(Resources.Load<GameObject>(it.prefab));
        Destroy(newo.GetComponent<Rigidbody>());
        newo.transform.SetParent(GameObject.Find("MainCamera").transform);


        newo.transform.localPosition = GameObject.Find("MainCamera").transform.position - transform.up * 3 - transform.forward * 1.6f;
        newo.transform.localRotation = GameObject.Find("MainCamera").transform.rotation;










        Destroy(obj);
        list.Remove(it);



    }
}
