using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DisplayCards : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject _missingCardsContent;
    [SerializeField] private GameObject _allCardsObject;
    [Header("Debug pourposes")]
    [SerializeField] private List<Card> _missingCards = new List<Card>();
    
    // Start is called before the first frame update
    void Start()
    {
        var allCards = _allCardsObject.GetComponentsInChildren<Card>().ToList();

        var doneCards = CardManagement.Instance?.DoneCards;

        _missingCards = allCards?.Where(e => !doneCards.Contains(e.GetTitle())).ToList();

        if (_missingCards != null)
        {
            foreach (Card _ in _missingCards)
            {
                Debug.Log($"Colocando { _.GetTitle() } na lista");
                _.transform.SetParent(_missingCardsContent.transform);
            }
        }
    }
}
