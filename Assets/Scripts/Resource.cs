
using UnityEngine;

public class Resource : MonoBehaviour
{
    [SerializeField] private float mySpeed;
    [SerializeField] private PlayerInventory inventory;
    [SerializeField] private LineRenderer line;
    [SerializeField] private TrajectoryRenderer traectoryRenderer;
    [SerializeField] private Vector3 traectory;
    public bool HasTaken;
    public bool IsMove;
    public bool IsNearFactoryStore;
    public int MyCount;
    public int MyIndex;
    public Transform Dirrection;

    private void Awake()
    {
        inventory = GameObject.Find("Player").GetComponentInChildren<PlayerInventory>();
        line = GetComponent<LineRenderer>();
        Dirrection = GetComponent<Transform>();
        traectoryRenderer = GetComponentInChildren<TrajectoryRenderer>();
    }
    private void FixedUpdate()
    {
        MakeTaking();
        MoveResource(Dirrection);
        traectory = Vector3.MoveTowards(transform.position, Dirrection.position, mySpeed);

        if (Dirrection != null && traectoryRenderer != null)
        traectoryRenderer.ShowTraectory(transform.position, Dirrection.position);
    }

    private void MakeTaking()
    {
        if (inventory != null)
        {
            if (HasTaken)
            {
                Dirrection = inventory.ItemPoint[MyCount].transform;
                gameObject.GetComponent<Collider>().enabled = false;
            }
        }
    }

    private void MoveResource(Transform target)
    {
        transform.position = Vector3.Lerp(transform.position, target.position, mySpeed);
        Debug.DrawLine(transform.position, target.position, Color.red, 1f);
    }

    public void LerpRenderer(Vector3 target)
    {
        line.enabled = true;
        line.SetPosition(0, target);
    }
}
