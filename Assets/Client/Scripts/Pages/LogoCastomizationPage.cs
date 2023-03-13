using UnityEngine;
public class LogoCastomizationPage : ColorCustomizationPage
{
    [SerializeField]
    private CustomizationHandler _uniformHandler;
    [SerializeField]
    private RectTransform _uniformContainer;

    public override void Initialize()
    {
        RectTransform uniformTransform = (RectTransform)_uniformHandler.ActiveObject.transform;

        uniformTransform.SetParent(_uniformContainer);
        uniformTransform.localScale = Vector3.one;
        uniformTransform.sizeDelta = Vector2.zero;
        uniformTransform.anchoredPosition = Vector2.zero;
        base.Initialize();
    }

    public override string ToString()
    {
        return "LogoCastomizationPage";
    }
}
