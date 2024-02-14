using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{

    [SerializeField] InputActionAsset inputActions;
    private InputAction pointerPositionAction;
    public Vector2 pointerPositionValue => pointerPositionAction.ReadValue<Vector2>();

    public Action PressedAnyKey;
    public Action Escape;
    public Action<Vector2> Click;

    public void Init()
    {
        pointerPositionAction = inputActions.FindAction("Point");
    }

    public void PressAnyKey(InputAction.CallbackContext context)
    {
        if (context.started) PressedAnyKey?.Invoke();
    }

    public void OnEscape(InputAction.CallbackContext context)
    {
        if (context.started) Escape?.Invoke();
    }

    public void OnClick(InputAction.CallbackContext context)
    {
        //if (context.started)
        //    Debug.Log("Action was started");
        //else if (context.performed)
        //    Debug.Log("Action was performed");
        //else if (context.canceled)
        //    Debug.Log("Action was cancelled");

        if (context.started) Click?.Invoke(pointerPositionValue);
        
    }
}
