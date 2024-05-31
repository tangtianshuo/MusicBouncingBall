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


    private PanelBehaviour panelBehaviour;

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
            if (count > 6)
            {
                LoadPanel(2);
            }
        }
    }
    public GameObject CreatePanel(Vector2 position, Vector2 rotation)
    {
        var panel = PopPanel();
        panel.transform.SetPositionAndRotation(position, Quaternion.Euler(rotation));
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
                  Debug.Log(panel.name);
                  var go = Instantiate(panel);
                  go.SetActive(false);
                  panelList.Add(go);
              }
          };
    }



}
