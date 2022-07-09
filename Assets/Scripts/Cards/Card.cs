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
    [SerializeField] private string _onwer;
    [SerializeField][Range(1, 3)] private int _priority;

    [Header("Card configuration")]
    [SerializeField] private TextMeshProUGUI _titleText;
    [SerializeField] private TextMeshProUGUI _durationText;
    [SerializeField] private TextMeshProUGUI _ownerText;
    [SerializeField] private Image _ownerImage;

    private Image _cardImage;

    // Start is called before the first frame update
    void Start()
    {
        #region base setup
        _cardImage = GetComponent<Image>();

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
            _durationText.text = GenerateDurationTime();
        }
        else
        {
            Debug.LogError("Necessário adicionar o Duration Text no objeto do Card");
        }

        if (_ownerText && _ownerImage && _cardConfiguration)
        {
            _ownerText.text = _onwer;
            _ownerImage.color = _cardConfiguration.Color;
        }
        else
        {
            Debug.LogError("Necessário adicionar o Owner Text, Owner Image e CardConfiguration no objeto do Card");
        }
        #endregion base setup
    }    

    private string GenerateDurationTime()
    {
        if (_priority == 1)
        {
            return Random.Range(1, 4) + "h";
        }
        else if (_priority == 2)
        {
            var value = Random.Range(1, 25);
            return value == 24 ? "1d" : value + "h";
        }
        else
        {
            var value = Random.Range(24, 72);

            var durationText = $"{value / 24}d {value % 24}h";
            Debug.Log("Total horas da task = " + value + " duration text = " + durationText);

            return durationText;
        }
    }
}
