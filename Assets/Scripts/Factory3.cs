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
            for (int i = 0; i < otherInventory.invenoryItem.Length; i++)
            {
                int currentArrayIndex = otherInventory.currentCount - 1;

                if (otherInventory.currentCount >= 1)
                    if (otherInventory.invenoryItem[currentArrayIndex] != null)
                        if (otherInventory.invenoryItem[currentArrayIndex].GetComponent<Resource>().myIndex == takingResource3Index)
                            if (takingStoreSpace[i] == null)
                            {
                                takingStoreSpace[i] = otherInventory.invenoryItem[currentArrayIndex];
                                otherInventory.invenoryItem[currentArrayIndex].GetComponent<Resource>().hasTaken = false;
                                otherInventory.invenoryItem[currentArrayIndex] = null;
                                takingStoreSpace[i].transform.position = Vector3.MoveTowards(takingStoreSpace[i].transform.position, takingStorePlace[i].position, resourceSpeed);
                                resource3++;
                                otherInventory.currentCount--;
                            }
            }
        }
    }
    public override void MakeResources()
    {
        if (resource1 >= resource1Required && resource3 >= resource3Required)
        {
            for (int i = 0; i < takingStoreSpace.Length; i++)
            {
                if (takingStoreSpace[i] != null && takingStoreSpace[i].GetComponent<Resource>().myIndex == takingResource1Index)
                {
                    takingStoreSpace[i].transform.position = Vector3.MoveTowards(takingStoreSpace[i].transform.position, transform.position, resourceSpeed);
                    Destroy(takingStoreSpace[i].gameObject, makingTime);
                    takingStoreSpace[i] = null;
                    resource1--;
                }
                else if (takingStoreSpace[i] != null && takingStoreSpace[i].GetComponent<Resource>().myIndex == takingResource3Index)
                {
                    takingStoreSpace[i].transform.position = Vector3.MoveTowards(takingStoreSpace[i].transform.position, transform.position, resourceSpeed);
                    Destroy(takingStoreSpace[i].gameObject, makingTime);
                    takingStoreSpace[i] = null;
                    resource3--;
                }
            }
            resource4++;
        }
    }
    public override void GiveResources()
    {
        for (int i = 0; i < GivingStoreSpace.Length; i++)
        {
            if (resource4 > 0)
                if (GivingStoreSpace[i] == null)
                {
                    GivingStoreSpace[i] = Instantiate(resourcePrefabs[4], GivingStorePlace[i].position, transform.rotation);
                    resource4--;
                    Debug.Log("HereISWorkig");
                }

        }
    }
}
