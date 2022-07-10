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

        Debug.Log("Dropou algo na tag " + this.tag);
        //object being drag
        if (eventData.pointerDrag != null && dragDrop != null)
        {
            if (this.CompareTag("Backlog") || this.CompareTag("Done"))
            {
                dragDrop.DropCorrectly = false;

                return;
            }            

            dragDrop.DropCorrectly = true;

            var duplicate = dragDrop.GetDuplicate();

            if (this.CompareTag("ToDo"))
            {
                Debug.Log("Removeu dono");
                var card = duplicate.GetComponent<Card>();
                card.SetStatus(CardStatus.ToDo);
                card.RemoveOwner();                
            }

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
