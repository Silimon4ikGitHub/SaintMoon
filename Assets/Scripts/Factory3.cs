
using UnityEngine;

public class Factory3 : ParentFactory, FactoryInterface
{
    public override void TakeResourcesToStore(GameObject inventory, int resource1Index, int resource2Index, int takingResource3Index)
    {
        {
            PlayerInventory otherInventory = inventory.GetComponent<PlayerInventory>();

            for (int i = 0; i < otherInventory.InvenoryItem.Length; i++)
            {
                int currentArrayIndex = otherInventory.CurrentCount - 1;

                if (otherInventory.CurrentCount >= 1)
                    if (otherInventory.InvenoryItem[currentArrayIndex] != null)
                        if (otherInventory.InvenoryItem[currentArrayIndex].GetComponent<Resource>().MyIndex == resource2Index)
                            if (TakingStoreSpace[i] == null)
                            {
                                ChangeStoreAndArray(ref TakingStoreSpace[i], ref otherInventory.InvenoryItem[currentArrayIndex], ref TakingStorePlace[i]);
                                Resource2++;
                                otherInventory.CurrentCount--;
                            }
            }
            for (int i = 0; i < otherInventory.InvenoryItem.Length; i++)
            {
                int currentArrayIndex = otherInventory.CurrentCount - 1;

                if (otherInventory.CurrentCount >= 1)
                    if (otherInventory.InvenoryItem[currentArrayIndex] != null)
                        if (otherInventory.InvenoryItem[currentArrayIndex].GetComponent<Resource>().MyIndex == takingResource3Index)
                            if (TakingStoreSpace[i] == null)
                            {
                                ChangeStoreAndArray(ref TakingStoreSpace[i], ref otherInventory.InvenoryItem[currentArrayIndex], ref TakingStorePlace[i]);
                                Resource3++;
                                otherInventory.CurrentCount--;
                            }
            }
        }
    }
    public override void MakeResources()
    {
        bool isTakeOne = true;
        bool isTakeTwo = true;

        if (GivingStoreSpace[GivingStoreSpace.Length - 1] == null)
        {
            if (Resource2 >= Resource2Required && Resource3 >= Resource3Required)
            {
                IsOnTimer = true;
                if (Timer > MakingTime)
                {
                    for (int i = 0; i < TakingStoreSpace.Length; i++)
                    {
                        if(isTakeOne)
                            if (TakingStoreSpace[i] != null && TakingStoreSpace[i].GetComponent<Resource>().MyIndex == TakingResource2Index)
                            {
                                DestroyResource(ref TakingStoreSpace[i]);
                                isTakeOne = false;
                            }
                        
                    }
                    for (int i = 0; i < TakingStoreSpace.Length; i++)
                    {
                        if(isTakeTwo)
                            if (TakingStoreSpace[i] != null && TakingStoreSpace[i].GetComponent<Resource>().MyIndex == TakingResource3Index)
                            {
                                DestroyResource(ref TakingStoreSpace[i]);
                                isTakeTwo = false;
                            }
                    }
                    Resource2 -= Resource2Required;
                    Resource3 -= Resource3Required;
                    Resource4++;
                    Timer = 0;
                }
            }
            else IsOnTimer = false;
        }
        else IsOnTimer = false;
    }
    public override void GiveResources()
    {
        for (int i = 0; i < GivingStoreSpace.Length; i++)
        {
            if (Resource4 > 0)
                if (GivingStoreSpace[i] == null)
                {
                    GivingStoreSpace[i] = Instantiate(ResourcePrefabs[3], GivingStorePlace[i].position, transform.rotation);
                    Resource4--;
                }

        }
    }
}
