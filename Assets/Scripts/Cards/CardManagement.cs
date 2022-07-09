using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CardManagement : MonoBehaviour
{
    [Header("Debug Pourposes")]
    [SerializeField] private List<Card> _cards;

    [Header("Manager Configurations")]
    [SerializeField]private GameObject _backLogContent;    

    // Start is called before the first frame update
    void Start()
    {
        _cards = GetComponentsInChildren<Card>().ToList();

        _cards.ForEach(e => e.transform.SetParent(_backLogContent.transform));
    }

    // Update is called once per frame
    void Update()
    {        
    }    
}
