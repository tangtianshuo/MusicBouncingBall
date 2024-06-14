using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using Dweiss;
using Unity.Burst.Intrinsics;
using HeathenEngineering.UnityPhysics;
/*
 * 作者: 唐天硕
 * 创建: 2024-05-15 20:52
 *
 * 描述: 获取 时间偏移值，预测时间偏移值之后的小球位置，并且画线，移动到指定位置。
 */


public class BallBehaviour : MonoBehaviour
{
    public static BallBehaviour Share = null;
    public virtual void Awake()
    {
        Share = this;
        speed = 5f;
    }
    public Vector3 force;

    public float speed = 5f;

    public float ballR;

    [SerializeField]
    private Vector3 nextPosition;

    public LineRenderer lineRenderer;

    private List<Info> infos;

    public Vector3 V;
    // public BallisticPathLineRender ballisticPathLineRender;

    private Rigidbody rb;
    // Start is called before the first frame update
    public bool stopSign;

    public Vector3 addV;
    public Vector3 incident;

    public BallisticPathLineRender ballisticPathLineRender;


    void Start()
    {
        ballisticPathLineRender = GetComponent<BallisticPathLineRender>();
        ballR = transform.GetComponent<SphereCollider>().bounds.size.y / 2;
        rb = GetComponent<Rigidbody>();
        // 使用其他方式
        infos = Director.Share.infoList.info;
    }

    void FixedUpdate()
    {
        nextPosition = transform.position + rb.velocity * Time.fixedDeltaTime;
        // 实时获取小球运动状态
        V = rb.velocity;
        // rb.CalculateMovement()
    }
    Vector3 v = Vector3.zero;
    public Vector3 rbV;

    public bool ballMove;

    public Vector3 confirmPosition;

    void Update()
    {

    }

    /// <summary>
    /// 根据重力和速度的值，获取timeOffset之后的世界坐标值
    /// </summary>
    /// <param name="timeOffset"></param>
    /// <returns></returns>
    public Vector3 GetBallPosition(float timeOffset)
    {
        Vector2 originPosition = new Vector2(transform.position.x, transform.position.y);
        Vector2 velocity = new Vector2(rb.velocity.x, rb.velocity.y);
        Vector2 gravityEffect = Physics.gravity * timeOffset * timeOffset * 0.5f;
        var result = originPosition + velocity * timeOffset + new Vector2(gravityEffect.x, gravityEffect.y);
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(result, 1f);
        return result;
    }

    // 给予一个跳板法线方向的速度，结合重力计算小球位置。

    public Vector3 GetBallPosition(Vector3 panelNomal)
    {
        return panelNomal * speed + Physics.gravity;
    }
    // private void OnDrawGizmos()
    // {
    //     var position = GetBallPosition(Vector3.up);
    //     Gizmos.color = Color.yellow;
    //     Gizmos.DrawSphere(position, 1f);
    // }


    // public Vector3 GetBallPosition(Vector3 ballPosition, Vector3 velocity, float timeOffset)
    // {
    //     // Vector2 originPosition = new Vector2(transform.position.x, transform.position.y);
    //     // Vector2 velocity = new Vector2(rb.velocity.x, rb.velocity.y); 
    //     // Vector2 gravityEffect = Physics.gravity * timeOffset * timeOffset * 0.5f;
    //     var result = ballPosition + velocity * timeOffset + new Vector2(gravityEffect.x, gravityEffect.y);
    // }

    public Vector3 GetNextPositionOrigin(Vector3 originPosition, Vector3 initialVelocity, float time, out Vector3 highestPoint, out Vector3 finalPosition)
    {
        highestPoint = Vector3.zero;
        finalPosition = Vector3.zero;

        // 计算最高点
        float timeToHighestPoint = -initialVelocity.y / Physics.gravity.y;
        if (timeToHighestPoint > 0 && timeToHighestPoint < time)
        {
            highestPoint = originPosition + initialVelocity * timeToHighestPoint + 0.5f * Physics.gravity * timeToHighestPoint * timeToHighestPoint;
        }

        // 计算终点
        finalPosition = originPosition + initialVelocity * time + 0.5f * Physics.gravity * time * time;

        // 返回终点，此处可以根据需要返回最高点或其他信息
        return finalPosition;
    }

    public Vector3 GetNextPosition(Vector3 initialVelocity, float time, out Vector3 highestPoint, out Vector3 finalPosition)
    {
        highestPoint = Vector3.zero;
        finalPosition = Vector3.zero;

        // 计算最高点
        float timeToHighestPoint = -initialVelocity.y / Physics.gravity.y;
        if (timeToHighestPoint > 0 && timeToHighestPoint < time)
        {
            highestPoint = transform.position + initialVelocity * timeToHighestPoint + 0.5f * Physics.gravity * timeToHighestPoint * timeToHighestPoint;
        }

        // 计算终点
        finalPosition = transform.position + initialVelocity * time + 0.5f * Physics.gravity * time * time;

        // 返回终点，此处可以根据需要返回最高点或其他信息
        return finalPosition;
    }

    public void LineRendererController(Vector3 velocity)
    {
        // ballisticPathLineRender.projectile.velocity = velocity;
    }

    public void BallMove(float timeOffset)
    {
        DOTween.To(() => transform.position, x => x = transform.position, GetBallPosition(timeOffset) + Vector3.up * 2f, timeOffset);
        // transform.DOMove(GetBallPosition(timeOffset), timeOffset);
    }

    public Vector3 NextInReflect()
    {
        var lineRender = GetComponent<LineRenderer>();

        var count = lineRender.positionCount;
        var v = lineRender.GetPosition(count - 2) - lineRender.GetPosition(count - 1);
        return v;
    }


    public void StopMove()
    {
        rb.isKinematic = true;
        rb.Sleep();
    }
    public void StartMove()
    {
        rb.isKinematic = false;
        rb.WakeUp();
    }
    public void StartMove(Vector3 v)
    {
        StartMove();
        rb.velocity = v;
    }



}
