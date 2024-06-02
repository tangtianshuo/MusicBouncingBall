using System;
using System.Collections;
using System.Collections.Generic;
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
                // this.transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.x + 1, transform.rotation.y, transform.rotation.z));
                transform.Rotate(Vector3.left, Space.Self);
                ballController.LineRendererController(Vector3.Reflect(bouncingBall.transform.position.normalized, GetNomalizationVector3()));

            }
            else if (rollDirection == "RIGHT")
            {
                transform.Rotate(Vector3.right, Space.Self);
                Debug.Log(bouncingBall.transform.position.normalized);
                ballController.LineRendererController(Vector3.Reflect(bouncingBall.transform.position.normalized, GetNomalizationVector3()));
                Debug.Log(GetNomalizationVector3());
                Debug.Log(Vector3.Reflect(bouncingBall.transform.position.normalized, GetNomalizationVector3()));

            }
            reflectV = Vector3.Reflect(V, GetNomalizationVector3());
            EventManager.Instance.LineSimulateAction.Invoke(reflectV, Director.Share.timeOffset);
            Debug.Log(reflectV);
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

    public void OnCollisionEnter(Collision other)
    {
        var ball = other.gameObject;
        var rb = ball.GetComponent<Rigidbody>();
        V = ball.GetComponent<BallBehaviour>().V;
        // rb.Sleep();
        // var v = rb.velocity;

        // speed = V.magnitude;
        Debug.Log("BallV::" + V);
        reflectV = Vector3.Reflect(V, GetNomalizationVector3());
        Debug.Log("ReflectV::" + V);
        EventManager.Instance.LineSimulateAction.Invoke(reflectV, Director.Share.timeOffset);

    }
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
