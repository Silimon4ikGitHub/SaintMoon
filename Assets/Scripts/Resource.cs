using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : MonoBehaviour
{
    [SerializeField] private float mySpeed;
    [SerializeField] private PlayerInventory inventory;
    [SerializeField] private LineRenderer line;
    [SerializeField] private TrajectoryRenderer traectoryRenderer;
    [SerializeField] private Vector3 traectory;
    public bool hasTaken;
    public bool isMove;
    public bool isNearFactoryStore;
    public int myCount;
    public int myIndex;
    public Transform dirrection;

    private void Awake()
    {
        inventory = GameObject.Find("Player").GetComponentInChildren<PlayerInventory>();
        line = GetComponent<LineRenderer>();
        dirrection = GetComponent<Transform>();
        traectoryRenderer = GetComponentInChildren<TrajectoryRenderer>();
    }
    private void FixedUpdate()
    {
        MakeTaking();
        MoveResource(dirrection);
        traectory = Vector3.MoveTowards(transform.position, dirrection.position, mySpeed);

        if (dirrection != null && traectoryRenderer != null)
        traectoryRenderer.ShowTraectory(transform.position, dirrection.position);
    }

    private void MakeTaking()
    {
        if (inventory != null)
        {
            if (hasTaken)
            {
                //transform.position = inventory.itemPoint[myCount].transform.position;
                dirrection = inventory.itemPoint[myCount].transform;
                gameObject.GetComponent<Collider>().enabled = false;
            }
        }
    }

    private void MoveResource(Transform target)
    {
        //transform.position = Vector3.MoveTowards(transform.position, target.position, mySpeed);
        transform.position = Vector3.Lerp(transform.position, target.position, mySpeed);
        Debug.DrawLine(transform.position, target.position, Color.red, 1f);
    }

    public void LerpRenderer(Vector3 target)
    {
        line.enabled = true;
        line.SetPosition(0, target);
    }
}
