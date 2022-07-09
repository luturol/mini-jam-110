using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeCounter
{
    private bool _canCount = false;

    public float Tick()
    {
        if (_canCount)
            return Time.deltaTime * FastTime.TimeMultiplier;
        else
            return 0;
    }

    public void Start() => _canCount = true;
    public void Stop() => _canCount = false;
}
