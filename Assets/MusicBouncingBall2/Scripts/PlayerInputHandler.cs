using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using HeathenEngineering.UnityPhysics;
using JetBrains.Annotations;
using UnityEditor.Rendering.LookDev;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
namespace MusicBouncingBall
{
    public class PlayerInputHandler : MonoBehaviour
    {
        public static PlayerInputHandler Share = null;
        public virtual void Awake()
        {
            Share = this;

        }
        public PlayerInput input;

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
                    case "CreatePanel":
                        // createPanel
                        EventManager.Share.CreatePanelAction.Invoke();
                        break;


                    default:
                        break;

                }

            }
        }

    }
}