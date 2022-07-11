using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BossNotification : MonoBehaviour
{
    [Header("Boss configurations")]
    [SerializeField] private string _notification;
    [SerializeField] private int _minusDays;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI _notificationText;

    // Start is called before the first frame update
    void Start()
    {
        _notificationText.text = _notification;
    }    

    public int GetMinusDays() => _minusDays;
}
