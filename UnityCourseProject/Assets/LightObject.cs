using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LightObject : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    Text textBox;
    [SerializeField]
    GameObject targetObject;
    Color initialColor;
    string previousText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        previousText = textBox.text;
        switch (this.name)
        {
            case "SpiritLampViewButton": textBox.text = "Спиртовка"; break;
            case "TestTubeViewButton": textBox.text = "Пробирка"; break;
            case "RackViewButton": textBox.text = "Лабораторный штатив"; break;
            case "GasTubeViewButton": textBox.text = "Газоотводная трубка"; break;
            case "TestTubeRackViewButton": textBox.text = "Штатив для пробирок"; break;
            default: textBox.text = ""; break;
        }

        initialColor = targetObject.GetComponent<Renderer>().material.color;

        targetObject.GetComponent<Renderer>().material.color = Color.yellow;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        textBox.text = previousText;
        targetObject.GetComponent<Renderer>().material.color = initialColor;
    }
}
