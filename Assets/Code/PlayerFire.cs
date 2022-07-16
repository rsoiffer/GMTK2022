using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    public GameObject fireball;
    public float shootSpeed;

    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            var newFireball = Instantiate(fireball);
            newFireball.transform.position = transform.position;
            var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var toMouse = mousePos - transform.position;
            toMouse.z = 0;
            newFireball.GetComponent<Rigidbody2D>().velocity = shootSpeed * toMouse.normalized;
        }
    }
}