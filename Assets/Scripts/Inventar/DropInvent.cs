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
            Debug.Log("drag is" + drag);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                DropInvent dropPlace = hit.collider.GetComponent<DropInvent>();
                Debug.Log("drop is "+ hit.collider);

                if (dropPlace != null)
                {
                    Debug.Log("drop place is" + dropPlace);
                    
                }
            }

            drag.transform.SetParent(transform);
        }
    }

    private void OnMouseUp()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            DropInvent dropPlace = hit.collider.GetComponent<DropInvent>();
            Debug.Log("drop on mouse is " + hit.collider);
        }
            Debug.Log("on mouse up");
    }
}
