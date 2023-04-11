using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ParentFactory : MonoBehaviour
{
    [SerializeField] public float makingTime;
    [SerializeField] private bool ready;
    [SerializeField] private bool isResourceMove;
    [SerializeField] public float resourceSpeed;
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
    public FactoryProcessData ProcessData { get; private set; }

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
                            this.ChangeStoreAndArray(ref takingStoreSpace[i], ref otherInventory.invenoryItem[currentArrayIndex], ref takingStorePlace[i]);
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
                        DestroyResource(ref takingStoreSpace[resource1 - 1]);
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
                if (GivingStoreSpace[i].transform.position != GivingStorePlace[i].position)
                {
                    GivingStoreSpace[i] = null;
                }
        }
    }
    public virtual void ChangeStoreAndArray(ref GameObject store, ref GameObject inventoryItem, ref Transform storePlace)
    {
        store = inventoryItem;
        Resource resource = inventoryItem.GetComponent<Resource>();
        resource.hasTaken = false;
        inventoryItem = null;
        resource.dirrection = storePlace;
    }

    public virtual void DestroyResource(ref GameObject storeWithResource)
    {
        storeWithResource.GetComponent<Resource>().dirrection = transform;
        Destroy(storeWithResource.gameObject, makingTime);
        storeWithResource = null;
    }

    public virtual void GoTimer()
    {
        if (isOnTimer)
        {
            timer++;
        }
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
    public void ChangeUI()
    {
        CheckProcess(timer, GivingStoreSpace);
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerInventory>() != null)
        {
            playerInventory = other.gameObject;
            TakeResourcesToStore(playerInventory, takingResource1Index, takingResource2Index, takingResource3Index);
        }
    }

    private FactoryProcessData CheckProcess(float time, GameObject[] store)
    {
        bool IsNoResources = false;
        bool IsWorking = false;
        bool IsStoreFull = false;

        if (time > 1)
        {
            IsNoResources = false;
            IsStoreFull = false;
            IsWorking = true;
        }
        else if (store[store.Length - 1] != null)
        {
            IsWorking = false;
            IsNoResources = false;
            IsStoreFull = true;
        }
        else
        {
            IsWorking = false;
            IsStoreFull = false;
            IsNoResources = true;
        }

        ProcessData = new FactoryProcessData(IsWorking, IsStoreFull, IsNoResources);
        return ProcessData;
    }

    public struct FactoryProcessData
    {
        public readonly bool IsWorking;
        public readonly bool IsStoreFull;
        public readonly bool IsNoResources;

        public FactoryProcessData(bool isWorking, bool isStoreFull, bool isNoResources)
        {
            IsWorking = isWorking;
            IsStoreFull = isStoreFull;
            IsNoResources = isNoResources;
        }
    }
}
