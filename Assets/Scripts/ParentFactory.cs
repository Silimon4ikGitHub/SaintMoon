using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ParentFactory : MonoBehaviour
{ 
    [SerializeField] private GameObject playerInventory;
    [SerializeField] private GameObject takingStore;
    [SerializeField] private GameObject givingStore;
    public bool IsOnTimer;
    public float ResourceSpeed;
    public float MakingTime;
    public float Timer;
    public int TakingResource1Index;
    public int TakingResource2Index;
    public int TakingResource3Index;
    public int Resource1Required;
    public int Resource2Required;
    public int Resource3Required;
    public int Resource1;
    public int Resource2;
    public int Resource3;
    public int Resource4;
    public Collider TakeTrigger;
    public Transform[] TakingStorePlace;
    public Transform[] GivingStorePlace;
    public GameObject[] TakingStoreSpace;
    public GameObject[] GivingStoreSpace;
    public GameObject[] ResourcePrefabs;
    public FactoryProcessData ProcessData { get; private set; }

    private void Start()
    {
        TakeTrigger = GetComponent<Collider>();
        for (int i = 0; i < TakingStorePlace.Length; i++)
        {
            if (TakingStorePlace[i] == null)
            {
                TakingStorePlace[i] = takingStore.transform.GetChild(i);
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

        for (int i = 0; i < otherInventory.InvenoryItem.Length; i++)
        {
            int currentArrayIndex = otherInventory.CurrentCount - 1;
            if (otherInventory.CurrentCount >= 1)
                if (otherInventory.InvenoryItem[currentArrayIndex] != null)
                    if (otherInventory.InvenoryItem[currentArrayIndex].GetComponent<Resource>().MyIndex == resource1Index)
                        if (TakingStoreSpace[i] == null)
                        {
                            this.ChangeStoreAndArray(ref TakingStoreSpace[i], ref otherInventory.InvenoryItem[currentArrayIndex], ref TakingStorePlace[i]);
                            Resource1++;
                            otherInventory.CurrentCount--;
                        }
        }
    }

    public virtual void MakeResources()
    {
        if (GivingStoreSpace[GivingStoreSpace.Length - 1] == null)
        {
            if (Resource1 >= Resource1Required)
            {
                IsOnTimer = true;
                if (Timer > MakingTime)
                {
                    for (int i = 0; i < Resource1Required; i++)
                    {
                        DestroyResource(ref TakingStoreSpace[Resource1 - 1]);
                        Resource1--;
                    }
                    Resource2++;
                    Timer = 0;
                }

            }
            else IsOnTimer = false;
        }
        else IsOnTimer = false;
    }
    public virtual void GiveResources()
    {
        for (int i = 0; i < GivingStoreSpace.Length; i++)
        {
            if (Resource2 > 0)
                if (GivingStoreSpace[i] == null)
                {
                    GivingStoreSpace[i] = Instantiate(ResourcePrefabs[1], GivingStorePlace[i].position, transform.rotation);
                    Resource2--;
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
        resource.HasTaken = false;
        inventoryItem = null;
        resource.Dirrection = storePlace;
    }

    public virtual void DestroyResource(ref GameObject storeWithResource)
    {
        storeWithResource.GetComponent<Resource>().Dirrection = transform;
        Destroy(storeWithResource.gameObject, MakingTime);
        storeWithResource = null;
    }

    public virtual void GoTimer()
    {
        if (IsOnTimer)
        {
            Timer++;
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
        CheckProcess(Timer, GivingStoreSpace);
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerInventory>() != null)
        {
            playerInventory = other.gameObject;
            TakeResourcesToStore(playerInventory, TakingResource1Index, TakingResource2Index, TakingResource3Index);
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
