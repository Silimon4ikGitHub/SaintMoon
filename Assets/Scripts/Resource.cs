using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : MonoBehaviour
{
    [SerializeField] private PlayerInventory inventory;
    public bool hasTaken;
    public bool isNearFactoryStore;
    public int myCount;

    private void Awake()
    {
        inventory = GameObject.Find("Player").GetComponentInChildren<PlayerInventory>();
    }
    private void FixedUpdate()
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
}
