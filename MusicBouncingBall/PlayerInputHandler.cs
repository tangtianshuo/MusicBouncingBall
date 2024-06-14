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
                    BallBehaviour.Share.StartMove();
                    Director.Share.Save();
                    break;
                case "CreatePanel":
                    CreatePanel.Invoke();
                    var RecV = Vector2.Reflect(BallBehaviour.Share.V, transform.up);
                    var result = EMath.SimulateBallPosition(Director.Share.timeOffset, RecV, BallBehaviour.Share.transform.position, 50, out List<Vector2> pointList);
                    BallBehaviour.Share.GetComponent<Rigidbody>().isKinematic = true;
                    BallBehaviour.Share.transform.DOMove(result, 0.2f);
                    break;
                case "Shot":
                    PanelManager.Share.CreatePanel(PanelManager.Share.simulatePanel.transform.position, PanelManager.Share.simulatePanel.transform.rotation);
                    BallBehaviour.Share.StartMove();
                    BallBehaviour.Share.GetComponent<Rigidbody>().velocity = BallBehaviour.Share.GetComponent<BallisticPathLineRender>().projectile.velocity;
                    List<Vector3> positions = new List<Vector3>();
                    // BallBehaviour.Share.lineRenderer.GetPositions(positions.ToArray());
                    EventManager.Instance.RecordConfirmLine.Invoke(BallBehaviour.Share.GetComponent<BallisticPathLineRender>().trajectory.Take(PanelManager.Share.impactIndex).ToList());
                    BallBehaviour.Share.lineRenderer.positionCount = 0;
                    confirm.Invoke();
                    // 此处需要记录跳板位置和小球收到的力
                    var bouncingData = new BouncingData(Director.Share.timeOffset, BallBehaviour.Share.GetComponent<BallisticPathLineRender>().projectile.velocity, PanelManager.Share.currentPanel.transform.position, PanelManager.Share.currentPanel.transform.rotation.eulerAngles);
                    Debug.Log(bouncingData);
                    Director.Share.SetSaveData(bouncingData);
                    break;
                case "AddForce":
                    BallBehaviour.Share.GetComponent<BallisticPathLineRender>().projectile.velocity += new Vector3(1, 0, 0);
                    BallBehaviour.Share.GetComponent<BallisticPathLineRender>().Simulate();
                    break;


            }

        }
    }

}
