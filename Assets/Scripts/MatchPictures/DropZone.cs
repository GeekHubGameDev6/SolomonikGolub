using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropZone : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    bool main;
    public void OnDrop(PointerEventData eventData)
    {
        Dragable d = eventData.pointerDrag.GetComponent<Dragable>();
        if (d != null && this.transform.childCount > 0)
        {
            d.parentToReturnTo = this.transform;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null || this.transform.childCount > 0)
            return;

        Dragable d = eventData.pointerDrag.GetComponent<Dragable>();
        if(d != null)
        {
            d.placeHolderParent = this.transform;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null || this.transform.childCount > 0)
            return;

        Dragable d = eventData.pointerDrag.GetComponent<Dragable>();
        if(d != null && d.placeHolderParent == this.transform)
        {
            d.placeHolderParent = d.parentToReturnTo;
        }
    }
}
