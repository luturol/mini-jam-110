using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CardManagement : MonoBehaviour
{
    [Header("Debug Pourposes")]
    [SerializeField] private List<Card> _cards;
    [SerializeField] private List<Event> _events;

    [Header("Manager Configurations")]
    [SerializeField] private GameObject _backLogContent;

    // Start is called before the first frame update
    void Start()
    {
        _cards = GetComponentsInChildren<Card>().ToList();
        _events = GetComponentsInChildren<Event>().ToList();

        // _cards.ForEach(e => e.transform.SetParent(_backLogContent.transform));
    }

    // Update is called once per frame
    void Update()
    {
        var eventsHappened = _events.Where(e => e.CanSpawnCard());

        foreach (Event _ in eventsHappened)
        {
            var card = _.GetCard();

            card.transform.SetParent(_backLogContent.transform);
        }

        foreach (Event _ in eventsHappened)
        {
            Destroy(_.gameObject);
        }

        _events.RemoveAll(e => eventsHappened.Contains(e));
    }
}
