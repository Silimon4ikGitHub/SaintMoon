
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public int CurrentCount;
    public GameObject[] InvenoryItem;
    public GameObject[] ItemPoint;
    public Transform FreeSpace;
    void Update()
    {

        if (CurrentCount < InvenoryItem.Length)
            FreeSpace = ItemPoint[CurrentCount].transform;
    }

    private void OnTriggerEnter(Collider collision)
    {
        
        if (collision.gameObject.GetComponent<Resource>() != null)
        {
            Resource currentRecource = collision.gameObject.GetComponent<Resource>();
            currentRecource.MyCount = CurrentCount;

            if (CurrentCount < InvenoryItem.Length)
            {
                currentRecource.HasTaken = true;
                currentRecource.Dirrection = ItemPoint[CurrentCount].transform;
                InvenoryItem[CurrentCount] = collision.gameObject;
                CurrentCount++;
            }
        }
    }
}
