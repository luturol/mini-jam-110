using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Worker : MonoBehaviour
{
    [SerializeField] private GameObject _inProgressContentLane;
    [SerializeField] private GameObject _toDoContentLane;
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

                card.transform.SetParent(_inProgressContentLane.transform);
            }

        }
    }

    public string GetWorkerName() => _workerName;
}
