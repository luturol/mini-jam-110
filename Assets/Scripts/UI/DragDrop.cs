using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour, IPointerDownHandler, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [SerializeField] private Canvas _canvas;

    private GameObject _duplicate;
    private RectTransform _rectTransform;
    private CanvasGroup _canvasGroup;
    public bool DropCorrectly = false;

    private void Start()
    {
        _canvas = FindObjectOfType<Canvas>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _duplicate = Instantiate(gameObject);
        _duplicate.transform.position = gameObject.transform.position;

        var cardNew = _duplicate.GetComponent<Card>();
        var cardOld = GetComponent<Card>();

        if (cardNew && cardOld)
        {
            cardNew.IsDuplicate = true;
            _duplicate.GetComponent<Card>().DuplicateValues(cardOld);
        }

        _canvasGroup = _duplicate.GetComponent<CanvasGroup>();
        _canvasGroup.alpha = .6f;

        _rectTransform = _duplicate.GetComponent<RectTransform>();
        _rectTransform.sizeDelta = GetComponent<RectTransform>().sizeDelta;

        _canvasGroup.blocksRaycasts = false;

        _duplicate.transform.SetParent(_canvas.transform);
    }

    public void OnDrag(PointerEventData eventData)
    {
        _rectTransform.anchoredPosition += eventData.delta / _canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _canvasGroup.alpha = 1f;
        _canvasGroup.blocksRaycasts = true;

        if (!DropCorrectly)
        {
            Destroy(_duplicate);
        }
        else
        {
            //Não permitir mover
            // var card = eventData.pointerDrag.GetComponent<Card>();

            // if (card && card.IsBlocked())
            // {
            //     Destroy(_duplicate);
            // }
            // else
            // {
                Destroy(gameObject);
            // }

        }

        DropCorrectly = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //não implementar
    }

    public GameObject GetDuplicate() => _duplicate;
}
