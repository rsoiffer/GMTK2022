using UnityEngine;

public class PlayerAir : MonoBehaviour
{
    private Vector2 ToMouse()
    {
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var toMouse = mousePos - transform.position;
        return toMouse;
    }
}