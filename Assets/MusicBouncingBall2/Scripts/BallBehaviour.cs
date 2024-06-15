using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using HeathenEngineering.Events;
using UnityEngine;

namespace MusicBouncingBall
{

    public class BallBehaviour : MonoBehaviour
    {
        // Start is called before the first frame update
        public static BallBehaviour Share = null;

        private Vector2 lastV;

        private Director director = Director.Share;

        private Vector2 _lastPosition;
        public Vector2 GetLastVector()
        {
            return lastV;
        }

        public virtual void Awake()
        {
            Share = this;

            EventManager.Share.SimulatePositionAction += BouncingForceSimulate;
        }

        void Start()
        {

        }

        private Vector2 _simulatePosition;

        public void SetPosition(Vector2 simulatePosition)
        {
            _lastPosition = simulatePosition;
            _simulatePosition = simulatePosition;
            recordLastV = true;
            // Director.Share.Confirm();
            // StartCoroutine(PauseAfterTime(Director.Share.GetCurrentTimeOffset()));

        }
        private bool recordLastV;
        private bool pauseTime;
        public void Update()
        {
            if (_simulatePosition != Vector2.zero)
            {
                transform.DOMove(_simulatePosition, 0.02f).SetEase(Ease.Linear).Complete();
                _simulatePosition = Vector2.zero;


            }
            if (recordLastV)
            {
                RecordLastV();
            }
        }


        public void RecordLastV()
        {
            lastV = -new Vector2(transform.position.x, transform.position.y) - lastV;
            recordLastV = false;
            Debug.Log("LastV : " + lastV);
        }


        public Vector2 GetLastPosition()
        {
            Debug.Log("LastPosition" + _lastPosition);
            return _lastPosition;
        }
        public void FixedUpdate()
        {

        }


        public IEnumerator PauseAfterTime(float time)
        {
            pauseTime = false;
            Debug.Log(time);
            // 等待指定的时间
            GetComponent<Rigidbody>().isKinematic = false;

            yield return new WaitForSeconds(time);

            // 时间到后，暂停刚体
            GetComponent<Rigidbody>().isKinematic = true;
            _lastPosition = transform.position;
            RecordLastV();

        }

        public void BouncingForceSimulate(Vector2 ballPosition, Vector2 normal)
        {
            var bouncingForce = Vector2.Reflect(-lastV, normal) * 3f;
            var result = BouncingUtiils.SimulateBallPosition(Director.Share.GetCurrentTimeOffset(), bouncingForce, ballPosition, 50, out List<Vector2> pointList);
            _simulatePosition = result;
            Debug.Log(lastV);
            // SetPosition(result);
            // return bouncingForce;
        }





    }




}
