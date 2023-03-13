using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorPalette : MonoBehaviour
{

    private List<PaletteItem> _items;

    private PaletteItem activeItem;


    public event Action<PaletteItem> OnChooseColorEvent;

    public  void Initialize()
    {
        _items = new List<PaletteItem>(GetComponentsInChildren<PaletteItem>());

        foreach(PaletteItem button in _items)
        {
            button.OnClickEvent += OnChooseColor;
        }
    }

    public List<PaletteItem> GetItems()
    {
        var items = new List<PaletteItem>();

        foreach(PaletteItem item in _items)
            items.Add(item);        

        return items;
    }

    public void SetActiveItem(Color color)
    {
        if (activeItem != null)
            activeItem.SetActive(false);

        var item = _items.Find(item => item.Color == color);

        if (item == null)
            throw new System.Exception("There is no element with a suitable color");

        activeItem = item;
        activeItem.SetActive(true);
    }
    public void SetActiveItem(PaletteItem item)
    {
        if (activeItem != null)
            activeItem.SetActive(false);

        activeItem = item;
        activeItem.SetActive(true);
    }

    private void OnChooseColor(PaletteItem item)
    {
        OnChooseColorEvent?.Invoke(item);
    }
}
