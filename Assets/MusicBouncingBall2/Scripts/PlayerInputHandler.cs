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
                    case "Confirm":

                        EventManager.Share.ConfirmAction.Invoke();
                        BallBehaviour.Share.SetPosition(BallBehaviour.Share.GetSimulatePosition());
                        BallBehaviour.Share.transform.SetPositionAndRotation(BallBehaviour.Share.GetLastPosition(), Quaternion.Euler(0, 0, 0));




                        break;
                    case "CreatePanel":
                        EventManager.Share.ConfirmAction.Invoke();
                        // createPanel
                        EventManager.Share.CreatePanelAction.Invoke();
                        EventManager.Share.SimulatePositionAction.Invoke(BallBehaviour.Share.GetLastPosition(), PanelManager.Share.currentPanel.transform.up);
                        break;
                    case "LeftRoll":
                        EventManager.Share.PanelRollAction.Invoke("LEFT");
                        EventManager.Share.SimulatePositionAction.Invoke(BallBehaviour.Share.GetLastPosition(), PanelManager.Share.currentPanel.transform.up);

                        break;
                    case "RightRoll":
                        EventManager.Share.PanelRollAction.Invoke("RIGHT");
                        EventManager.Share.SimulatePositionAction.Invoke(BallBehaviour.Share.GetLastPosition(), PanelManager.Share.currentPanel.transform.up);

                        break;

                    default:
                        break;

                }

            }
        }

    }
}