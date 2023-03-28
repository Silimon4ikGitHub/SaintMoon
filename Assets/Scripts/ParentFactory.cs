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
    [SerializeField] private float resource1;
    public float resource2;
    public float resource3;
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
            if (otherInventory.invenoryItem[i] != null)
                if(takingStoreSpace[i] == null)
                {
                    takingStoreSpace[i] = otherInventory.invenoryItem[i];
                    otherInventory.invenoryItem[i].GetComponent<Resource>().hasTaken = false;
                    otherInventory.invenoryItem[i] = null;
                    takingStoreSpace[i].transform.position = Vector3.MoveTowards(takingStoreSpace[i].transform.position, takingStorePlace[i].position, resourceSpeed);
                    resource1++;
                    otherInventory.currentCount--;
                }
        }
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
        GiveResources();
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
