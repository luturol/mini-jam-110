using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Lane : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        //object being drag
        if (eventData.pointerDrag != null)
        {
            var dragDrop = eventData.pointerDrag.GetComponent<DragDrop>();
            dragDrop.DropCorrectly = true;
            
            var duplicate = dragDrop.GetDuplicate();
            // var vial = duplicate.GetComponent<Vial>();
            
            duplicate.transform.SetParent(transform);
            
            // Debug.Log("Got the vial with " + vial.GetElement().type.ToString());
            // Mix(vial.GetElement());
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
