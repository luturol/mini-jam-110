using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PriorityButton : MonoBehaviour
{
    [SerializeField] private Card _card;

    private Image _image;
    private bool _hasPriority;

    private void Start()
    {
        _image = GetComponent<Image>();
    }

    public void OnPriority_Click()
    {
        if (!_hasPriority)
        {
            _hasPriority = true;
            _image.color = Color.yellow;
            _card.AddPriority();
        }
        else
        {
            _image.color = Color.white;
            _card.RemovePriority();
        }
    }
}
