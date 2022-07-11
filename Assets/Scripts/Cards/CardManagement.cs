using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CardManagement : MonoBehaviour
{
    [Header("Debug Pourposes")]
    [SerializeField] public List<Card> AllCards;
    [SerializeField] public List<string> DoneCards;
    [SerializeField] private List<Card> _cards;
    [SerializeField] private List<Event> _events;
    [SerializeField] private GameObject _backUpCards;


    [Header("Manager Configurations")]
    [SerializeField] private GameObject _backLogContent;

    public static CardManagement Instance { get; private set; }
    public bool isEndGame = false;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }

        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        _cards = GetComponentsInChildren<Card>().ToList();
        _events = GetComponentsInChildren<Event>().ToList();

        if (_cards.Any())
        {
            // AllCards = _cards.Select(e =>
            // {
            //     var card = Instantiate(e);

            //     card.DuplicateValues(e);
            //     Debug.Log($@"{ e.GetTitle() } - IsBlocked = { e.IsBlocked() } -> { e.GetBlocked()?.GetTitle() } -> Blocking { e.GetBlocking()?.GetTitle() }
            //     { card.GetTitle() } - IsBlocked = { card.IsBlocked() } -> { card.GetBlocked()?.GetTitle() } -> Blocking { card.GetBlocking()?.GetTitle() }");

            //     card.transform.SetParent(_backUpCards.transform);
            //     return card;
            // }).ToList();

            DoneCards = new List<string>();
        }

        // _cards.ForEach(e => e.transform.SetParent(_backLogContent.transform));
    }

    // Update is called once per frame
    void Update()
    {        
        if(isEndGame)
            return;
        var eventsHappened = _events.Where(e => e.CanSpawnCard());

        foreach (Event _ in eventsHappened)
        {
            var card = _.GetCard();

            card?.transform.SetParent(_backLogContent.transform);
        }

        foreach (Event _ in eventsHappened)
        {
            Destroy(_.gameObject);
        }

        _events.RemoveAll(e => eventsHappened.Contains(e));
    }
}
