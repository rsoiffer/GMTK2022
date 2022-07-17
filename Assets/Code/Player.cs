using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;

    public float targetSpeed;
    public float accelRate;

    public List<GameObject> abilities;
    private int currentAbility = 0;

    private Rigidbody2D myRigidbody2D;

    private void Start()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();

        if (Instance != null)
        {
            Destroy(gameObject);
            Instance.transform.position = transform.position;
            return;
        }

        Instance = this;
        transform.parent = null;
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) currentAbility = 0;
        if (Input.GetKeyDown(KeyCode.Alpha2)) currentAbility = 1;
        if (Input.GetKeyDown(KeyCode.Alpha3)) currentAbility = 2;
        if (Input.GetKeyDown(KeyCode.Alpha4)) currentAbility = 3;

        for (int i = 0; i < abilities.Count; i++)
        {
            abilities[i].SetActive(i == currentAbility);
        }
    }

    private void FixedUpdate()
    {
        var targetVel = Vector2.zero;
        if (Input.GetKey(KeyCode.W)) targetVel += Vector2.up;
        if (Input.GetKey(KeyCode.A)) targetVel += Vector2.left;
        if (Input.GetKey(KeyCode.S)) targetVel += Vector2.down;
        if (Input.GetKey(KeyCode.D)) targetVel += Vector2.right;

        if (targetVel.magnitude > 1e-3)
        {
            targetVel = targetVel.normalized * targetSpeed;
        }

        var toTargetVel = targetVel - myRigidbody2D.velocity;
        myRigidbody2D.velocity += Time.fixedDeltaTime * accelRate * toTargetVel;
    }
}