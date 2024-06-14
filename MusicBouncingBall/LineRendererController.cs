using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRendererController : MonoBehaviour
{
    public Rigidbody ballRigidBody;
    public LineRenderer lineRenderer;

    public IEnumerator LineController()
    {

        yield return new WaitForSeconds(0);
    }

    void DrawParabola()
    {
        List<Vector3> points = new List<Vector3>();
        Vector3 currentVelocity = ballRigidBody.velocity;
        Vector3 currentPosition = transform.position;
        for (float t = 0; t < GetRandomTimeOffset(); t += 0.1f) // 预测未来2秒的位置
        {
            Vector3 predictedPosition = currentPosition + currentVelocity * t + 0.5f * Physics.gravity * t * t;
            points.Add(predictedPosition);
        }
        lineRenderer.positionCount = points.Count;
        lineRenderer.SetPositions(points.ToArray());
    }

    public float GetRandomTimeOffset()
    {
        return Random.Range(0, 2f);
    }
}
