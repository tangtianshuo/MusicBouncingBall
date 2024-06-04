using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEditor.Rendering.LookDev;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class PlayerInputHandler : MonoBehaviour
{
    public static PlayerInputHandler Share = null;
    public virtual void Awake()
    {
        Share = this;

    }
    public PlayerInput input;

    public GameObject jumpCube;

    public GameObject bouncingBall;

    public Action<string> rollAction;

    public Action confirm;

    public Action CreatePanel;


    void OnEnable()
    {
        input.onActionTriggered += KeyboardTrigger;
    }
    void OnDisable()
    {
        input.onActionTriggered -= KeyboardTrigger;
    }

    public void KeyboardTrigger(InputAction.CallbackContext value)
    {
        if (value.started)
        {
            switch (value.action.name)
            {

                case "LeftRoll":
                    rollAction.Invoke("LEFT");
                    break;
                case "RightRoll":
                    rollAction.Invoke("RIGHT");
                    break;
                case "Confirm":
                    confirm.Invoke();
                    break;
                case "CreatePanel":
                    CreatePanel.Invoke();
                    break;

            }

        }
    }

}
