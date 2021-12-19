using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddLimeWaterInTube : MonoBehaviour
{
    [SerializeField]
    Transform targetPosition;

    [SerializeField]
    Transform limewaterInTubePosition;

    [SerializeField]
    GameObject limewaterParticles;
    [SerializeField]
    GameObject limewaterInTube;

    [HideInInspector]
    public bool limeWaterIsInTube;
    [HideInInspector]
    public event Action propertyChanged;

    [HideInInspector]
    public GameObject newlimewaterObject;

    // Start is called before the first frame update
    void Start()
    {
        limeWaterIsInTube = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnMouseDown()
    {
        if (!limeWaterIsInTube)
        {
            Instantiate(limewaterParticles, targetPosition.position, targetPosition.rotation);
            newlimewaterObject = Instantiate(limewaterInTube, limewaterInTubePosition.position, limewaterInTubePosition.rotation);

            limeWaterIsInTube = true;
            propertyChanged?.Invoke();
        }
    }

    public void DestroyLimewaterInTube()
    {
        if (newlimewaterObject != null)
        {
            Destroy(newlimewaterObject);
        }
        limeWaterIsInTube = false;
    }
}
