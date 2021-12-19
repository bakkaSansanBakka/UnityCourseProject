using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LightGasTube : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    Text textBox;
    [SerializeField] 
    GameObject glassTube;
    [SerializeField] 
    GameObject tube;
    [SerializeField]
    GameObject tubeCap;
    Color initialColorOfGlassTube;
    Color initialColorOfTube;
    Color initialColorOfTubeCap;
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
        textBox.text = "Газоотводная трубка";
        initialColorOfGlassTube = glassTube.GetComponent<Renderer>().material.color;
        initialColorOfTube = tube.GetComponent<Renderer>().material.color;
        initialColorOfTubeCap = tubeCap.GetComponent<Renderer>().material.color;

        glassTube.GetComponent<Renderer>().material.color = Color.yellow;
        tube.GetComponent<Renderer>().material.color = Color.yellow;
        tubeCap.GetComponent<Renderer>().material.color = Color.yellow;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        textBox.text = previousText;
        glassTube.GetComponent<Renderer>().material.color = initialColorOfGlassTube;
        tube.GetComponent<Renderer>().material.color = initialColorOfTube;
        tubeCap.GetComponent<Renderer>().material.color = initialColorOfTubeCap;
    }
}
