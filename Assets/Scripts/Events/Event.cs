using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event : MonoBehaviour
{
    [SerializeField] private int _timeInSecods;

    private float _passedTime;
    private Card _card;
    // private BossNotification _bossNotification;

    private void Start()
    {
        _card = GetComponentInChildren<Card>();
    }

    private void Update()
    {
        _passedTime += Time.deltaTime;
    }

    public bool CanSpawnCard() => _passedTime >= _timeInSecods;
    public Card GetCard() => _card;
}
