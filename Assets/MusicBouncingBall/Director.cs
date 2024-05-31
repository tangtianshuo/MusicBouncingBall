using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.AddressableAssets;

/*
 * 作者: 唐天硕
 * 创建: 2024-05-27 22:05
 * 版权: myself
 *
 * 描述: 小球自由落体后，触碰到第一个倾斜板子，并进行弹射，弹射过程全程记录在LineRenderer中。
 * 弹射后第一次到时间后会进行计时，达到时间后暂停并生成一个板子。
 * 生成板子预测二次弹射后的位置，并给小球施加一个反射力。以此进行循环。
 */


public class Director : MonoBehaviour
{
    public static Director Share = null;
    public virtual void Awake()
    {
        Share = this;

    }
    public PanelManager panelManager;

    public PlayerInputHandler inputHandler;

    public float timeOffset;

    public int speed;
    public InfoList infoList;

    private float ballR;

    private float panelH;

    private float grivate;

    public Transform ball;

    private GameObject panel;

    private BallBehaviour ballBehaviour;
    private PanelBehaviour panelBehaviour;

    /// <summary>
    ///  小球半径和跳板的高度和
    /// </summary>
    private float offset;


    void Start()
    {
        ballBehaviour = ball.GetComponent<BallBehaviour>();
        offset = ballBehaviour.ballR + PanelManager.Share.panelH;
        grivate = Physics.gravity.y;
        isConfirm = false;
        speed = 5;

        FileUtils fileUtils = new();
        string content = fileUtils.ReadFile("first");
        infoList = JsonUtility.FromJson<InfoList>(content);
        confirmAction += Confrim;
        inputHandler.confirm += Confirm;
        StartCoroutine(MainCircle());

        // emitter = GameObject.FindWithTag("Emitter");
    }

    // /// <summary>
    // /// 主进程循环
    // /// </summary>
    // void FixedUpdate()
    // {

    // }

    #region  废案
    public IEnumerator MainCircle()
    {

        if (infoList == null)
        {
            throw new Exception("infoList is null");
        }

        List<Info> infos = infoList.info;
        int count = 1;
        while (count < infos.Count)
        {
            ballBehaviour.Stop();
            yield return new WaitUntil(() => PanelManager.Share.panelList.Count > 0);
            ballBehaviour.StartMove();
            if (PanelManager.Share.panelList.Count < 0)
            {
                yield return null;
            }
            Info info = infos[count];
            timeOffset = info.timeOffset;
            var ballPosition = ballBehaviour.GetBallPosition(timeOffset);

            // 计算下一块板子的位置
            var panelPosition = new Vector2(ballPosition.x, ballPosition.y);
            var panelRotation = new Vector2();
            var v = ballBehaviour.V;
            // var angle = Vector3.Angle(panel.transform.up, v.normalized);
            // panelRotation = new Vector2(angle, 90);
            if (count == 1)
            {
                panelRotation = new Vector2(20, 90);
            }
            var currentPanel = PanelManager.Share.CreatePanel(panelPosition, panelRotation);

            EventManager.Instance.LineSimulateAction.Invoke(new Vector3(), timeOffset);
            // foreach (var item in GameObject.FindGameObjectsWithTag("JumpPanel"))
            // {
            //     Debug.Log(item.name + "enable false");
            //     item.GetComponent<PanelBehaviour>().DestoryController();
            // }

            // float distance = timeOffset * speed;
            // // Vector3 position = new Vector3(0, UnityEngine.Random.Range(0, 2), 0);
            // Vector3 position = new Vector3(0, distance, 0);
            // // 小球移动事件
            // Vector3 offset = new Vector3(0, ballR + panelH, 0);


            // Vector3 panelPosition = new(position.x, distance, position.z);
            // // 生成跳板

            // var currentPanel = Instantiate(panel, panelPosition, Quaternion.Euler(new Vector3(20, 90, 0)));

            // // Vector3.Reflect();
            // // 计算力的方向
            // emitter.speed = speed;

            // currentPanel.tag = "JumpPanel";
            // // 这里进入一个协程，去控制下一次跳动。
            // PanelController panelController = currentPanel.GetComponent<PanelController>();
            // // 实时获取法线方向
            // Vector3 panelNomal = new Vector3(0, 0, 0);
            // // Vector3.Reflect(); 
            // ball.GetComponent<Rigidbody>().AddForce(panelPosition, ForceMode.Impulse);


            // Debug.Log("Director Run");
            // API.Ballistics.Solution(ball, speed,);
            // yield return StartCoroutine(panelController.ControlPanel());

            // 等待确认事件

            yield return new WaitUntil(() => isConfirm);
            ball.GetComponent<Rigidbody>().velocity = currentPanel.GetComponent<PanelBehaviour>().reflectV * 2f;
            // 进入下一次循环
            count++;
            isConfirm = false;
            yield return null;

        }
    }
    #endregion
    public IEnumerator BallController(Vector3 position, float timeOffset)
    {

        yield return ball.DOMove(position, timeOffset);
    }
    private Func<bool, bool> confirmAction;
    public bool isConfirm { get; private set; }
    void Update()
    {
        // if (Input.GetKeyDown(KeyCode.Alpha1))
        // {
        //     isConfirm = true;
        // }

    }
    public bool Confrim(bool isConfirm)
    {
        bool confirmed = isConfirm;

        return !isConfirm;
    }
    public void Confirm()
    {
        Debug.Log("Confirm");
        isConfirm = true;
    }



}
