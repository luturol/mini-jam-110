using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Lane : MonoBehaviour, IDropHandler
{
    private VerticalLayoutGroup _content;

    public void OnDrop(PointerEventData eventData)
    {
        var dragDrop = eventData.pointerDrag.GetComponent<DragDrop>();

        //object being drag
        if (eventData.pointerDrag != null && dragDrop != null)
        {
            if (this.CompareTag("Backlog"))
            {
                dragDrop.DropCorrectly = false;
                
                return;
            }
            
            Debug.Log(dragDrop);
            dragDrop.DropCorrectly = true;

            var duplicate = dragDrop.GetDuplicate();

            duplicate.transform.SetParent(_content.transform);
            duplicate.transform.localScale = new Vector3(1, 1, 1);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        _content = GetComponentInChildren<VerticalLayoutGroup>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
