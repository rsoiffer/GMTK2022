using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float accelRate = 10;

    private GameObject player;
    private Rigidbody2D myRigidbody2D;
    private Vector2 direction;
    public float distanceToPlayer =0f;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        myRigidbody2D = gameObject.GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (player == null) return;
        
        direction = (Vector2)player.transform.position - enemyRigidbody.position;

        if (direction.magnitude > distanceToPlayer)
        {
            direction.Normalize();
            MoveCharacter();   
        }
    }

    private void MoveCharacter()
    {
        var toTargetVel = direction * moveSpeed - myRigidbody2D.velocity;
        myRigidbody2D.velocity += Time.fixedDeltaTime * accelRate * toTargetVel;
    }
}