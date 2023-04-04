using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory3 : ParentFactory, FactoryInterface
{
    public override void TakeResourcesToStore(GameObject inventory, int resource1Index, int resource2Index, int takingResource3Index)
    {
        {
            PlayerInventory otherInventory = inventory.GetComponent<PlayerInventory>();

            for (int i = 0; i < otherInventory.invenoryItem.Length; i++)
            {
                int currentArrayIndex = otherInventory.currentCount - 1;

                if (otherInventory.currentCount >= 1)
                    if (otherInventory.invenoryItem[currentArrayIndex] != null)
                        if (otherInventory.invenoryItem[currentArrayIndex].GetComponent<Resource>().myIndex == resource2Index)
                            if (takingStoreSpace[i] == null)
                            {
                                ChangeStoreAndArray(ref takingStoreSpace[i], ref otherInventory.invenoryItem[currentArrayIndex], ref takingStorePlace[i]);
                                resource2++;
                                otherInventory.currentCount--;
                            }
            }
            for (int i = 0; i < otherInventory.invenoryItem.Length; i++)
            {
                int currentArrayIndex = otherInventory.currentCount - 1;

                if (otherInventory.currentCount >= 1)
                    if (otherInventory.invenoryItem[currentArrayIndex] != null)
                        if (otherInventory.invenoryItem[currentArrayIndex].GetComponent<Resource>().myIndex == takingResource3Index)
                            if (takingStoreSpace[i] == null)
                            {
                                ChangeStoreAndArray(ref takingStoreSpace[i], ref otherInventory.invenoryItem[currentArrayIndex], ref takingStorePlace[i]);
                                resource3++;
                                otherInventory.currentCount--;
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
            if (resource2 >= resource2Required && resource3 >= resource3Required)
            {
                isOnTimer = true;
                if (timer > makingTime)
                {
                    for (int i = 0; i < takingStoreSpace.Length; i++)
                    {
                        if(isTakeOne)
                            if (takingStoreSpace[i] != null && takingStoreSpace[i].GetComponent<Resource>().myIndex == takingResource2Index)
                            {
                                DestroyResource(ref takingStoreSpace[i]);
                                isTakeOne = false;
                            }
                        
                    }
                    for (int i = 0; i < takingStoreSpace.Length; i++)
                    {
                        if(isTakeTwo)
                            if (takingStoreSpace[i] != null && takingStoreSpace[i].GetComponent<Resource>().myIndex == takingResource3Index)
                            {
                                DestroyResource(ref takingStoreSpace[i]);
                                isTakeTwo = false;
                            }
                    }
                    resource2 -= resource2Required;
                    resource3 -= resource3Required;
                    resource4++;
                    timer = 0;
                }
            }
            else isOnTimer = false;
        }
        else isOnTimer = false;
    }
    public override void GiveResources()
    {
        for (int i = 0; i < GivingStoreSpace.Length; i++)
        {
            if (resource4 > 0)
                if (GivingStoreSpace[i] == null)
                {
                    GivingStoreSpace[i] = Instantiate(resourcePrefabs[3], GivingStorePlace[i].position, transform.rotation);
                    resource4--;
                }

        }
    }
}
