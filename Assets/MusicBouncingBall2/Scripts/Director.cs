using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MusicBouncingBall
{
    public class Director : MonoBehaviour
    {

        #region 单例模式
        public static Director Share = null;
        public virtual void Awake()
        {
            _timeOffsetList = new List<float>();
            _timeOffsetList = GetTimeOffsetList();
            _currentTimeOffset = _timeOffsetList[count];
            Share = this;
            // EventManager.Share.ConfirmAction += Confirm;
        }
        #endregion

        #region  私有变量
        private float _currentTimeOffset;

        private float _grivaty = -9.8f;

        private List<float> _timeOffsetList;

        #endregion

        /// <summary>
        /// 小球运行速度系数
        /// </summary>
        public int ballSpeed;

        /// <summary>
        /// 弹性系数
        /// </summary>
        public float elastic;

        void Start()
        {
            EventManager.Share.ConfirmAction += Confirm;

        }
        public int count;

        void Update()
        {
            var _count = count;
            // Debug.Log("Current Count" + count);
            if (count > _count)
            {
                Debug.Log("count changed");
                // _currentTimeOffset = _timeOffsetList[count];
                // var position = BouncingUtiils.SimulateBallPosition(GetCurrentTimeOffset(), new Vector2(0, 0), Vector2.zero, 50, out List<Vector2> pointList);
                // lastPosition = position;
                // var LineRenderer = GetComponent<LineRenderer>();
                // LineRenderer.positionCount = 50;
                // LineRenderer.SetPositions(BouncingUtiils.Vector2List2Vector3List(pointList).ToArray());
                // BallBehaviour.Share.SetPosition(position);
            }
        }
        public bool confirm = false;



        public List<float> GetTimeOffsetList()
        {
            List<float> timeOffsetList = new List<float>();
            FileUtils fileUtils = new();
            string content = fileUtils.ReadFile("first");
            var infoList = JsonUtility.FromJson<InfoList>(content);
            foreach (var info in infoList.info)
            {
                timeOffsetList.Add(info.timeOffset);
            }

            return timeOffsetList;
        }


        public float GetCurrentTimeOffset()
        {
            return _currentTimeOffset;
        }

        public float GetGrivaty()
        {
            return _grivaty;
        }

        // public Vector2 lastPosition;
        // public void OnDrawGizmos()
        // {
        //     if (lastPosition != Vector2.zero)
        //     {
        //         Gizmos.color = Color.green;
        //         Gizmos.DrawSphere(lastPosition, 0.1f);
        //     }
        // }
        public void Confirm()
        {
            confirm = true;
            Debug.Log("Confirm");
            count++;
            _currentTimeOffset = _timeOffsetList[count];

            if (count == 1)
            {
                StartCoroutine(BallBehaviour.Share.PauseAfterTime(GetCurrentTimeOffset()));
                BallBehaviour.Share.RecordLastV();

            }
        }

    }


    public static class BouncingUtiils
    {
        public static List<Vector3> Vector2List2Vector3List(List<Vector2> list)
        {
            var result = new List<Vector3>();
            foreach (var item in list)
            {
                result.Add(new Vector3(item.x, item.y, 0));
            }
            return result;
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
            float gravity = Director.Share.GetGrivaty(); // 重力加速度
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
}