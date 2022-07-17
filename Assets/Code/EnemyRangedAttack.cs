using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRangedAttack : MonoBehaviour
{
    public Projectile fireball;
    public float playerDistance = 10f;
    public float shootSpeed = 7f;
    private bool isRunning = false;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if ((Player.Instance.transform.position - transform.position).magnitude < playerDistance)
        {
            if (isRunning == false)
            {
                StartCoroutine(Shoot());
            }
        }
    }

    IEnumerator Shoot()
    {
        isRunning = true;
        Projectile projectile = Instantiate(fireball);
        projectile.transform.position = transform.position;
        Vector2 direction = Player.Instance.transform.position - transform.position;
        projectile.GetComponent<Rigidbody2D>().velocity = shootSpeed * direction.normalized;
        yield return new WaitForSeconds(3f);
        isRunning = false;
    }
}
