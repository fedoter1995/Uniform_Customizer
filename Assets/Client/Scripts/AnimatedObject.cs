using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class AnimatedObject : MonoBehaviour
{
    [SerializeField]
    private AnimationAction _action = AnimationAction.Right;
    [SerializeField]
    private float _delay = 0.1f;
    [SerializeField]
    private float _smoothness = 1f;


    private RectTransform objTransform;


    public void Initialize()
    {
        objTransform = GetComponent<RectTransform>();
    }

    public void Enter()
    {
        switch(_action)
        {
            case AnimationAction.Right:
                {
                    var x = objTransform.localPosition.x;
                    objTransform.localPosition = new Vector2(-Screen.width, objTransform.localPosition.y);
                    objTransform.LeanMoveLocalX(x, _smoothness).setEaseInOutExpo().delay = _delay;
                    break;
                }
            case AnimationAction.Left:
                {
                    var x = objTransform.localPosition.x;
                    objTransform.localPosition = new Vector2(Screen.width, objTransform.localPosition.y);
                    objTransform.LeanMoveLocalX(x, _smoothness).setEaseInOutExpo().delay = _delay;
                    break;
                }
            case AnimationAction.Up:
                {
                    var y = objTransform.localPosition.y;
                    objTransform.localPosition = new Vector2(objTransform.localPosition.x, -Screen.height);
                    objTransform.LeanMoveLocalY(y, _smoothness).setEaseInOutExpo().delay = _delay;
                    break;
                }
            case AnimationAction.Down:
                {
                    var y = objTransform.localPosition.y;
                    objTransform.localPosition = new Vector2(objTransform.localPosition.x, Screen.height);
                    objTransform.LeanMoveLocalY(y, _smoothness).setEaseInOutExpo().delay = _delay;
                    break;
                }
        }

    }
    public void Exit()
    {
        switch (_action)
        {
            case AnimationAction.Right:
                {
                    objTransform.LeanMoveLocalX(Screen.width, _smoothness).setEaseInExpo().setOnComplete(OnExit);
                    break;
                }
            case AnimationAction.Left:
                {
                    objTransform.LeanMoveLocalX(-Screen.width, _smoothness).setEaseInExpo().setOnComplete(OnExit);
                    break;
                }
            case AnimationAction.Up:
                {
                    objTransform.LeanMoveLocalY(Screen.height, _smoothness).setEaseInExpo().setOnComplete(OnExit);
                    break;
                }
            case AnimationAction.Down:
                {
                    objTransform.LeanMoveLocalY(-Screen.height, _smoothness).setEaseInExpo().setOnComplete(OnExit);
                    break;
                }
        }

    }
    private void OnExit()
    {
        gameObject.SetActive(false);
    }
}
public enum AnimationAction
{
    Up,
    Down,
    Left,
    Right
}