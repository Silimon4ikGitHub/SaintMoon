using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public GameObject[] invenoryItem;
    public GameObject[] itemPoint;
    public Transform freeSpace;
    [SerializeField] private float inventorySpace;
    [SerializeField] private float itemSpeed;
    public int currentCount;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (currentCount < invenoryItem.Length)
            freeSpace = itemPoint[currentCount].transform;
    }

    private void Inventory()
    {

    }

    private void OnTriggerEnter(Collider collision)
    {
        
        if (collision.gameObject.GetComponent<Resource>() != null)
        {
            Resource currentRecource = collision.gameObject.GetComponent<Resource>();
            currentRecource.myCount = currentCount;

            if (currentCount < invenoryItem.Length)
            {
                currentRecource.hasTaken = true;
                currentRecource.dirrection = itemPoint[currentCount].transform;
                //collision.gameObject.transform.position = Vector3.MoveTowards(collision.gameObject.transform.position, itemPoint[currentCount].transform.position, itemSpeed);
                invenoryItem[currentCount] = collision.gameObject;
                currentCount++;
            }
        }
    }
    //private void OnCollisionEnter(Collision collision)
    //{
     //   Resource currentRecource = collision.gameObject.GetComponent<Resource>();
     //   currentRecource.hasTaken = true;
    //}
}
