using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullTubeWithMalachit : MonoBehaviour
{
    [SerializeField]
    Transform targetPositionHalf;
    [SerializeField]
    Transform targetPositionFull;
    [SerializeField]
    Transform powderPosition;
    [SerializeField]
    Transform malachitPosition;

    List<Transform> listOfPositions;

    Vector3 initialPosition;
    Quaternion initialRotation;

    [HideInInspector]
    public bool tubeIsHalfWay;
    [HideInInspector]
    public bool tubeIsFullWay;
    [HideInInspector]
    public bool malachiteIsInTube;

    [SerializeField]
    GameObject mainTube;

    [SerializeField]
    GameObject malachitPowder;
    [SerializeField]
    GameObject malachit;
    [HideInInspector]
    public GameObject newMalachitObject;

    [HideInInspector]
    public event Action propertyChanged;
    float speed = 2f;

    private bool isMoving = false;

    // Start is called before the first frame update
    void Start()
    {
        tubeIsHalfWay = false;
        tubeIsFullWay = false;
        malachiteIsInTube = false;
        initialPosition = transform.position;

        listOfPositions = new List<Transform>() { targetPositionHalf, targetPositionFull };
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnMouseDown()
    {
        if (!tubeIsHalfWay && !tubeIsFullWay)
        {
            StartCoroutine(MoveAndAddMalachitToTubeInRack(listOfPositions));
            malachiteIsInTube = true;
            propertyChanged?.Invoke();
        }
    }

    public void DestroyMalachitInTube()
    {
        if (newMalachitObject != null)
        {
            Destroy(newMalachitObject);
        }
        malachiteIsInTube = false;
    }

    IEnumerator MoveAndAddMalachitToTubeInRack(List<Transform> ListOfPositions)
    {
        if (isMoving)
            yield break;

        isMoving = true;

        foreach (var position in ListOfPositions)
        {
            while (Vector3.Distance(transform.position, position.position) > 0.1f)
            {
                transform.position = Vector3.Lerp(transform.position, position.position, speed * Time.deltaTime);
                transform.rotation = Quaternion.Lerp(transform.rotation, position.rotation, speed * Time.deltaTime);
                yield return null;
            }
        }

        Instantiate(malachitPowder, powderPosition.position, powderPosition.rotation);

        newMalachitObject = Instantiate(malachit, malachitPosition.position, malachitPosition.rotation);
        newMalachitObject.transform.parent = mainTube.transform;

        while (Vector3.Distance(transform.position, targetPositionHalf.position) > 0.1f)
        {
            transform.position = Vector3.Lerp(transform.position, targetPositionHalf.position, speed * Time.deltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetPositionHalf.rotation, speed * Time.deltaTime);
            yield return null;
        }

        while (Vector3.Distance(transform.position, initialPosition) > 0.1f)
        {
            transform.position = Vector3.Lerp(transform.position, initialPosition, speed * Time.deltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, initialRotation, speed * Time.deltaTime);
            yield return null;
        }

        isMoving = false;
    }
}
