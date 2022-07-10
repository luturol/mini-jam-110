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
    [SerializeField] private int _priority = 0;
    [SerializeField] private Image _cardImage;

    [Header("Card configuration")]
    [SerializeField] private TextMeshProUGUI _titleText;
    [SerializeField] private TextMeshProUGUI _durationText;
    [SerializeField] private TextMeshProUGUI _ownerText;
    [SerializeField] private Image _ownerImage;
    [SerializeField] private Worker _worker;
    [SerializeField] private Slider _slider;

    [Header("Duration Configurations")]
    [SerializeField] private int _durationValueReview = 0;
    [SerializeField] private int _durationValue = 0;

    [Header("Debug pourposes")]
    [SerializeField] private CardStatus _cardStatus = CardStatus.ToDo;

    private TimeCounter _timerCounter;
    private float _passedTime = 0f;

    public bool IsDuplicate = false;    

    // Start is called before the first frame update
    void Start()
    {
        _timerCounter = new TimeCounter();

        if (!IsDuplicate)
        {
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

    private string TranslateDurationTime(int durationTime)
    {
        if (durationTime <= 24)
            return durationTime == 24 ? "1d" : durationTime + "h";
        else
            return $"{durationTime / 24}d {durationTime % 24}h";
    }

    public void DuplicateValues(Card cardOld)
    {
        SetValues(cardOld.GetCardConfiguration(), cardOld.GetTitle(), cardOld.GetDurationValue(), cardOld.GetOwner(), cardOld.GetPassedTime());
    }

    private void SetValues(CardObject cardConfiguration, string title, int durationValue, string owner, float passedTime)
    {
        Debug.Log("Setou valores " + owner);
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
    public void StopTime() => _timerCounter.Stop();

    public void RemoveOwner()
    {
        if(_worker != null)
            _worker.DropCard();
        
        _worker = null;
        _owner = string.Empty;        
        _ownerText.text = string.Empty;

        _timerCounter.Stop();
    }

    public void SetStatus(CardStatus status)
    {
        if (status == CardStatus.ReviewQA)
        {
            _passedTime = 0f;
            _slider.value = 0f;
            _slider.maxValue = _durationValueReview * 3600;
            _durationText.text = TranslateDurationTime(_durationValueReview);
        }
        else if(status == CardStatus.Done)
        {
            _durationText.text = TranslateDurationTime(_durationValueReview + _durationValue);
        }

        _cardStatus = status;
    }

    public CardStatus GetStatus() => _cardStatus;

    public void AddPriority() => _priority++;
    public void RemovePriority() => _priority--;
    public int GetPriority() => _priority;
}
