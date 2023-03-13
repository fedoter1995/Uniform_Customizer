using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorCustomizationPage : CustomizationPage
{
    [SerializeField]
    protected CustomizationHandler _customHandler;
    [SerializeField]
    protected BookmarkSwitch _switch;


    public override void Initialize()
    {
        base.Initialize();
        SetObjectData(GameManager.LoadObjectData(ToString()));
        _customHandler.Initialize(_switch);
    }


    public override void OnExit()
    {
        Hide();
        _customHandler.OnChangePage();
    }
    public override void OnEnter()
    {
        Initialize();
        Show();
    }
    public override Dictionary<string, object> GetObjectData()
    {
        return _customHandler.GetObjectData();
    }
    public override void SetObjectData(Dictionary<string, object> data)
    {
        _customHandler.SetObjectData(data);
    }
}
