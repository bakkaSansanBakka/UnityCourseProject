using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FireSpiritlamp : MonoBehaviour
{
    [SerializeField]
    Transform firePosition, mainUnderTubePosition, additionalUnderTubePosition;

    Vector3 initialPosition;
    Quaternion initialRotation;

    [HideInInspector]
    public bool fireIsLit, malachitIsDark;

    [SerializeField]
    GameObject fire;
    GameObject newFireObject;

    [HideInInspector]
    public event Action propertyChanged;

    float speed = 2f;
    private bool isMoving = false;
    private bool isTemperatureRaising = false;

    [SerializeField]
    PullTubeWithMalachit pullTubeWithMalachitScript;
    [SerializeField]
    AddLimeWaterInTube addLimeWaterInTubeScript;

    [SerializeField]
    Material darkMalachit, greenMalachit, turbidLiquid;

    [SerializeField]
    Text textBox;

    // Start is called before the first frame update
    void Start()
    {
        fireIsLit = false;
        malachitIsDark = false;
        isTemperatureRaising = false;
        initialPosition = transform.position;
        initialRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PutOutFire()
    {
        if (newFireObject != null)
        {
            Destroy(newFireObject);
        }

        if (fireIsLit)
        {
            fireIsLit = false;
            propertyChanged?.Invoke();
        }

        if (malachitIsDark)
        {
            malachitIsDark = false;
            propertyChanged?.Invoke();
        }

        StartCoroutine(MoveSpiritlampToInitial());
    }

    public void PutOutFireInProgress()
    {
        if (fireIsLit && newFireObject != null)
        {
            Destroy(newFireObject);
            StartCoroutine(MoveSpiritlampToInitial());
            fireIsLit = false;
            propertyChanged?.Invoke();
        }
    }

    public void OnMouseDown()
    {
        if (!fireIsLit && !malachitIsDark)
        {
            newFireObject = Instantiate(fire, firePosition.position, firePosition.rotation);
            newFireObject.transform.parent = transform;

            fireIsLit = true;
            propertyChanged?.Invoke();
        }
        else if (fireIsLit && !malachitIsDark)
        {
            StartCoroutine(MoveSpiritlampAndMakeMalachitDarker());
            StartCoroutine(RaiseTemperature());
        }
        else if (fireIsLit && malachitIsDark)
        {
            PutOutFireInProgress();
        }
    }

    IEnumerator MoveSpiritlampAndMakeMalachitDarker()
    {
        if (isMoving)
            yield break;

        isMoving = true;

        // move to under tube
        while (Vector3.Distance(transform.position, mainUnderTubePosition.position) > 0.1f)
        {
            transform.position = Vector3.Lerp(transform.position, mainUnderTubePosition.position, speed * Time.deltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, mainUnderTubePosition.rotation, speed * Time.deltaTime);
            yield return null;
        }

        //StartCoroutine(RaiseTemperature());

        // move 2 times under tube and stop
        while (Vector3.Distance(transform.position, additionalUnderTubePosition.position) > 0.1f)
        {
            transform.position = Vector3.Lerp(transform.position, additionalUnderTubePosition.position, speed * Time.deltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, additionalUnderTubePosition.rotation, speed * Time.deltaTime);
            yield return null;
        }

        addLimeWaterInTubeScript.newlimewaterObject.GetComponent<MeshRenderer>().material = turbidLiquid;

        while (Vector3.Distance(transform.position, mainUnderTubePosition.position) > 0.1f)
        {
            transform.position = Vector3.Lerp(transform.position, mainUnderTubePosition.position, speed * Time.deltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, mainUnderTubePosition.rotation, speed * Time.deltaTime);
            yield return null;
        }

        pullTubeWithMalachitScript.newMalachitObject.GetComponent<MeshRenderer>().material = darkMalachit;
        malachitIsDark = true;
        propertyChanged?.Invoke();

        isMoving = false;
    }

    IEnumerator MoveSpiritlampToInitial()
    {
        if (isMoving)
            yield break;

        isMoving = true;

        while (Vector3.Distance(transform.position, initialPosition) > 0.1f)
        {
            transform.position = Vector3.Lerp(transform.position, initialPosition, speed * Time.deltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, initialRotation, speed * Time.deltaTime);
            yield return null;
        }

        isMoving = false;
    }

    IEnumerator RaiseTemperature()
    {
        if (isTemperatureRaising)
            yield break;

        isTemperatureRaising = true;
        double temperature = double.Parse(textBox.text);

        while (double.Parse(textBox.text) < 60f)
        {
            temperature++;
            textBox.text = temperature.ToString();
            yield return new WaitForSeconds(0.3f);
        }

        isTemperatureRaising = false;
    }
}
