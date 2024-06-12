using UnityEngine;
using HeathenEngineering.UnityPhysics;
using API = HeathenEngineering.UnityPhysics.API;
public class TestBallRaycast : MonoBehaviour
{
    public GameObject ball;
    private void Start()
    {
        var startPosition = ball.transform.position;
        var collider = ball.gameObject.GetComponent<Collider>();

        // // API.Ballistics.SphereCast(startPosition, collider, new Vector3(), .5f,)
        // var ballisticPath = new BallisticPath()
        // ballisticPath.Get();
    }
}
