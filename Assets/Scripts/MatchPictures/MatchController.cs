using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MatchController : MonoBehaviour
{

    public Sprite[] mainSprites;
    public Sprite[] spritesForMatch;

    public Transform upperParent;
    public Transform holdersParent;

    public Transform lowerParent;

    public GameObject upperImagePrefab;
    public GameObject holderPrefab;

    public GameObject lowerImagePrefab;

    public GameObject resultPanel;
    public Text resultText;
    public Color right, wrong;
    bool result=false;

    private void Start()
    {
        resultPanel.SetActive(false);

    }

    private void CreateObjects(GameObject prefab, int count, Transform parent, string[] initValue)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject go = GameObject.Instantiate(prefab);
            go.GetComponent<Text>().text = initValue[i];
            go.transform.SetParent(parent);
        }
    }

    public void OnCheckBtnClick()
    {
        for (int i = 0; i < upperParent.childCount; i++)
        {
            if ((int)holdersParent.GetChild(i).childCount <= 0 || ((int)upperParent.GetChild(i).GetComponent<MatchAnimalItem>().animalType !=
                (int)holdersParent.GetChild(i).GetChild(0).GetComponent<MatchFoodItem>().foodType))
            {
                result = false;
                resultPanel.SetActive(true);
                resultText.text = "You have made a mistake, pls try again";
                resultText.color = wrong;
                return;
            }
        }

        Debug.Log("Win");
        result = true;
        resultPanel.SetActive(true);
        resultText.text = "Everything OK";
        resultText.color = right;
    }

    public bool getResult(){
        return result;
    }
}

public enum AnimalType
{
    Fish = 0,
    Dog,
    Cat,
    Bear,
    Horse
}

public enum FoodType
{
    Worm = 0,
    Bone,
    Fish,
    Honey,
    Grass
}
