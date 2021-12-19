using System;
using System.Collections;
using UnityEngine;

public class FixTubeInRack : MonoBehaviour
{
    [SerializeField]
    Transform targetPosition;
    [SerializeField]
    Transform demonstrateTubePosition;
    [SerializeField]
    Transform acidLiquidPosition;

    [SerializeField]
    GameObject acidLiquid;

    [SerializeField]
    FireSpiritlamp fireSpiritlampScript;
    [SerializeField]
    PullTubeWithMalachit pullTubeWithMalachitScript;

    [HideInInspector]
    public bool tubeIsFixed, malachitIsBlue;
    [HideInInspector]
    public event Action propertyChanged;
    float speed = 2f;

    private bool isMoving = false;

    Vector3 initialPosition;
    Quaternion initialRotation;

    [SerializeField]
    Material malachitBlue;

    // Start is called before the first frame update
    void Start()
    {
        tubeIsFixed = false;
        malachitIsBlue = false;
        initialPosition = transform.position;
        initialRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetInitialPosition()
    {
        if (tubeIsFixed)
        {
            tubeIsFixed = false;
            propertyChanged?.Invoke();
        }

        if (malachitIsBlue)
        {
            malachitIsBlue = false;
            propertyChanged?.Invoke();
        }

        if (transform.position != initialPosition || transform.rotation != initialRotation)
        {
            StartCoroutine(MoveTubeTo(initialPosition, initialRotation));
        }
    }

    public void OnMouseDown()
    {
        if (!tubeIsFixed)
        {
            StartCoroutine(MoveTubeTo(targetPosition.position, targetPosition.rotation));
            tubeIsFixed = true;
            propertyChanged?.Invoke();
        }
        else if (tubeIsFixed && !malachitIsBlue && fireSpiritlampScript.malachitIsDark)
        {
            StartCoroutine(DemonstrateTubeAndMakeMalachitBlue());
            tubeIsFixed = false;
            fireSpiritlampScript.malachitIsDark = false;
            malachitIsBlue = true;
            propertyChanged?.Invoke();
        }
    }

    IEnumerator MoveTubeTo(Vector3 position, Quaternion rotation)
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

    IEnumerator DemonstrateTubeAndMakeMalachitBlue()
    {
        if (isMoving)
            yield break;

        isMoving = true;

        while (Vector3.Distance(transform.position, demonstrateTubePosition.position) > 0.1f)
        {
            transform.position = Vector3.Lerp(transform.position, demonstrateTubePosition.position, speed * Time.deltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, demonstrateTubePosition.rotation, speed * Time.deltaTime);
            yield return null;
        }

        Instantiate(acidLiquid, acidLiquidPosition.position, acidLiquidPosition.rotation);

        pullTubeWithMalachitScript.newMalachitObject.GetComponent<MeshRenderer>().material = malachitBlue;

        isMoving = false;
    }
}
