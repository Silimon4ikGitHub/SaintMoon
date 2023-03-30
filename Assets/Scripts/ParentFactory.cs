using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ParentFactory : MonoBehaviour
{
    [SerializeField] private float timer;
    [SerializeField] public float makingTime;
    [SerializeField] private bool ready;
    [SerializeField] public int resourceSpeed;
    [SerializeField] private GameObject playerInventory;
    [SerializeField] private GameObject takingStore;
    [SerializeField] private GameObject givingStore;
    [SerializeField] public Transform[] takingStorePlace;
    [SerializeField] public Transform[] GivingStorePlace;
    [SerializeField] public GameObject[] takingStoreSpace;
    [SerializeField] public GameObject[] GivingStoreSpace;
    [SerializeField] public GameObject[] resourcePrefabs;
    [SerializeField] public int resource1;
    [SerializeField] public int takingResource1Index;
    [SerializeField] public int takingResource2Index;
    [SerializeField] public int takingResource3Index;
    [SerializeField] public int resource1Required;
    [SerializeField] public int resource2Required;
    [SerializeField] public int resource3Required;
    public int resource2;
    public int resource3;
    public int resource4;
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
        
        for (int i = 0; i < GivingStorePlace.Length; i++)
        {
            if (GivingStorePlace[i] == null)
            {
                GivingStorePlace[i] = givingStore.transform.GetChild(i);
            }
        }
    }

    public virtual void TakeResources()
    {
        
    }
    public virtual void TakeResourcesToStore(GameObject inventory, int resource1Index, int resource2Index, int takingResource3Index)
    {
        PlayerInventory otherInventory = inventory.GetComponent<PlayerInventory>();
        
        for (int i = 0; i < otherInventory.invenoryItem.Length; i++)
        {
            int currentArrayIndex = otherInventory.currentCount - 1;
            
            if (otherInventory.currentCount >= 1)
            if (otherInventory.invenoryItem[currentArrayIndex] != null)
            if (otherInventory.invenoryItem[currentArrayIndex].GetComponent<Resource>().myIndex == resource1Index)
                if (takingStoreSpace[i] == null)
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
            for (int i = 0; i < resource1Required; i++)
            {
                takingStoreSpace[resource1 - 1].transform.position = Vector3.MoveTowards(takingStoreSpace[resource1 - 1].transform.position, transform.position, resourceSpeed);
                Destroy(takingStoreSpace[resource1 - 1].gameObject, makingTime);
                takingStoreSpace[resource1 - 1] = null;
                resource1--;
            }
            resource2++;
        }
    }
    public virtual void GiveResources()
    {
        for (int i = 0; i < GivingStoreSpace.Length; i++)
        {
           if (resource2 > 0)
           if (GivingStoreSpace[i] == null)
           {
              GivingStoreSpace[i] = Instantiate(resourcePrefabs[1], GivingStorePlace[i].position, transform.rotation);
              resource2--;
           }
        }
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

    public void GiveOneStuck()
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
            TakeResourcesToStore(playerInventory, takingResource1Index, takingResource2Index, takingResource3Index);
        }
    }
}
