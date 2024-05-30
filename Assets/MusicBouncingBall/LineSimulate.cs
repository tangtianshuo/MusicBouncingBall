using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class LineSimulate : MonoBehaviour
{

    public LineRenderer lineRenderer;
    public Transform ball;

    public int numPoints;

    void Awake()
    {
        numPoints = 50;
        if (lineRenderer == null)
        {
            lineRenderer = GetComponent<LineRenderer>();

        }
        lineRenderer.useWorldSpace = true;
        EventManager.Instance.LineSimulateAction += Simulate;

    }

    /// <summary>
    /// 使用LineRenderer 模拟小球运动轨迹
    /// </summary>
    public void Simulate(Vector3 initialVelocity, float time)
    {
        // lineRenderer
        var list = CalculateTrajectoryPoints(initialVelocity, time, numPoints);
        Debug.Log("list" + list.Length);
        foreach (var point in list)
        {
            lineRenderer.positionCount++;
            lineRenderer.SetPosition(lineRenderer.positionCount - 1, point);
        }

    }
    void FixedUpdate()
    {
        // lineRenderer.positionCount += 1;
        // lineRenderer.SetPosition(lineRenderer.positionCount - 1, ball.position);
    }

    /// <summary>
    /// 根据给定的速度，重力，时间，计算终点位置和过程的路径点集数组
    /// </summary>
    /// <param name="initialVelocity">初始速度</param>
    /// 
    /// <param name="time">总时间</param>
    /// <returns>路径点集数组</returns>
    public Vector3[] CalculateTrajectoryPoints(Vector3 initialVelocity, float time, int numPoints)
    {
        if (numPoints == 0)
        {
            numPoints = 50; // 路径点的数量
        }
        Vector3[] points = new Vector3[numPoints];
        float deltaTime = time / numPoints; // 每个点之间的时间间隔

        for (int i = 0; i < numPoints; i++)
        {
            float t = deltaTime * i;
            Vector3 position = transform.position + initialVelocity * t + 0.5f * Vector3.down * Physics.gravity.y * t * t;
            points[i] = position;
            Debug.Log(position);
        }

        return points;
    }

}
