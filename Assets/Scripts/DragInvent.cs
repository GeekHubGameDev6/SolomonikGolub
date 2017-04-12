using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class DragInvent : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    Transform canvas;
    Transform old;

    // Use this for initialization
    void Start () {
        canvas = GameObject.Find("Canvas").transform;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        GetComponent<CanvasGroup>().blocksRaycasts = false;
        old = transform.parent;
        transform.SetParent(canvas);
    }


    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }


    public void OnEndDrag(PointerEventData eventData)
    {
        if (transform.parent == canvas)
        {

            transform.SetParent(old);
        }
        GetComponent<CanvasGroup>().blocksRaycasts = true;
    }

}
