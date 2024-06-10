#if HE_SYSCORE
using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace HeathenEngineering.UnityPhysics
{
    [RequireComponent(typeof(LineRenderer))]
    public class BallisticPathLineRender : MonoBehaviour
    {
        public enum GravityMode
        {
            None,
            Physics,
            Custom
        }

        [Header("Launch Settings")]
        public Vector3 start;
        public BallisticsData projectile;

        [Header("Behaviour Settings")]
        public bool runOnStart = true;
        public bool continuousRun = false;
        public LayerMask collisionLayers = 0;
        public float resolution = 0.1f;
        public float maxLength = 10f;
        public float maxBounces = 0;
        public float bounceDamping = 0.2f;
        public GravityMode gravityMode = GravityMode.Physics;
        public Vector3 customGravity = Vector3.zero;

        private LineRenderer lineRenderer;

        public (Vector3 position, Vector3 velocity, float time)[] steps;

        public List<float> time;

        private void Start()
        {
            lineRenderer = GetComponent<LineRenderer>();
            lineRenderer.useWorldSpace = true;
            time = new List<float>();
            if (runOnStart)
                Simulate();
        }

        private void FixedUpdate()
        {

            if (continuousRun)
                Simulate();
        }
        public List<Vector3> trajectory = new();
        float flightTime;
        public void Simulate()
        {
            resolution = Mathf.Max(resolution, 0.001f);

            if (projectile.Speed > 0)
            {
                Vector3 cGrav = Vector3.zero;

                if (gravityMode == GravityMode.Custom)
                    cGrav = customGravity;
                else if (gravityMode == GravityMode.Physics)
                    cGrav = Physics.gravity;

                var impacts = new List<RaycastHit>();
                trajectory = new List<Vector3>();

                var result = projectile.Predict(start, null, resolution, maxLength, collisionLayers, cGrav);
                steps = result.steps;
                flightTime = result.flightTime;
                Debug.Log(flightTime);
                foreach (var step in result.steps)
                {
                    trajectory.Add(step.position);
                }

                if (result.impact.HasValue)
                {
                    impacts.Add(result.impact.Value);

                    if (maxBounces > 0)
                    {
                        var remainingLength = maxLength - result.flightDistance;
                        var project = projectile;
                        var (position, velocity, time) = result.steps[result.steps.Length - 1];
                        project.velocity = Vector3.Reflect(velocity, result.impact.Value.normal);
                        project.velocity *= (1f - bounceDamping);

                        for (int i = 0; i < maxBounces; i++)
                        {
                            if (remainingLength > 0)
                            {
                                result = project.Predict(position, result.impact.Value.collider, resolution, remainingLength, collisionLayers, cGrav);
                                foreach (var step in result.steps)
                                    trajectory.Add(step.position);
                                if (result.impact.HasValue)
                                    impacts.Add(result.impact.Value);
                                remainingLength -= result.flightDistance;

                                if (result.impact.HasValue)
                                {
                                    (position, velocity, time) = result.steps[result.steps.Length - 1];
                                    project.velocity = Vector3.Reflect(velocity, result.impact.Value.normal);
                                    project.velocity *= (1f - bounceDamping);
                                }
                                else
                                    break;
                            }
                        }
                    }
                }
                time.Clear();
                foreach (var step in result.steps)
                {
                    time.Add(step.time);
                }
                lineRenderer.positionCount = trajectory.Count;
                lineRenderer.SetPositions(trajectory.ToArray());
            }
        }
    }
}


#endif