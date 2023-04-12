using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory1 : ParentFactory, FactoryInterface
{
    public override void MakeResources()
    {
        if (GivingStoreSpace[GivingStoreSpace.Length - 1] == null)
        {
            if (Resource1 >= Resource1Required)
            {
                IsOnTimer = true;
                if (Timer > MakingTime)
                {
                    Resource2++;
                    Timer = 0;
                }

            }
            else IsOnTimer = false;
        }
        else IsOnTimer = false;
    }
}
