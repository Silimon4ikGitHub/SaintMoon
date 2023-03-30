using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ParentFactory : MonoBehaviour
{
    [SerializeField] public float makingTime;
    [SerializeField] private bool ready;
    [SerializeField] public int resourceSpeed;
    [SerializeField] private GameObject playerInventory;
    [SerializeField] private GameObject takingStore;
    [SerializeField] private GameObject givingStore;
    public Transform[] takingStorePlace;
    public Transform[] GivingStorePlace;
    public GameObject[] takingStoreSpace;
    public GameObject[] GivingStoreSpace;
    public GameObject[] resourcePrefabs;
    public bool isOnTimer;
    public float timer;
    public int takingResource1Index;
    public int takingResource2Index;
    public int takingResource3Index;
    public int resource1Required;
    public int resource2Required;
    public int resource3Required;
    public int resource1;
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
        if (GivingStoreSpace[GivingStoreSpace.Length - 1] == null)
        {
            if (resource1 >= resource1Required)
            {
                isOnTimer = true;
                if (timer > makingTime)
                {
                    for (int i = 0; i < resource1Required; i++)
                    {
                        takingStoreSpace[resource1 - 1].transform.position = Vector3.MoveTowards(takingStoreSpace[resource1 - 1].transform.position, transform.position, resourceSpeed);
                        Destroy(takingStoreSpace[resource1 - 1].gameObject, makingTime);
                        takingStoreSpace[resource1 - 1] = null;
                        resource1--;
                    }
                    resource2++;
                    timer = 0;
                }

            }
            else isOnTimer = false;
        }
        else isOnTimer = false;
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
    public virtual void CheckGivingStore()
    {
        for (int i = 0; i < GivingStoreSpace.Length; i++)
        {
            if (GivingStoreSpace[i] != null)
            {
                if (GivingStoreSpace[i].transform.position != GivingStorePlace[i].position)
                {
                    GivingStoreSpace[i] = null;
                }
            }
        }
    }

    public virtual void GoTimer()
    {
        if (isOnTimer)
        {
            timer++;
            //Debug.Log(timer);
        }
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

    public void CheckMyStore()
    {
        CheckGivingStore();
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
