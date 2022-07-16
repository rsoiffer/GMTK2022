using UnityEngine;

public class PlayerWater : MonoBehaviour
{
    public GameObject waterball;
    public float shootSpeed;

    private Vector2 ToMouse()
    {
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var toMouse = mousePos - transform.position;
        return toMouse;
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            var newWaterball = Instantiate(waterball);
            newWaterball.transform.position = transform.position;
            newWaterball.GetComponent<Rigidbody2D>().velocity = shootSpeed * ToMouse().normalized;
            newWaterball.GetComponent<Rigidbody2D>().velocity = shootSpeed * ToMouse().normalized;
        }
    }
}