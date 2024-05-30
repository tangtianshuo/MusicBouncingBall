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

    public PlayerInputHandler inputHandler;

    public float timeOffset;

    public int speed;
    public InfoList infoList;

    public Transform ball;

    private float ballR;

    private float panelH;

    private float grivate;

    private GameObject panel;


    void Start()
    {
        isConfirm = false;
        speed = 5;
        ballR = ball.GetComponent<SphereCollider>().bounds.size.y / 2;
        Addressables.LoadAssetAsync<GameObject>("JumpPanel").Completed += (handle) =>
         {
             panel = handle.Result;
             panelH = panel.GetComponent<Collider>().bounds.size.y;

             grivate = Physics.gravity.y;
             FileUtils fileUtils = new();
             string content = fileUtils.ReadFile("first");
             Debug.Log(content);
             infoList = JsonUtility.FromJson<InfoList>(content);
             confirmAction += Confrim;
             inputHandler.confirm += Confirm;
             StartCoroutine(MainCircle());
         };
        // emitter = GameObject.FindWithTag("Emitter");
    }


    // public IEnumerator MainCircle()
    // {
    //     if (infoList == null)
    //     {
    //         throw new Exception("infoList is null");
    //     }

    //     yield return null;
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
            Info info = infos[count];
            timeOffset = info.timeOffset;

            // yield return StartCoroutine();
            EventManager.Instance.LineSimulateAction.Invoke(new Vector3(), timeOffset);


            float distance = timeOffset * speed;
            // Vector3 position = new Vector3(0, UnityEngine.Random.Range(0, 2), 0);
            Vector3 position = new Vector3(0, distance, 0);
            // 小球移动事件
            Vector3 offset = new Vector3(0, ballR + panelH, 0);


            Vector3 panelPosition = new(position.x, distance, position.z);
            // 生成跳板
            foreach (var item in GameObject.FindGameObjectsWithTag("JumpPanel"))
            {
                Debug.Log(item.name + "enable false");
                item.GetComponent<PanelController>().DestoryController();
            }
            var currentPanel = Instantiate(panel, panelPosition, Quaternion.Euler(new Vector3(20, 90, 0)));

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
            ball.GetComponent<Rigidbody>().velocity = currentPanel.GetComponent<PanelController>().reflectV;
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
