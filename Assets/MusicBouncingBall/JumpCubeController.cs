using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class JumpCubeController : MonoBehaviour
{

    public GameObject bouncingBall;

    private Rigidbody ballRb;

    private Director director;

    // private Vector3 

    // Start is called before the first frame update
    void Start()
    {
        ballRb = bouncingBall.GetComponent<Rigidbody>();
        director = GameObject.Find("Director").GetComponent<Director>();
    }

    // Update is called once per frame
    void Update()
    {


    }
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("BouncingBall"))
        {
            StartCoroutine(JumpCubeHandler(other.gameObject));
            GameObject obj = other.gameObject;
            DrawLine drawLine = obj.GetComponent<DrawLine>();
            // 当接触到跳板的时候给小球一个力，但是要把小球的运动停止下来。
            Rigidbody rb = obj.gameObject.GetComponent<Rigidbody>();
            rb.isKinematic = true;
            Collider col = obj.GetComponent<Collider>();
            col.isTrigger = true;

            // 产生一个空白物体，用这个空白物体来模拟小球下一步的运动。

            drawLine.addedV = new Vector3(3, 3, 0);


        }
    }

    /// <summary>
    /// 跳板控制器。 
    /// </summary>
    /// <returns></returns>//
    public IEnumerator JumpCubeHandler(GameObject ball)
    {
        while (director.isConfirm)
        {

            yield return null;
        }
    }

}
