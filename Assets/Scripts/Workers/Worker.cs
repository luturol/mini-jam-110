using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Worker : MonoBehaviour
{
    [Header("Lanes")]
    [SerializeField] private GameObject _toDoContentLane;
    [SerializeField] private GameObject _inProgressContentLane;
    [SerializeField] private GameObject _reviewContentLane;
    [SerializeField] private GameObject _doneContentLane;

    [Header("Card and Work configurations")]
    [SerializeField] private CardType _cardTypeToWork;
    [SerializeField] private Card _cardDoing;
    [SerializeField] private bool _isAvailableToWork = true;
    [SerializeField] private string _workerName;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (_cardDoing == null && _isAvailableToWork)
        {
            Card card = null;

            if (_cardTypeToWork == CardType.GameDesign)
            {
                card = GetAvailableCardFromLane(_reviewContentLane);
            }

            if (card == null)
            {
                card = GetAvailableCardFromLane(_toDoContentLane);
            }

            if (card != null)
            {
                Debug.Log($"Pegou um card na fila {card.GetStatus()}");
                card.SetWorker(this);
                _cardDoing = card;
                _isAvailableToWork = false;

                if (_cardDoing.GetStatus() == CardStatus.ToDo)
                {
                    _cardDoing.transform.SetParent(_inProgressContentLane.transform);
                    _cardDoing.SetStatus(CardStatus.InProgress);
                }
            }
        }

        if (_cardDoing != null && _cardDoing.HasCompletedCard())
        {
            Debug.Log("Completou o card " + _cardDoing.GetTitle());
            if (_cardDoing.GetStatus() == CardStatus.InProgress)
            {
                _cardDoing.transform.SetParent(_reviewContentLane.transform);
                _cardDoing.SetStatus(CardStatus.ReviewQA);
                _cardDoing.RemoveOwner();
            }
            else if (_cardDoing.GetStatus() == CardStatus.ReviewQA)
            {
                _cardDoing.transform.SetParent(_doneContentLane.transform);
                _cardDoing.SetStatus(CardStatus.Done);
            }

            DropCard();
        }
    }

    private Card GetAvailableCardFromLane(GameObject lane)
    {
        return lane.GetComponentsInChildren<Card>()
                               .ToList()
                               .Where(e => string.IsNullOrEmpty(e.GetOwner()) &&
                                    !e.IsBlocked() &&
                                   (e.GetCardConfiguration().CardType == _cardTypeToWork ||
                                   (_cardTypeToWork == CardType.GameDesign && e.GetStatus() == CardStatus.ReviewQA)))
                                .OrderByDescending(e => e.GetPriority())
                                .FirstOrDefault();
    }

    public string GetWorkerName() => _workerName;
    public void DropCard()
    {
        Debug.Log("Dropou carta");
        _isAvailableToWork = true;
        _cardDoing = null;
    }
}
