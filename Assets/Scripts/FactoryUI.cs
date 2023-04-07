using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using JetBrains.Annotations;

public class FactoryUI : MonoBehaviour
{
    private ParentFactory myFactory;
    private TextMeshProUGUI[] texts;


    private void Start()
    {
        myFactory = GetComponentInParent<ParentFactory>();

        texts = GetComponentsInChildren<TextMeshProUGUI>();
    }

    private void FixedUpdate()
    {
        if (myFactory.ProcessData.IsNoResources)
        {
            texts[0].enabled = true;
        }
        else texts[0].enabled = false;

        if (myFactory.ProcessData.IsWorking)
        {
            texts[1].enabled = true;
        }
        else texts[1].enabled = false;

        if (myFactory.ProcessData.IsStoreFull)
        {
            texts[2].enabled = true;
        }
        else texts[2].enabled = false;
    }
}
