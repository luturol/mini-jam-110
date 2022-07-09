using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Worker : MonoBehaviour
{
    [Header("Lanes")]
    [SerializeField] private GameObject _inProgressContentLane;
    [SerializeField] private GameObject _toDoContentLane;
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
            var card = _toDoContentLane.GetComponentsInChildren<Card>()
                        .ToList()
                        .FirstOrDefault(e => string.IsNullOrEmpty(e.GetOwner()) &&
                            e.GetCardConfiguration().CardType == _cardTypeToWork);

            if (card != null)
            {
                card.SetWorker(this);
                _cardDoing = card;
                _isAvailableToWork = false;

                _cardDoing.transform.SetParent(_inProgressContentLane.transform);
            }
        }

        if(_cardDoing != null && _cardDoing.HasCompletedCard())
        {
            _cardDoing.transform.SetParent(_doneContentLane.transform);
            _cardDoing = null;
            _isAvailableToWork = true;
        }
    }

    public string GetWorkerName() => _workerName;
}
