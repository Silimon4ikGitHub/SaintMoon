
using UnityEngine;

public class TrajectoryRenderer : MonoBehaviour
{
    [SerializeField] private int lineLength;
    private LineRenderer LineRendererComponent;

    void Start()
    {
        LineRendererComponent = GetComponent<LineRenderer>();
    }

    public void ShowTraectory(Vector3 origin, Vector3 speed)
    {
        LineRendererComponent.SetPosition(0, speed);
        LineRendererComponent.SetPosition(1, origin);
    }
}
