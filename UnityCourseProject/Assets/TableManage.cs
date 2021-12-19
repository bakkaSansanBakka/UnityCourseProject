using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TableManage : MonoBehaviour
{
    [SerializeField]
    Text temperatureTextBox;

    [HideInInspector]
    public bool isWrittenToTable;

    [HideInInspector]
    public event Action propertyChanged;

    [SerializeField]
    Text T1;
    [SerializeField]
    Text T2;
    [SerializeField]
    Text T3;
    [SerializeField]
    Text T4;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
        isWrittenToTable = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenTable()
    {
        gameObject.SetActive(true);
    }

    public void CloseTable()
    {
        gameObject.SetActive(false);
    }

    public void WriteToTable()
    {
        if (T4.text != "-")
            Clean();

        if (T1.text == "-")
            T1.text = temperatureTextBox.text;
        else if (T2.text == "-")
            T2.text = temperatureTextBox.text;
        else if (T3.text == "-")
            T3.text = temperatureTextBox.text;
        else if (T4.text == "-")
            T4.text = temperatureTextBox.text;

        isWrittenToTable = true;
        propertyChanged?.Invoke();
    }

    public void Clean()
    {
        T1.text = "-";
        T2.text = "-";
        T3.text = "-";
        T4.text = "-";
    }
}
