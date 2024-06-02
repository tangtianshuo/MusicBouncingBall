using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/*
 * 作者: 唐天硕
 * 创建: 2024-05-15 20:52
 *
 * 描述: 获取 时间偏移值，预测时间偏移值之后的小球位置，并且画线，移动到指定位置。
 */


public class BallBehaviour : MonoBehaviour
{

    public float speed = 2;

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


    void Start()
    {
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

    }
    Vector3 v = Vector3.zero;
    public Vector3 rbV;

    void Update()
    {
        rbV = rb.velocity;
        if (Input.GetKeyDown(KeyCode.K))
        {
            Debug.Log(rb.velocity);
            v = rb.velocity;
            rb.Sleep();
        }
        if (Input.GetKeyUp(KeyCode.K))
        {

            rb.WakeUp();
            rb.velocity = v;

        }
    }

    /// <summary>
    /// 根据重力和速度的值，获取timeOffset之后的世界坐标值
    /// </summary>
    /// <param name="timeOffset"></param>
    /// <returns></returns>
    public Vector2 GetBallPosition(float timeOffset)
    {
        Vector2 originPosition = new Vector2(transform.position.x, transform.position.y);
        Vector2 velocity = new Vector2(rb.velocity.x, rb.velocity.y);
        Vector2 gravityEffect = Physics.gravity * timeOffset * timeOffset * 0.5f;
        return originPosition + velocity * timeOffset + new Vector2(gravityEffect.x, gravityEffect.y);
    }

    public Vector3 GetNextPosition()
    {
        // Vector3 ballDirection = nextPosition.normalized;
        // nextPosition = ballDirection * (transform.GetComponent<Collider>().bounds.size.x / 2);
        Debug.Log(nextPosition);
        return nextPosition;
    }

    public void LineRendererController(Vector3 velocity)
    {
        // ballisticPathLineRender.projectile.velocity = velocity;
    }

    public void Stop()
    {
        rb.Sleep();
    }
    public void StartMove()
    {
        rb.WakeUp();
    }
    public void StartMove(Vector3 v)
    {
        StartMove();
        rb.velocity = v;
    }

}
