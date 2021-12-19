using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LightRack : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    Text textBox;
    [SerializeField]
    GameObject paw;
    [SerializeField]
    GameObject stickSmall;
    [SerializeField]
    GameObject stickMain;
    [SerializeField]
    GameObject holder;
    [SerializeField]
    GameObject baseb;
    [SerializeField]
    GameObject bolt1;
    [SerializeField]
    GameObject bolt2;
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
        textBox.text = "Лабораторный штатив";
        initialColor = paw.GetComponent<Renderer>().material.color;

        paw.GetComponent<Renderer>().material.color = Color.yellow;
        stickSmall.GetComponent<Renderer>().material.color = Color.yellow;
        stickMain.GetComponent<Renderer>().material.color = Color.yellow;
        holder.GetComponent<Renderer>().material.color = Color.yellow;
        baseb.GetComponent<Renderer>().material.color = Color.yellow;
        bolt1.GetComponent<Renderer>().material.color = Color.yellow;
        bolt2.GetComponent<Renderer>().material.color = Color.yellow;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        textBox.text = previousText;
        paw.GetComponent<Renderer>().material.color = initialColor;
        stickSmall.GetComponent<Renderer>().material.color = initialColor;
        stickMain.GetComponent<Renderer>().material.color = initialColor;
        holder.GetComponent<Renderer>().material.color = initialColor;
        baseb.GetComponent<Renderer>().material.color = initialColor;
        bolt1.GetComponent<Renderer>().material.color = initialColor;
        bolt2.GetComponent<Renderer>().material.color = initialColor;
    }
}
