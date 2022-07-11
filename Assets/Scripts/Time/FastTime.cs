using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FastTime : MonoBehaviour
{
    [SerializeField] private float _passedTime;
    [SerializeField] public static float TimeMultiplier = 3000f;

    private TextMeshProUGUI _text;    
    private TimeCounter _timerCounter;
    // Start is called before the first frame update
    void Start()
    {
        _text = GetComponent<TextMeshProUGUI>();

        _timerCounter = new TimeCounter();
        _timerCounter.Start();
    }

    // Update is called once per frame
    void Update()
    {
        _passedTime += _timerCounter.Tick();

        var time = TimeSpan.FromSeconds(_passedTime);
        
        _text.text = $"{ time.Days.ToString("D2")}d { time.Hours.ToString("D2") }:{ time.Minutes.ToString("D2") }:{ time.Seconds.ToString("D2") }";
    }

    public float GetPassedTime() => _passedTime;
}
