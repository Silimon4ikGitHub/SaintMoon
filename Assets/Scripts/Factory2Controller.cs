using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory2Controller : MonoBehaviour
{
    public GameObject factory2;

    private FactoryInterface workingFactory;
    void Start()
    {
        workingFactory = factory2.GetComponent<FactoryInterface>();

        if (workingFactory == null)
        {
            throw new NullReferenceException("factory dont have icontrollable interface");

        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        workingFactory.TakeOneStack();
        workingFactory.MakeOneStack();
        workingFactory.GiveOneStuck();
        workingFactory.CheckMyStore();
        workingFactory.GoFactoryTimer();
    }
}
