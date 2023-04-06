using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using JetBrains.Annotations;

public class FactoryUI : MonoBehaviour
{
    [SerializeField] private ParentFactory myFactory;
    [SerializeField] private TextMeshProUGUI[] texts;


    private void Start()
    {
        myFactory = GetComponentInParent<ParentFactory>();

        texts = GetComponentsInChildren<TextMeshProUGUI>();
    }
}
