using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour, IPointerDownHandler, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [SerializeField] private Canvas canvas;

    private GameObject duplicate;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    public bool DropCorrectly = false;

    public void OnBeginDrag(PointerEventData eventData)
    {
        duplicate = Instantiate(gameObject);
        duplicate.transform.position = gameObject.transform.position;

        canvasGroup = duplicate.GetComponent<CanvasGroup>();
        canvasGroup.alpha = .6f;

        rectTransform = duplicate.GetComponent<RectTransform>();
        rectTransform.sizeDelta = GetComponent<RectTransform>().sizeDelta;

        canvasGroup.blocksRaycasts = false;

        duplicate.transform.SetParent(canvas.transform);
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;

        if(!DropCorrectly)
        {
            Destroy(duplicate);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //não implementar
    }

    public GameObject GetDuplicate() => duplicate;
}
