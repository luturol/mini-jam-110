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
        _text.text = $"{ time.Hours }:{ time.Minutes }:{ time.Seconds }";
    }
}
