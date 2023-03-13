using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CustomizationPage : MonoBehaviour, ISavable
{
    [SerializeField]
    private CanvasGroup _bg;
    [SerializeField]
    private float _smoothnes = 0.1f;

    [SerializeField]
    protected List<AnimatedObject> _animObjects = new List<AnimatedObject>();



    private Coroutine showHideEnumerator = null;


    public abstract Dictionary<string, object> GetObjectData();
    public abstract void SetObjectData(Dictionary<string, object> data);


    public void SetActive(bool activity)
    {
        gameObject.SetActive(activity);
    }

    public abstract void OnEnter();
    public abstract void OnExit();

    public virtual void Initialize()
    {
        foreach (AnimatedObject anim in _animObjects)
            anim.Initialize();
    }
    public void Hide()
    {
        if(showHideEnumerator == null)
            showHideEnumerator = StartCoroutine(HideRoutine(_smoothnes));
        
        foreach (AnimatedObject anim in _animObjects)
            anim.Exit();

    }
    public void Show()
    {
        SetActive(true);
        _bg.alpha = 0;

        if (showHideEnumerator == null)
            showHideEnumerator = StartCoroutine(ShowRoutine(_smoothnes));

        foreach (AnimatedObject anim in _animObjects)
            anim.Enter();
    }

    private IEnumerator HideRoutine(float smoothnes)
    {
        while(_bg.alpha > 0)
        {
            _bg.alpha -= smoothnes;
            yield return new WaitForEndOfFrame();
        }
        showHideEnumerator = null;

        SetActive(false);
    }
    private IEnumerator ShowRoutine(float smoothnes)
    {
        while (_bg.alpha < 1)
        {
            _bg.alpha += smoothnes;
            yield return new WaitForEndOfFrame();
        }
        showHideEnumerator = null;
    }

}
