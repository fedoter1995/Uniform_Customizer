using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PaletteItem : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private Image _image;
    [SerializeField]
    private Image _contour;


    public Color Color => _image.color;

    public event Action<PaletteItem> OnClickEvent;

    public void SetActive(bool activity)
    {
        _contour.gameObject.SetActive(activity);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnClickEvent?.Invoke(this);
    }
}
