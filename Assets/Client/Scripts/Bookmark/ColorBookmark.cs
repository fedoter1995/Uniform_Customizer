using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ColorBookmark : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private Color _currentColor;
    [SerializeField]
    private Image _cloredImage;
    [SerializeField]
    private Image _contour;
    public int Index { get; private set; }
    public Color Color => _currentColor;

    public event Action<ColorBookmark> OnClickEvent;


    public void Initialize(int index, Color color)
    {
        Index = index;
        ChangeColor(color);
    }


    public void ChangeColor(Color color)
    {
        _currentColor = color;

        _cloredImage.color = _currentColor;
    }

    public void SetActive(bool activity)
    {
        _contour.gameObject.SetActive(activity);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnClickEvent?.Invoke(this);
    }

}
