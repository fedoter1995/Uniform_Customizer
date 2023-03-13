using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BookmarkSwitch : MonoBehaviour
{
    [SerializeField]
    private List<ColorBookmark> _bookmarks;
    [SerializeField]
    private ColorPalette _palette;
    [SerializeField]
    private Button _randomColorButton;

    private ColorBookmark activeBookmark;


    public event Action<ColorBookmark> OnChangeColorEvent;


    public void Initialize(List<Color> colors)
    {
        if (_bookmarks.Count != colors.Count)
            throw new System.Exception("The number of layers does not match");

        for (int i = 0; i < _bookmarks.Count; i++)
        {
            _bookmarks[i].Initialize(i, colors[i]);
            _bookmarks[i].OnClickEvent += SetActiveBookmark;
            _bookmarks[i].SetActive(false);
        }

        _palette.Initialize();
        _palette.OnChooseColorEvent += ChangeColor;
        _randomColorButton.onClick.AddListener(SetRandomColors);


        SetActiveBookmark(_bookmarks[0]);
    }
    public List<ColorBookmark> GetLayers()
    {
        return new List<ColorBookmark>(_bookmarks);
    }

    public void ChangeColors(List<Color> colors)
    {
        for (int i = 0; i < _bookmarks.Count; i++)
        {
            _bookmarks[i].ChangeColor(colors[i]);
        }
        SetActiveBookmark(activeBookmark);
    }
    private void SetActiveBookmark(ColorBookmark layer)
    {
        if(activeBookmark != null)
            activeBookmark.SetActive(false);

        activeBookmark = layer;
        activeBookmark.SetActive(true);

        _palette.SetActiveItem(activeBookmark.Color);
    }
    private void SetRandomColors()
    {
        var items = new List<PaletteItem>(_palette.GetItems());

        foreach(ColorBookmark bookmark in _bookmarks)
        {
            var rnd = new System.Random();
            var i = rnd.Next(0, items.Count);
            var backup = activeBookmark;

            activeBookmark = bookmark;
            ChangeColor(items[i]);
            activeBookmark = backup;

            items.Remove(items[i]);
        }
    }
    private void ChangeColor(PaletteItem item)
    {
        var layer = _bookmarks.Find(bookmark => bookmark.Color == item.Color);
        
        if(!layer)
        {
            activeBookmark.ChangeColor(item.Color);
            _palette.SetActiveItem(item);
            OnChangeColorEvent?.Invoke(activeBookmark);
        }
    }
}
