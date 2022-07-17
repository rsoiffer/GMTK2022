using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float moveSpeed = 2f;

    private GameObject player;
    private Rigidbody2D enemyRigidbody;
    private Vector2 direction;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        enemyRigidbody = gameObject.GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (player == null) return;
        direction = (Vector2)player.transform.position - enemyRigidbody.position;
        direction.Normalize();
        MoveCharacter();
    }

    private void MoveCharacter()
    {
        enemyRigidbody.velocity = direction * moveSpeed;
    }
}