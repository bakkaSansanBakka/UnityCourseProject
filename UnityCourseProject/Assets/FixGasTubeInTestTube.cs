using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixGasTubeInTestTube : MonoBehaviour
{
    [SerializeField]
    Transform targetPosition;
    [HideInInspector]
    public bool gasTubeIsFixed = false;
    [HideInInspector]
    public event Action propertyChanged;
    float speed = 2f;

    private bool isMoving = false;

    Vector3 initialPosition;
    Quaternion initialRotation;

    // Start is called before the first frame update
    void Start()
    {
        gasTubeIsFixed = false;
        initialPosition = transform.position;
        initialRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnMouseDown()
    {
        if (!gasTubeIsFixed)
        {
            StartCoroutine(MoveGasTubeTo(targetPosition.position, targetPosition.rotation));
            gasTubeIsFixed = true;
            propertyChanged?.Invoke();
        }
        else if (gasTubeIsFixed)
        {
            StartCoroutine(MoveGasTubeTo(initialPosition, initialRotation));
            gasTubeIsFixed = false;
            propertyChanged?.Invoke();
        }
    }

    IEnumerator MoveGasTubeTo(Vector3 position, Quaternion rotation)
    {
        if (isMoving)
            yield break;

        isMoving = true;

        while (Vector3.Distance(transform.position, position) > 0.1f)
        {
            transform.position = Vector3.Lerp(transform.position, position, speed * Time.deltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, speed * Time.deltaTime);
            yield return null;
        }

        isMoving = false;
    }

    public void SetInitialPosition()
    {
        if (gasTubeIsFixed)
        {
            StartCoroutine(MoveGasTubeTo(initialPosition, initialRotation));
            gasTubeIsFixed = false;
            propertyChanged?.Invoke();
        }
    }
}
