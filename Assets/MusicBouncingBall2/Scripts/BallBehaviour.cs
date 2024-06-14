using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
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
            StartCoroutine(PauseAfterTime(Director.Share.GetTimeOffsetList()[1]));
        }

        private Vector2 _simulatePosition;

        public void SetPosition(Vector2 simulatePosition)
        {
            _lastPosition = _simulatePosition;
            _simulatePosition = simulatePosition;
        }
        public void Update()
        {
            var v = lastV;
            lastV = new Vector2(transform.position.x, transform.position.y) - lastV;
            Debug.Log(lastV.normalized);
            if (_simulatePosition != Vector2.zero)
            {
                transform.DOMove(_simulatePosition, 0.02f).SetEase(Ease.Linear).Complete();
                _simulatePosition = Vector2.zero;
            }
        }


        public Vector2 GetLastPosition()
        {
            return _lastPosition;
        }
        public void FixedUpdate()
        {

        }


        IEnumerator PauseAfterTime(float time)
        {
            Debug.Log(time);
            // 等待指定的时间
            yield return new WaitForSeconds(time);

            // 时间到后，暂停刚体
            GetComponent<Rigidbody>().isKinematic = true;
        }

        public void BouncingForceSimulate(Vector2 ballPosition, Vector2 normal)
        {
            var bouncingForce = new Vector2();
            bouncingForce = Vector2.Reflect(lastV, normal);
            var result = BouncingUtiils.SimulateBallPosition(Director.Share.GetTimeOffsetList()[2], bouncingForce, ballPosition, 50, out List<Vector2> pointList);
            _simulatePosition = result;
            Debug.Log(result);
            // SetPosition(result);
            // return bouncingForce;
        }



    }




}
