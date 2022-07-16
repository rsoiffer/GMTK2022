using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Vector2 playerPos;
    private Rigidbody2D enemyRigidbody;
    public float moveSpeed = 2f;
    private Vector2 direction;
    
    // Start is called before the first frame update
    void Start()
    {
        playerPos = GameObject.FindWithTag("Player").GetComponent<Transform>().position;
        enemyRigidbody = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    { 
        direction = (playerPos- enemyRigidbody.position);
        direction.Normalize();
    }
    
    void FixedUpdate()
    {
        moveCharacter(direction);
    }

    void moveCharacter(Vector2 movement)
    {
        enemyRigidbody.velocity = direction * moveSpeed;
    }
}
