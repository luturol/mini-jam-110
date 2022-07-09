using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FastTime : MonoBehaviour
{
    [SerializeField] private float _passedTime;
    [SerializeField] private float _timeMultiplier;

    private TextMeshProUGUI _text;
    private int days = 0;

    // Start is called before the first frame update
    void Start()
    {
        _text = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        _passedTime += Time.deltaTime * _timeMultiplier;

        var time = TimeSpan.FromSeconds(_passedTime);
        
        _text.text = $"{ time.Days.ToString("D2")}d { time.Hours.ToString("D2") }:{ time.Minutes.ToString("D2") }:{ time.Seconds.ToString("D2") }";
    }
}
