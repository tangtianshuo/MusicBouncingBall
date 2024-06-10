using System;
using System.Collections;
using System.Collections.Generic;
using HeathenEngineering.UnityPhysics;
using UnityEngine;

public class PanelBehaviour : MonoBehaviour
{

    PlayerInputHandler inputHandler;
    Director director;
    public GameObject bouncingBall;
    public bool isActivate;
    private BallBehaviour ballController;
    public Vector3 nextPosition;

    public Quaternion nextRotation;

    public Action simulateLineAction;

    public float height;


    private Rigidbody ballRb;
    void Start()
    {
        height = GetComponent<Collider>().bounds.size.y;
        isActivate = true;
        bouncingBall = GameObject.FindWithTag("BouncingBall");
        ballController = bouncingBall.GetComponent<BallBehaviour>();
        inputHandler = Director.Share.GetComponent<PlayerInputHandler>();
        director = Director.Share.GetComponent<Director>();
        inputHandler.rollAction += Roll;
        confirm = false;
    }
    private void Roll(string rollDirection)
    {
        if (isActivate)
        {
            //需要改变panel的法线
            if (rollDirection == "LEFT")
            {
                transform.Rotate(Vector3.left, Space.Self);
                ballController.LineRendererController(Vector3.Reflect(bouncingBall.transform.position.normalized, GetNomalizationVector3()));
                // BallBehaviour.Share.GetComponent<BallisticPathLineRender>().projectile.velocity = transform.up * BallBehaviour.Share.speed;
            }
            else if (rollDirection == "RIGHT")
            {
                transform.Rotate(Vector3.right, Space.Self);
                ballController.LineRendererController(Vector3.Reflect(bouncingBall.transform.position.normalized, GetNomalizationVector3()));
                // BallBehaviour.Share.GetComponent<BallisticPathLineRender>().projectile.velocity = transform.up * BallBehaviour.Share.speed;

            }
            BallisticPathLineRender ballistic = BallBehaviour.Share.GetComponent<BallisticPathLineRender>();
            ballistic.projectile.velocity = transform.up * BallBehaviour.Share.speed;
            ballistic.start = BallBehaviour.Share.transform.position;
            ballistic.Simulate();
            // reflectV = Vector3.Reflect(V, GetNomalizationVector3());
            // // ballBehaviour.GetComponent<DrawLine>().addedV = reflectV;
            // // ballRb.velocity = reflectV;
            // var v = BallBehaviour.Share.incident + transform.up;
            // var final = new Vector3();
            // var height = new Vector3();
            // var pos = BallBehaviour.Share.GetNextPositionOrigin(originPosition, V, Director.Share.timeOffset, out height, out final);
            // BallBehaviour.Share.transform.SetPositionAndRotation(final, Quaternion.Euler(new Vector3()));
            // // EventManager.Instance.LineSimulateAction.Invoke(reflectV, Director.Share.timeOffset);
            // Debug.Log(reflectV);

        }

    }
    public void DestoryController()
    {
        inputHandler.rollAction -= Roll;
        isActivate = false;
    }
    void OnEnable()
    {
        // Debug.Log("JumpPanel OnEnable");
        isActivate = false;
    }
    public Vector3 V;
    public Vector3 reflectV;
    public BallBehaviour ballBehaviour;

    public Vector3 originPosition;
    // public void OnCollisionEnter(Collision other)
    // {
    //     BallBehaviour.Share.GetComponent<BallisticPathLineRender>().Simulate();
    //     var ball = other.gameObject;
    //     var rb = ball.GetComponent<Rigidbody>();
    //     originPosition = ball.transform.position;
    //     ballRb = rb;
    //     ballBehaviour = ball.GetComponent<BallBehaviour>();
    //     // ballBehaviour.StopMove();
    //     V = BallBehaviour.Share.incident;
    //     Debug.Log("BallV::" + V);
    //     reflectV = Vector3.Reflect(V, GetNomalizationVector3());
    //     Debug.Log("ReflectV::" + reflectV);
    //     // EventManager.Instance.LineSimulateAction.Invoke(reflectV, Director.Share.timeOffset);
    //     ball.GetComponent<DrawLine>().addedV = reflectV;
    //     // rb.velocity = reflectV;
    //     Vector3 heightestPoint = new();
    //     Vector3 finnalPoint = new();
    //     ballBehaviour.GetNextPosition(reflectV, Director.Share.timeOffset, out heightestPoint, out finnalPoint);

    //     Debug.Log("heightest: " + heightestPoint);
    //     Debug.Log("finnalPoint: " + finnalPoint);
    //     ballRb.velocity = V;

    // }
    public void OnCollisionEnter(Collision other)
    {
        BallBehaviour.Share.GetComponent<BallisticPathLineRender>().Simulate();

    }




    // // 根据入射角 推算反射角 
    // public Vector3 SimulateReflect()
    // {

    // }

    // public void OnCollisionStay(Collision other)
    // {
    //     var ball = other.gameObject;
    //     var rb = ball.GetComponent<Rigidbody>();
    //     ballBehaviour = ball.GetComponent<BallBehaviour>();
    //     ball.GetComponent<DrawLine>().addedV = reflectV;


    // }

    /// <summary>
    /// 获取立方体物体碰撞体上表面的法线方向
    /// </summary>
    /// <returns>返回碰撞体上表面的法线向量</returns>
    private Vector3 GetNomalizationVector3()
    {
        return transform.up;
    }

    private bool confirm;
    public IEnumerator ControlPanel()
    {
        // 计算当前法线方向
        var count = 0;
        while (!confirm || count < 500)
        {
            count++;
            confirm = Director.Share.isConfirm;
            yield return null;
        }

    }



}
