using System;
using UnityEngine;

public class Factory3Controller : MonoBehaviour
{
    [SerializeField] private GameObject factory3;

    private FactoryInterface workingFactory;
    void Start()
    {
        workingFactory = factory3.GetComponent<FactoryInterface>();

        if (workingFactory == null)
        {
            throw new NullReferenceException("factory dont have icontrollable interface");

        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        workingFactory.MakeOneStack();
        workingFactory.GiveOneStuck();
        workingFactory.CheckMyStore();
        workingFactory.GoFactoryTimer();
        workingFactory.ChangeUI();
    }
}
