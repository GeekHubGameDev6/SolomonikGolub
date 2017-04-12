using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Dragable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{

    private Vector2 offset;

    public Transform parentToReturnTo = null;
    public Transform placeHolderParent = null;

    private GameObject placeHolder = null;


    public void OnBeginDrag(PointerEventData eventData)
    {
        placeHolder = new GameObject();
        placeHolder.transform.SetParent(this.transform.parent);

        LayoutElement le = placeHolder.AddComponent<LayoutElement>();
        le.preferredWidth = this.GetComponent<LayoutElement>().preferredWidth;
        le.preferredHeight = this.GetComponent<LayoutElement>().preferredHeight;
        le.flexibleWidth = 0;
        le.flexibleHeight = 0;

        placeHolder.transform.SetSiblingIndex(this.transform.GetSiblingIndex());

        parentToReturnTo = this.transform.parent;
        placeHolderParent = parentToReturnTo;
        offset = this.transform.position - new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z);
        this.transform.SetParent(this.transform.parent.parent);
        this.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform);

        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        this.transform.position = offset + eventData.position;

        if (placeHolder.transform.parent != placeHolderParent)
            placeHolder.transform.SetParent(placeHolderParent);

        int newSiblinIndex = placeHolderParent.childCount;

        for (int i = 0; i < placeHolderParent.childCount; i++)
        {
            if (this.transform.position.x < placeHolderParent.GetChild(i).transform.position.x)
            {
                newSiblinIndex = i;
                if (placeHolder.transform.GetSiblingIndex() < newSiblinIndex)
                    newSiblinIndex--;

                break;
            }
        }

        placeHolder.transform.SetSiblingIndex(newSiblinIndex);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        this.transform.parent = parentToReturnTo;
        this.transform.SetSiblingIndex(placeHolder.transform.GetSiblingIndex());

        GetComponent<CanvasGroup>().blocksRaycasts = true;
        Destroy(placeHolder);
    }
}

