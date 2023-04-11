using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrajectoryRenderer : MonoBehaviour
{
    [SerializeField] private int lineLength;
    private LineRenderer lineRendererComponent;

    void Start()
    {
        lineRendererComponent = GetComponent<LineRenderer>();
    }

    public void ShowTraectory(Vector3 origin, Vector3 speed)
    {
        /*Vector3[] points = new Vector3[lineLength];
        lineRendererComponent.positionCount = points.Length;

        for (int i = 0; i < points.Length; i++)
        {
            float time = i * 0.1f;

            points[i] = origin + speed * time * time / 2f;
        }
        lineRendererComponent.SetPositions(points);
        */
        lineRendererComponent.SetPosition(0, speed);
        lineRendererComponent.SetPosition(1, origin);
    }
}
