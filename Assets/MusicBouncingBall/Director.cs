using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
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


    public Transform ball;


    private BallBehaviour ballBehaviour;

    private int _count;

    public int GetCount()
    {
        return _count;
    }



    private MusicBouncingBallData saveData = new MusicBouncingBallData();


    public void SetSaveData(BouncingData data)
    {
        saveData.data.Add(data);
    }

    public void Save()
    {
        File.WriteAllText("./SaveDataList.json", JsonUtility.ToJson(saveData));
    }

    /// <summary>
    ///  小球半径和跳板的高度和
    /// </summary>
    private float offset;


    void Start()
    {
        ballBehaviour = ball.GetComponent<BallBehaviour>();
        offset = ballBehaviour.ballR + PanelManager.Share.panelH;
        isConfirm = false;
        speed = 5;
        // ballBehaviour.StopMove();
        FileUtils fileUtils = new();
        string content = fileUtils.ReadFile("first");
        infoList = JsonUtility.FromJson<InfoList>(content);
        confirmAction += Confrim;
        inputHandler.confirm += Confirm;
        StartCoroutine(MainCircleDoTween());

        // emitter = GameObject.FindWithTag("Emitter");
    }

    // /// <summary>
    // /// 主进程循环
    // /// </summary>
    // void FixedUpdate()
    // {

    // }

    public IEnumerator MainCircleDoTween()
    {
        yield return new WaitUntil(() => PanelManager.Share.panelList.Count > 0);

        if (infoList == null)
        {
            throw new Exception("infoList is null");
        }

        List<Info> infos = infoList.info;
        _count = 0;
        while (_count < infos.Count)
        {
            if (_count == 0)
                ballBehaviour.StopMove();


            timeOffset = infos[_count].timeOffset;

            if (_count == 1)
            {
                var height = Physics.gravity.y * timeOffset * timeOffset / 2 - BallBehaviour.Share.ballR - 0.2f; // panel的厚度
                PanelManager.Share.CreatePanel(new Vector3(0, height), new Vector3(0, 90));
            }

            yield return new WaitForSeconds(timeOffset);
            // BallBehaviour.Share.GetComponent<Rigidbody>().isKinematic = true;

            // var result = EMath.SimulateBallPosition(timeOffset, Vector2.Reflect(BallBehaviour.Share.V, transform.up), BallBehaviour.Share.transform.position, 50, out List<Vector2> pointList);


            // BallBehaviour.Share.transform.position = result;




            yield return new WaitUntil(() => isConfirm);
            // 进入下一次循环
            _count++;
            isConfirm = false;
        }

    }


    public IEnumerator BallController(Vector3 position, float timeOffset)
    {

        yield return ball.DOMove(position, timeOffset);
    }
    private Func<bool, bool> confirmAction;
    public bool isConfirm { get; private set; }
    public bool Confrim(bool isConfirm)
    {
        bool confirmed = isConfirm;

        return !isConfirm;
    }
    public void Confirm()
    {
        Debug.Log("Confirm");
        // ball.GetComponent<DrawLine>().AddPower();
        isConfirm = true;
    }



}
