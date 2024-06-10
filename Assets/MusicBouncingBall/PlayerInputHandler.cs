using System;
using System.Collections;
using System.Collections.Generic;
using HeathenEngineering.UnityPhysics;
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
                case "Shot":
                    BallBehaviour.Share.StartMove();
                    BallBehaviour.Share.GetComponent<Rigidbody>().velocity = BallBehaviour.Share.GetComponent<BallisticPathLineRender>().projectile.velocity;
                    List<Vector3> positions = new List<Vector3>();
                    // BallBehaviour.Share.lineRenderer.GetPositions(positions.ToArray());
                    EventManager.Instance.RecordConfirmLine.Invoke(BallBehaviour.Share.GetComponent<BallisticPathLineRender>().trajectory);
                    BallBehaviour.Share.lineRenderer.positionCount = 0;
                    break;
                case "AddForce":
                    BallBehaviour.Share.GetComponent<BallisticPathLineRender>().projectile.velocity += new Vector3(1, 0, 0);
                    BallBehaviour.Share.GetComponent<BallisticPathLineRender>().Simulate();
                    break;


            }

        }
    }

}
