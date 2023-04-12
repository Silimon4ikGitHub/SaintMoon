using System;
using UnityEngine;

public class FactoryController : MonoBehaviour
{
    [SerializeField] private GameObject factory;

    private FactoryInterface workingFactory;
    void Start()
    {
        workingFactory = factory.GetComponent<FactoryInterface>();

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
