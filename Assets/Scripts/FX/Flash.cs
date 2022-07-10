using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Flash : MonoBehaviour
{
    [SerializeField] private Material _flashMaterial;
    [SerializeField] private float _duration;

    [SerializeField] private Image _image;
    private Material _originalMaterial;
    private Coroutine _flashRoutine;


    // Start is called before the first frame update
    void Start()
    {        
        _originalMaterial = _image.material;
    }

    public void FlashAnimation()
    {
        if (_flashRoutine != null)
        {
            StopCoroutine(_flashRoutine);
        }

        _flashRoutine = StartCoroutine(FlashCo());
    }

    private IEnumerator FlashCo()
    {
        var backupColor = _image.color;

        _image.material = _flashMaterial;
        _image.color = Color.white;
        yield return new WaitForSeconds(_duration);

        _image.material = _originalMaterial;
        _image.color = backupColor;
        _flashRoutine = null;
    }
}
