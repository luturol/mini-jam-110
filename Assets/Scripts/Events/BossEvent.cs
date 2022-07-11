using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEvent : MonoBehaviour
{
    [SerializeField] private int _timeInSecods;
    [SerializeField] private GameObject _bossNotification;
    
    [Header("Hide Configurations")]
    [SerializeField] private int _secondsToWaitBeforeHide;

    private float _passedTime;

    private TimeCounter _timeCounter;
    private bool _isShowing = false;
    private bool _executedCo = false;
    private void Start()
    {
        // _timeCounter = new TimeCounter();

        // _timeCounter.Start();
    }

    private void Update()
    {
        _passedTime += Time.deltaTime;

        if (_isShowing && !_executedCo)
        {
             StartCoroutine(Hide_Coroutine());
        }
    }

    IEnumerator Hide_Coroutine()
    {
        _executedCo = true;
        Debug.Log("executou somente 1x");
        yield return new WaitForSeconds(_secondsToWaitBeforeHide);
        _isShowing = false;
        Hide();
    }

    public void Show()
    {
        _isShowing = true;
        _bossNotification.SetActive(true);
    }
    public void Hide() => _bossNotification.SetActive(false);
    public bool CanShowNotification() => _passedTime >= _timeInSecods;


    public BossNotification GetBossNotification() => _bossNotification.GetComponent<BossNotification>();
}
