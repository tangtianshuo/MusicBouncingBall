using System.Collections.Generic;
using UnityEngine;

public partial class EMath
{
    public static Vector3 Parabola(Vector3 start, Vector3 end, float height, float t)
    {
        float Func(float x) => 4 * (-height * x * x + height * x);

        var mid = Vector3.Lerp(start, end, t);

        return new Vector3(mid.x, Func(t) + Mathf.Lerp(start.y, end.y, t), mid.z);
    }

    public static Vector2 Parabola(Vector2 start, Vector2 end, float height, float t)
    {
        float Func(float x) => 4 * (-height * x * x + height * x);

        var mid = Vector2.Lerp(start, end, t);

        return new Vector2(mid.x, Func(t) + Mathf.Lerp(start.y, end.y, t));
    }

    /// <summary>
    /// 根据给定的条件，返回受重力影响下的小球最终坐标，pointList 是小球的运动轨迹，点位数量根据pointNumber进行决定
    /// </summary>
    /// <param name="time">模拟的总时间</param>
    /// <param name="velocity">初始速度向量</param>
    /// <param name="currentPosition">物体当前的位置</param>
    /// <param name="pointNumber">轨迹点的数量</param>
    /// <param name="pointList">输出的轨迹点列表</param>
    /// <returns>最终位置的向量</returns>
    public static Vector2 SimulateBallPosition(float time, Vector2 velocity, Vector2 currentPosition, int pointNumber, out List<Vector2> pointList)
    {
        pointList = new List<Vector2>();
        float gravity = -9.81f; // 重力加速度
        float deltaTime = time / pointNumber; // 每个点的时间间隔

        Vector2 currentVelocity = velocity;

        for (int i = 0; i < pointNumber; i++)
        {
            // 更新位置
            currentPosition += currentVelocity * deltaTime;
            // 将当前位置添加到轨迹列表
            pointList.Add(currentPosition);
            // 更新速度
            currentVelocity.y += gravity * deltaTime;
        }

        return currentPosition; // 返回最终位置
    }
}
