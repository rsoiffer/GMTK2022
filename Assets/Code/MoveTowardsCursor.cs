using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTowardsCursor : MonoBehaviour
{
    public float zPositionDepth = -1;
    [Space]
    public float smoothTime = 0.1f;
    public float maxSpeed = 50f;
    private Vector3 currVelocity;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            transform.position = Vector3.SmoothDamp(transform.position, GetCursorWorldPos(), ref currVelocity, smoothTime, maxSpeed, Time.deltaTime);
        }        
    }

    Vector3 GetCursorWorldPos()
    {
        var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pos.z = 0;
        return pos;
    }
}
