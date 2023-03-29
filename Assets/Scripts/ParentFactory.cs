using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ParentFactory : MonoBehaviour
{
    [SerializeField] private float timer;
    [SerializeField] private float makingTime;
    [SerializeField] private bool ready;
    [SerializeField] private int resourceSpeed;
    [SerializeField] private GameObject playerInventory;
    [SerializeField] private GameObject takingStore;
    [SerializeField] private Transform[] takingStorePlace;
    [SerializeField] private GameObject[] takingStoreSpace;
    [SerializeField] private int resource1;
    [SerializeField] private int resource1Required;
    public int resource2;
    public int resource3;
    public Collider takeTrigger;

    private void Start()
    {
        takeTrigger = GetComponent<Collider>();
        for (int i = 0; i < takingStorePlace.Length; i++)
        {
            if (takingStorePlace[i] == null)
            {
                takingStorePlace[i] = takingStore.transform.GetChild(i);
            }
        }
    }


    public virtual void TakeResources()
    {
        
    }
    public virtual void TakeResourcesToStore(GameObject inventory)
    {
        PlayerInventory otherInventory = inventory.GetComponent<PlayerInventory>();

        for (int i = 0; i < otherInventory.invenoryItem.Length; i++)
        {
            int currentArrayIndex = otherInventory.currentCount - 1;

            if (otherInventory.currentCount >= 1)
            if (otherInventory.invenoryItem[currentArrayIndex] != null)
                if(takingStoreSpace[i] == null)
                {
                    takingStoreSpace[i] = otherInventory.invenoryItem[currentArrayIndex];
                    otherInventory.invenoryItem[currentArrayIndex].GetComponent<Resource>().hasTaken = false;
                    otherInventory.invenoryItem[currentArrayIndex] = null;
                    takingStoreSpace[i].transform.position = Vector3.MoveTowards(takingStoreSpace[i].transform.position, takingStorePlace[i].position, resourceSpeed);
                    resource1++;
                    otherInventory.currentCount--;
                }
        }
    }
    public virtual void MakeResources()
    {
        if (resource1 >= resource1Required)
        {
            int rest = resource1 + resource1Required;

            for (int i = 0; i < resource1Required; i++)
            {
                takingStoreSpace[resource1 - 1].transform.position = Vector3.MoveTowards(takingStoreSpace[resource1 - 1].transform.position, transform.position, resourceSpeed);
                Destroy(takingStoreSpace[resource1 - 1].gameObject, 1);
                takingStoreSpace[resource1 - 1] = null;
                Debug.Log("here is working");
                resource1--;
            }
            //resource1 -= resource1Required;
        }
        /*{
            Destroy(takingStoreSpace[resource1 - 1].gameObject);
            Destroy(takingStoreSpace[resource1 - 2].gameObject);
            Destroy(takingStoreSpace[resource1 - 3].gameObject);

            takingStoreSpace[resource1 - 1] = null;
            takingStoreSpace[resource1 - 2] = null;
            takingStoreSpace[resource1 - 3] = null;
            resource1 -= resource1Required;
        }/***/
    }
    public virtual void GiveResources()
    {
        
    }

    public virtual void GoTimer()
    {
        timer++;

        if (timer > makingTime)
            ready = true;
        else
            ready = false;
        //Debug.Log(timer);
    }

    public void TakeOneStack()
    {
        TakeResources();
    }
    public void MakeOneStack()
    {
        MakeResources();
    }
    public void GoFactoryTimer()
    {
        GoTimer();
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerInventory>() != null)
        {
            playerInventory = other.gameObject;
            TakeResourcesToStore(playerInventory);
        }

    }


}
