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
        if (Input.GetMouseButton(1))
        {
            var targetVec = GetCursorWorldPos() - transform.position;
            transform.rotation = Quaternion.LookRotation(Vector3.forward, targetVec) * Quaternion.Euler(0, 0, 90);
        }
    }

    Vector3 GetCursorWorldPos()
    {
        var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pos.z = 0;
        return pos;
    }
}
