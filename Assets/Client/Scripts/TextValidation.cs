using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class TextValidation : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField _input;
    [SerializeField]
    private TextMeshProUGUI _text;
    
    [SerializeField]
    private CanvasGroup target;
    [SerializeField]
    private Color _dangerColor;

    private Color defaultColor;


    public void Initialize()
    {
        defaultColor = _text.faceColor;
        Validate(_input.text);
        _input.onValueChanged.AddListener(Validate);
    }

    private void Validate(string str)
    {
        
        if (str.Length > 10 || str.Length == 0)
        {
            _text.faceColor = _dangerColor;
            target.interactable = false;
        }
        else
        {
            target.interactable = true;
            _text.faceColor = defaultColor;
        }

    }

    public string GetText()
    {

        return _input.text;
    }
    public void SetText(string str)
    {

        _input.text = str;
    }
}
