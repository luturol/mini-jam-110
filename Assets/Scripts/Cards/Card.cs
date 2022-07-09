using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    [Header("Card settings")]
    [SerializeField] private CardObject _cardConfiguration;
    [SerializeField] private string _title;
    [SerializeField] private string _owner;
    [SerializeField][Range(1, 3)] private int _priority;
    [SerializeField] private Image _cardImage;

    [Header("Card configuration")]
    [SerializeField] private TextMeshProUGUI _titleText;
    [SerializeField] private TextMeshProUGUI _durationText;
    [SerializeField] private TextMeshProUGUI _ownerText;
    [SerializeField] private Image _ownerImage;
    [SerializeField] private Worker _worker;
    [SerializeField] private Slider _slider;

    private int _durationValue = 0;
    private TimeCounter _timerCounter;
    private float _passedTime = 0f;

    public bool IsDuplicate = false;

    // Start is called before the first frame update
    void Start()
    {
        _timerCounter = new TimeCounter();

        if (!IsDuplicate)
        {
            _durationValue = GenerateDurationTime();
            var durationText = TranslateDurationTime(_durationValue);

            SetBaseProps();
        }

        _slider.maxValue = _durationValue * 3600;
    }

    private void Update()
    {
        _passedTime += _timerCounter.Tick();

        if (_slider.value <= _slider.maxValue)
        {
            _slider.value = _passedTime;
        }
        else
        {
            _timerCounter.Stop();
        }
    }

    private int GenerateDurationTime()
    {
        if (_priority == 1)
        {
            return Random.Range(1, 4);
        }
        else if (_priority == 2)
        {
            return Random.Range(1, 25);
        }
        else
        {
            return Random.Range(24, 72);
        }
    }

    private string TranslateDurationTime(int durationTime)
    {
        if (durationTime <= 24)
            return _durationValue == 24 ? "1d" : _durationValue + "h";
        else
            return $"{_durationValue / 24}d {_durationValue % 24}h";
    }

    public void DuplicateValues(Card cardOld)
    {
        SetValues(cardOld.GetCardConfiguration(), cardOld.GetTitle(), cardOld.GetDurationValue(), cardOld.GetOwner(), cardOld.GetPassedTime());
    }

    private void SetValues(CardObject cardConfiguration, string title, int durationValue, string owner, float passedTime)
    {
        #region setting props
        _cardConfiguration = cardConfiguration;
        _title = title;
        _durationValue = durationValue;
        _owner = owner;
        _passedTime = passedTime;
        #endregion setting props

        SetBaseProps();
    }

    private void SetBaseProps()
    {
        #region base setup

        if (_cardConfiguration && _cardImage)
        {
            _cardImage.color = _cardConfiguration.Color;
        }
        else
        {
            Debug.LogError("Necessário adicionar o Card Image e CardConfiguration no objeto do Card");
        }

        if (_titleText)
        {
            _titleText.text = _title;
        }
        else
        {
            Debug.LogError("Necessário adicionar o Title Text no objeto do Card");
        }

        if (_durationText)
        {
            _durationText.text = TranslateDurationTime(_durationValue);
        }
        else
        {
            Debug.LogError("Necessário adicionar o Duration Text no objeto do Card");
        }

        if (_ownerText && _ownerImage && _cardConfiguration)
        {
            _ownerText.text = _owner;
            _ownerImage.color = _cardConfiguration.Color;
        }
        else
        {
            Debug.LogError("Necessário adicionar o Owner Text, Owner Image e CardConfiguration no objeto do Card");
        }
        #endregion base setup
    }

    public CardObject GetCardConfiguration() => _cardConfiguration;
    public string GetTitle() => _title;
    public int GetDurationValue() => _durationValue;
    public string GetOwner() => _owner;
    public float GetPassedTime() => _passedTime;
    public void SetWorker(Worker worker)
    {
        _worker = worker;
        _owner = worker.GetWorkerName();
        _ownerText.text = _owner;

        _timerCounter.Start();
    }

    public bool HasCompletedCard() => _slider.value >= _slider.maxValue;
}
