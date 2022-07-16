using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    public GameObject fireball;
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
            var newFireball = Instantiate(fireball);
            newFireball.transform.position = transform.position;
            newFireball.GetComponent<Rigidbody2D>().velocity = shootSpeed * ToMouse().normalized;
        }
    }
}