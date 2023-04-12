
using UnityEngine;
using TMPro;


public class FactoryUI : MonoBehaviour
{
    private ParentFactory _myFactory;
    private TextMeshProUGUI[] _texts;


    private void Start()
    {
        _myFactory = GetComponentInParent<ParentFactory>();

        _texts = GetComponentsInChildren<TextMeshProUGUI>();
    }

    private void FixedUpdate()
    {
        if (_myFactory.ProcessData.IsNoResources)
        {
            _texts[0].enabled = true;
        }
        else _texts[0].enabled = false;

        if (_myFactory.ProcessData.IsWorking)
        {
            _texts[1].enabled = true;
        }
        else _texts[1].enabled = false;

        if (_myFactory.ProcessData.IsStoreFull)
        {
            _texts[2].enabled = true;
        }
        else _texts[2].enabled = false;
    }
}
