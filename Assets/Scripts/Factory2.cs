
using UnityEngine;

public class Factory2 : ParentFactory, FactoryInterface
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
        }
    }
    public override void MakeResources()
    {
        if (GivingStoreSpace[GivingStoreSpace.Length - 1] == null)
        {
            if (Resource2 >= Resource2Required)
            {
                IsOnTimer = true;
                    if (Timer > MakingTime)
                    {
                        for (int i = 0; i < Resource2Required; i++)
                        {
                            DestroyResource(ref TakingStoreSpace[Resource2 - 1]);
                            Resource2--;
                        }
                        Resource3++;
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
         if (Resource3 > 0)
            if (GivingStoreSpace[i] == null)
            {
                GivingStoreSpace[i] = Instantiate(ResourcePrefabs[2], GivingStorePlace[i].position, transform.rotation);
                Resource3--;
            }
       }
    }
}
