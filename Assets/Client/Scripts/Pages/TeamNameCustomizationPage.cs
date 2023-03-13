using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamNameCustomizationPage : CustomizationPage
{

    [SerializeField]
    private CustomizationHandler _uniformHandler;
    [SerializeField]
    private CustomizationHandler _logosHandler;

    [SerializeField]
    private RectTransform _uniformContainer;
    [SerializeField]
    private RectTransform _logoContainer;

    [SerializeField]
    private TextValidation _textValidator;

    public override void Initialize()
    {
        base.Initialize();

        SetObjectData(GameManager.LoadObjectData(ToString()));

        _textValidator.Initialize();

        RectTransform uniformTransform = (RectTransform)_uniformHandler.ActiveObject.transform;
        RectTransform logoTransform = (RectTransform)_logosHandler.ActiveObject.transform;

        uniformTransform.SetParent(_uniformContainer);
        logoTransform.SetParent(_logoContainer);

        ResetTransform(uniformTransform);
        ResetTransform(logoTransform);

        SetActive(true);
    }

    public override void OnEnter()
    {
        Initialize();
        Show();
    }
    public override void OnExit()
    {
        Hide();
    }
    public override Dictionary<string, object> GetObjectData()
    {
        var data = new Dictionary<string, object>();

        data.Add("Team_Name", _textValidator.GetText());

        return data;
    }
    public override void SetObjectData(Dictionary<string, object> data)
    {
        if(data != null)
        {
            var name = data["Team_Name"].ToString();

            _textValidator.SetText(name);
        }

    }
    public override string ToString()
    {
        return "TeamNameCustomizationPage";
    }

    private void ResetTransform(RectTransform transform)
    {
        
        transform.localScale = Vector3.one;
        transform.sizeDelta = Vector2.zero;
        transform.anchoredPosition = Vector2.zero;
    }
}
