using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public abstract class CustomObject : MonoBehaviour
{
    [SerializeField]
    protected string _id;
    [SerializeField]
    protected List<Color> _colors;
    [SerializeField]
    protected List<Image> _layers;

    public List<Color> Colors => _colors;
    public string Id => _id;

    public virtual void Initialize()
    {
        
        if (_layers.Count != Colors.Count)
            throw new System.Exception("The number of layers does not match");

        for (int i = 0; i < _layers.Count; i++)
        {
            UpdateLayerColor(i);
        }
    }
    public virtual void Initialize(List<Color> colors)
    {

        if (_layers.Count != Colors.Count)
            throw new System.Exception("The number of layers does not match");

        _colors = colors;
        for (int i = 0; i < _layers.Count; i++)
        {
            UpdateLayerColor(i);
        }
    }
    public void ChangeLayerColor(ColorBookmark bookmark)
    {
        _colors[bookmark.Index] = bookmark.Color;

        UpdateLayerColor(bookmark.Index);
    }
    public virtual void Enter()
    {
        gameObject.SetActive(true);
    }
    public virtual void Exit()
    {
        gameObject.SetActive(false);
    }
    private void UpdateLayerColor(int index)
    {
        _layers[index].color = Colors[index];
    }

    public Dictionary<string, object> GetObjectData()
    {
        var hexColors = new List<string>();
        var data = new Dictionary<string, object>();

        foreach(Color color in Colors)
        {
            hexColors.Add("#" + ColorUtility.ToHtmlStringRGB(color));
        }

        data.Add("Id", Id);
        data.Add("Colors", hexColors);

        return data;
    }
}
