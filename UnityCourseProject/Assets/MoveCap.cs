using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCap : MonoBehaviour
{
    [SerializeField]
    Transform targetPosition;
    [HideInInspector]
    public bool capIsPutOff;
    [HideInInspector]
    public event Action propertyChanged;
    float speed = 2f;

    private bool isMoving = false;

    Vector3 initialPosition;
    Quaternion initialRotation;

    // Start is called before the first frame update
    void Start()
    {
        capIsPutOff = false;
        initialPosition = transform.position;
        initialRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetInitialPosition()
    {
        if (capIsPutOff)
        {
            StartCoroutine(MoveTo(initialPosition, initialRotation));
            capIsPutOff = false;
            propertyChanged?.Invoke();
        }
    }

    public void OnMouseDown()
    {
        if (!capIsPutOff)
        {
            StartCoroutine(MoveTo(targetPosition.position, targetPosition.rotation));
            capIsPutOff = true;
            propertyChanged?.Invoke();
        }
        else if (capIsPutOff)
        {
            SetInitialPosition();
        }
    }

    IEnumerator MoveTo(Vector3 position, Quaternion rotation)
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
}
