using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ParentFactory : MonoBehaviour
{
    [SerializeField] private float timer;
    [SerializeField] private float makingTime;
    [SerializeField] private bool ready;
    public float resource1;
    public float resource2;
    public float resource3;
    public Collider takeTrigger;

    private void Start()
    {
        takeTrigger = GetComponent<Collider>();
    }

   
    public virtual void TakeResources()
    {
        
    }
    public virtual void MakeResources()
    {
        
    }

    public virtual void GoTimer()
    {
        timer++;

        if (timer > makingTime)
            ready = true;
        else
            ready = false;
        Debug.Log(timer);
    }

    public void TakeOneStack()
    {
        TakeResources();
    }
    public void MakeOneStack()
    {
        MakeResources();
    }
    public void GoFactoryTimer()
    {
        GoTimer();
    }


}
