using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : MonoBehaviour
{
    [SerializeField] private PlayerInventory inventory;
    [SerializeField] private LineRenderer line;
    public bool hasTaken;
    public bool isNearFactoryStore;
    public int myCount;
    public int myIndex;

    private void Awake()
    {
        inventory = GameObject.Find("Player").GetComponentInChildren<PlayerInventory>();
        line = GetComponent<LineRenderer>();
    }
    private void FixedUpdate()
    {
        MakeTaking();
    }

    private void MakeTaking()
    {
        if (inventory != null)
        {
            if (hasTaken)
            {
                transform.position = inventory.itemPoint[myCount].transform.position;
                gameObject.GetComponent<Collider>().enabled = false;
            }
        }
    }

    public void LerpRenderer(Vector3 target)
    {
        line.enabled = true;
        line.SetPosition(0, target);
    }
}
