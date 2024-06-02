using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class LineSimulate : MonoBehaviour
{

    public LineRenderer lineRenderer;
    public LineRenderer confirmLineRenderer;
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

    public void BallSimulate()
    {

    }

    public void SaveSimulate()
    {

    }

    /// <summary>
    /// 使用LineRenderer 模拟小球运动轨迹
    /// </summary>
    public void Simulate(Vector3 initialVelocity, float time)
    {
        // lineRenderer
        var list = CalculateTrajectoryPoints(initialVelocity, time, numPoints);
        // Debug.Log("list" + list.Length);
        var count = 0;
        for (int i = 0; i < list.Length; i++)
        {
            lineRenderer.SetPosition(i, list[i]);
        }
        // foreach (var point in list)
        // {

        //     lineRenderer.SetPosition(lineRenderer.positionCount - 51 + count, point);
        //     count++;
        // }

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
    /// <param name="time">总时间</param>
    /// <param name="numPoints">路径点的数量</param>
    /// <returns>路径点集数组</returns>
    public Vector3[] CalculateTrajectoryPoints(Vector3 initialVelocity, float time, int numPoints)
    {
        if (numPoints <= 0)
        {
            numPoints = 50; // 默认路径点的数量
        }
        Vector3[] points = new Vector3[numPoints];
        float deltaTime = time / numPoints; // 每个点之间的时间间隔

        for (int i = 0; i < numPoints; i++)
        {
            float t = deltaTime * i;
            Vector3 displacement = initialVelocity * t + 0.5f * Physics.gravity * t * t;
            Vector3 position = transform.position + displacement;
            points[i] = position;
            // Debug.Log("Trajectory point " + i + ": " + position);
        }

        return points;
    }

    public void ConfirmLine(int count, List<Vector2> points)
    {
        confirmLineRenderer.positionCount += count;
        var times = 0;
        foreach (var point in points)
        {

            confirmLineRenderer.SetPosition(confirmLineRenderer.positionCount - count + times, point);
            times++;
        }
    }
}
