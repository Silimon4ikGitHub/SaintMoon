using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory2 : ParentFactory, FactoryInterface
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
                                takingStoreSpace[i] = otherInventory.invenoryItem[currentArrayIndex];
                                otherInventory.invenoryItem[currentArrayIndex].GetComponent<Resource>().hasTaken = false;
                                otherInventory.invenoryItem[currentArrayIndex] = null;
                                takingStoreSpace[i].transform.position = Vector3.MoveTowards(takingStoreSpace[i].transform.position, takingStorePlace[i].position, resourceSpeed);
                                resource2++;
                                otherInventory.currentCount--;
                            }
            }
        }
    }
    public override void MakeResources()
    {
        if (GivingStoreSpace[GivingStoreSpace.Length - 1] == null)
        {
            if (resource2 >= resource2Required)
            {
                isOnTimer = true;
                    if (timer > makingTime)
                    {
                        for (int i = 0; i < resource2Required; i++)
                        {
                            takingStoreSpace[resource2 - 1].transform.position = Vector3.MoveTowards(takingStoreSpace[resource2 - 1].transform.position, transform.position, resourceSpeed);
                            Destroy(takingStoreSpace[resource2 - 1].gameObject, makingTime);
                            takingStoreSpace[resource2 - 1] = null;
                            resource2--;
                        }
                        resource3++;
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
         if (resource3 > 0)
            if (GivingStoreSpace[i] == null)
            {
                GivingStoreSpace[i] = Instantiate(resourcePrefabs[2], GivingStorePlace[i].position, transform.rotation);
                resource3--;
            }
       }
    }
}
