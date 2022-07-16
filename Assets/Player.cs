using UnityEngine;

public class Player : MonoBehaviour
{
    public float targetSpeed;
    public float accelRate;

    public GameObject fireball;
    public float shootSpeed;

    private Rigidbody2D myRigidbody2D;

    private void Start()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            var newFireball = Instantiate(fireball);
            newFireball.transform.position = transform.position;
            var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            newFireball.GetComponent<Rigidbody2D>().velocity = (mousePos - transform.position).normalized * shootSpeed;
        }
    }

    private void FixedUpdate()
    {
        var targetVel = Vector2.zero;
        if (Input.GetKey(KeyCode.W))
        {
            targetVel += Vector2.up;
        }

        if (Input.GetKey(KeyCode.A))
        {
            targetVel += Vector2.left;
        }

        if (Input.GetKey(KeyCode.S))
        {
            targetVel += Vector2.down;
        }

        if (Input.GetKey(KeyCode.D))
        {
            targetVel += Vector2.right;
        }

        if (targetVel.magnitude > 1e-3)
        {
            targetVel = targetVel.normalized * targetSpeed;
        }

        var toTargetVel = targetVel - myRigidbody2D.velocity;
        myRigidbody2D.velocity += Time.fixedDeltaTime * accelRate * toTargetVel;
    }
}