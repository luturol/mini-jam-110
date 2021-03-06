using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeliverProject : MonoBehaviour
{
    [SerializeField] private int _dueDate;
    [SerializeField] private FastTime _fastTime;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI _dueDateText;


    [Header("Game Over")]
    [SerializeField] private bool _enableGameOver = false;

    // Update is called once per frame
    void Update()
    {
        _dueDateText.text = _dueDate + "d";

        var passedTime = TimeSpan.FromSeconds(_fastTime.GetPassedTime());

        if (passedTime.Days >= _dueDate)
        {
            //Game Over
            if (_enableGameOver)
            {
                CardManagement.Instance.isEndGame = true;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }

        }
    }

    public void SubtractDays(int value)
    {
        if (_dueDate - value > 0)
        {
            _dueDate -= value;
        }
        else
        {
            Debug.LogError("Due Date não pode ser menor que zero. DueDate atual " + _dueDate + " valor que está tentando subtrair " + value);
        }
    }
}
