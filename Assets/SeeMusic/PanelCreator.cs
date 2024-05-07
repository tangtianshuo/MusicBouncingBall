using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PanelCreator : MonoBehaviour
{
    public PlayerInput playerInput;
    void OnEnable()
    {
        playerInput.onActionTriggered += KeyboardTrigger;
    }
    void OnDisable()
    {
        playerInput.onActionTriggered -= KeyboardTrigger;
    }
    public void KeyboardTrigger(InputAction.CallbackContext value)
    {
        Debug.Log(value.action.name + "Triggered");
    }

}
