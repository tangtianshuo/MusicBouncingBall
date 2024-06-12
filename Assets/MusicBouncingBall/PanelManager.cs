using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PanelManager : MonoBehaviour
{
    public static PanelManager Share = null;

    public GameObject currentPanel;

    public List<GameObject> panelList;

    public float panelH;


    public virtual void Awake()
    {
        panelList = new List<GameObject>();
        Share = this;
        // 构建资源池
        LoadPanel(10);
        panelH = 1f;
        PlayerInputHandler.Share.CreatePanel += CreatePanel;
        currentPanel = new();
        Addressables.LoadAssetAsync<GameObject>("SimulateJumpPanel").Completed += (handle) =>
        {
            simulatePanel = Instantiate(handle.Result);
            simulatePanel.name += "_Simulate";
            simulatePanel.SetActive(false);
        };

    }


    void LateUpdate()
    {
        if (Time.frameCount % 100 == 0)
        {
            var count = 0;

            for (int i = 0; i < panelList.Count; i++)
            {
                if (panelList[i].activeSelf == true)
                {
                    count++;
                }
            }
            if (panelList.Count - count < 3)
            {
                LoadPanel(2);
            }
        }
    }

    public void CreatePanel()
    {
        Vector2 currentPanelPosition = new Vector2(BallBehaviour.Share.transform.position.x, BallBehaviour.Share.transform.position.y - BallBehaviour.Share.ballR - 0.1f);
        Vector2 currentPanelRotation = new Vector2(0, 90);
        currentPanel = CreatePanel(currentPanelPosition, currentPanelRotation);
    }

    public GameObject CreatePanel(Vector2 position, Vector2 rotation)
    {

        foreach (var item in GameObject.FindGameObjectsWithTag("JumpPanel"))
        {
            Debug.Log(item.name + "enable false");
            item.GetComponent<PanelBehaviour>().DestoryController();
        }


        var panel = PopPanel();

        panel.transform.SetPositionAndRotation(position, Quaternion.Euler(rotation));
        panel.SetActive(true);
        currentPanel = panel;

        return panel;

    }

    public GameObject CreatePanel(Vector2 position, Quaternion rotation)
    {

        foreach (var item in GameObject.FindGameObjectsWithTag("JumpPanel"))
        {
            Debug.Log(item.name + "enable false");
            item.GetComponent<PanelBehaviour>().DestoryController();
        }


        var panel = PopPanel();

        panel.transform.SetPositionAndRotation(position, rotation);
        panel.SetActive(true);


        return panel;

    }

    public GameObject PopPanel()
    {
        for (int i = 0; i < panelList.Count; i++)
        {
            if (panelList[i].activeSelf != true)
            {
                return panelList[i];
            }
        }
        return null;
    }

    public void LoadPanel(int num)
    {
        Addressables.LoadAssetAsync<GameObject>("JumpPanel").Completed += (handle) =>
          {
              for (int i = 0; i < num; i++)
              {
                  var panel = handle.Result;
                  //   Debug.Log(panel.name);
                  var go = Instantiate(panel);
                  go.SetActive(false);
                  panelList.Add(go);
              }
          };
    }

    public GameObject simulatePanel;
    public int impactIndex;

    /// <summary>
    /// 模拟跳板出现的位置
    /// </summary>
    public void SimulatePanelPosition()
    {

        impactIndex = Interrupt();
        if (impactIndex == 0)
        {
            return;
        }
        simulatePanel.SetActive(true);
        // CreatePanel(BallBehaviour.Share.ballisticPathLineRender.trajectory[i], new Vector2(0, 90));

        simulatePanel.transform.SetPositionAndRotation(BallBehaviour.Share.ballisticPathLineRender.trajectory[impactIndex], Quaternion.Euler(new Vector3(0, 90, 0)));

    }

    /// <summary>
    /// 根据音阶触发事件，来插入跳板
    /// </summary>
    public int Interrupt()
    {
        var timeOffset = Director.Share.timeOffset;
        var timeList = BallBehaviour.Share.ballisticPathLineRender.time;
        for (int i = 0; i < timeList.Count; i++)
        {
            if (timeOffset.ToShortString() == timeList[i].ToShortString())
            {
                Debug.Log("Interrupt Index " + i);

                return i;

            }
        }
        return 0;
    }



}
