using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MoveToObject : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    Camera Camera;

    [SerializeField]
    GameObject targetObject;
    float speed = 0.01f;
    bool move;
    float offset = 0;

    public void OnPointerClick(PointerEventData eventData)
    {
        offset = 0;
        move = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (move)
        {
            if (offset <= 1)
            {
                offset += speed;
                Camera.transform.position = Vector3.Lerp(Camera.transform.position, targetObject.transform.position, offset);
                Camera.transform.rotation = Quaternion.Lerp(Camera.transform.rotation, targetObject.transform.rotation, offset);
            }
            else
            {
                move = false;
            }
        }
    }


}
