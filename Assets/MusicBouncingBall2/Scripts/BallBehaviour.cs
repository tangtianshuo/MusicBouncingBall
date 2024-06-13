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

        public Vector2 GetLastVector()
        {
            return lastV;
        }

        public virtual void Awake()
        {
            Share = this;


        }

        void Start()
        {
            StartCoroutine(PauseAfterTime(Director.Share.GetTimeOffsetList()[1]));
        }

        private Vector2 _simulatePosition;

        public void SetPosition(Vector2 simulatePosition)
        {
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
    }




}
