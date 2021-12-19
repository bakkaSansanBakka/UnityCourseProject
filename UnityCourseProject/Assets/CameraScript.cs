using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField]
    Transform targetPos;
    int sensivity = 5;
    [SerializeField]
    float scrollSpeed = 20f;
    [SerializeField]
    int mindistance = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetMouseButton(1))
        {
            transform.RotateAround(targetPos.position, Vector3.up, Input.GetAxis("Mouse X") * sensivity);
            transform.Rotate(Vector3.left, Input.GetAxis("Mouse Y") * sensivity);
        }

    }

    void FixedUpdate()
    {
        float x = Input.GetAxis("Horizontal"); // клавиши A, D
        float y = Input.GetAxis("Vertical"); // клавиши W, S
        if (x != 0 || y != 0)
        {

            Vector3 newpos = transform.position + (transform.TransformDirection(new Vector3(x, 0, 0)) + Vector3.up * y) / sensivity;
            if (ControlDistance(Vector3.Distance(newpos, targetPos.position))) transform.position = newpos;
        }

        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            Vector3 newpos = transform.position + transform.TransformDirection(Vector3.forward * Input.GetAxis("Mouse ScrollWheel") * scrollSpeed);
            if (ControlDistance(Vector3.Distance(newpos, targetPos.position))) transform.position = newpos;
        }

    }

    bool ControlDistance(float distance)
    {
        if (distance > mindistance && distance < 50) return true;
        return false;
    }
}
