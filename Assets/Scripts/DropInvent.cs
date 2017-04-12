using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropInvent : MonoBehaviour, IDropHandler
{

    public void OnDrop(PointerEventData eventData)
    {
        DragInvent drag = eventData.pointerDrag.GetComponent<DragInvent>();
        if (drag != null)
        {

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                DropInvent dropPlace = hit.collider.GetComponent<DropInvent>();
                Debug.Log("drop is "+ hit.collider);

                if (dropPlace != null)
                {
                }
            }



                    drag.transform.SetParent(transform);
        }
    }
}
