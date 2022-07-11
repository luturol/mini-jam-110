using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class Lane : MonoBehaviour, IDropHandler
{
    [SerializeField] private GameObject _blockedContentLane;
    [SerializeField] private AudioClip _dropSfx;

    private VerticalLayoutGroup _content;
    private AudioSource _audioSource;
    // Start is called before the first frame update
    void Start()
    {
        _content = GetComponentInChildren<VerticalLayoutGroup>();
        _audioSource = GetComponent<AudioSource>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        _audioSource.PlayOneShot(_dropSfx);
        
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
            var card = duplicate.GetComponent<Card>();

            if (this.CompareTag("ToDo"))
            {
                Debug.Log("Removeu dono");

                card.SetStatus(CardStatus.ToDo);
                card.RemoveOwner();
            }

            Debug.Log("Esta bloqueado: " + card.IsBlocked());
            if (card.IsBlocked())
            {
                //move para blocked
                ChangeLaneFromCard(duplicate, _blockedContentLane);
            }
            else
            {
                ChangeLaneFromCard(duplicate, _content.gameObject);
            }

        }
    }

    private void ChangeLaneFromCard(GameObject duplicate, GameObject lane)
    {
        duplicate.transform.SetParent(lane.transform);
        duplicate.transform.localScale = new Vector3(1, 1, 1);
    }
}
